using System.Text;
using System.Threading.RateLimiting;
using GoalRoad.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using DotNetEnv;

try
{
    var candidates = new[] {
        Path.Combine(Directory.GetCurrentDirectory(), ".env"),
        Path.Combine(Directory.GetCurrentDirectory(), "..", ".env"),
        Path.Combine(AppContext.BaseDirectory, ".env"),
        Path.Combine(AppContext.BaseDirectory, "..", ".env")
    };

    string? loadedPath = null;
    foreach (var p in candidates)
    {
        try
        {
            if (File.Exists(p))
            {
                Env.Load(p);
                loadedPath = p;
                break;
            }
        }
        catch { }
    }

    if (loadedPath != null)
        Console.WriteLine($"[DotNetEnv] Loaded .env from {loadedPath}");
    else
    {
        try { Env.Load(); Console.WriteLine("[DotNetEnv] Env.Load() called (default)"); } catch { Console.WriteLine("[DotNetEnv] Env.Load() default attempt failed or no .env found"); }
    }
}
catch (Exception ex)
{
    Console.WriteLine("[DotNetEnv] Env.Load() failed: " + ex.Message);
}

try
{
    string[] keys = new[] { "ConnectionStrings__DefaultConnection", "SQLSERVER_HOST", "SQLSERVER_PORT", "SQLSERVER_DATABASE", "SQLSERVER_USER", "SQLSERVER_PASSWORD", "ASPNETCORE_ENVIRONMENT" };
    foreach (var k in keys)
    {
        var v = Environment.GetEnvironmentVariable(k);
    }
}
catch { }

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

try
{
    string? configured = Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? builder.Configuration["ASPNETCORE_URLS"];
    if (!string.IsNullOrWhiteSpace(configured))
    {
        var first = configured.Split(';', ',').Select(s => s.Trim()).FirstOrDefault(s => !string.IsNullOrEmpty(s));
        if (!string.IsNullOrEmpty(first))
        {
            var normal = first.Replace("+", "localhost");
            if (System.Uri.TryCreate(normal, System.UriKind.Absolute, out var uri) && uri.Port > 0)
            {
                bool portFree = true;
                try
                {
                    var listener = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Loopback, uri.Port);
                    listener.Start();
                    listener.Stop();
                }
                catch
                {
                    portFree = false;
                }

                if (!portFree)
                {
                    try
                    {
                        Environment.SetEnvironmentVariable("ASPNETCORE_URLS", "http://localhost:0");
                    }
                    catch { }

                    builder.WebHost.UseUrls("http://localhost:0");
                    Console.WriteLine($"[PortCheck] Configured port {uri.Port} is in use. Overriding ASPNETCORE_URLS and falling back to ephemeral port (http://localhost:0).");
                }
                else
                {
                    Console.WriteLine($"[PortCheck] Configured port {uri.Port} is available.");
                }
            }
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine("[PortCheck] Error while checking configured ASPNETCORE_URLS: " + ex.Message);
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddInfrastructure(builder.Configuration);

try
{
    string[] keys = new[] { "ConnectionStrings__DefaultConnection", "SQLSERVER_HOST", "SQLSERVER_PORT", "SQLSERVER_DATABASE", "SQLSERVER_USER", "SQLSERVER_PASSWORD", "ASPNETCORE_ENVIRONMENT" };
    foreach (var k in keys)
    {
        var v = Environment.GetEnvironmentVariable(k);
    }
}
catch { }

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPresentation(builder.Configuration);

builder.Services.AddApiVersioningConfig();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddHealthChecksConfig();

builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("rateLimitePolicy", opt =>
    {
        opt.PermitLimit = 5;
        opt.Window = TimeSpan.FromSeconds(20);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

var app = builder.Build();

try
{
    var conn = builder.Configuration.GetConnectionString("DefaultConnection") ?? "<null>";
    Console.WriteLine($"[DEBUG] ConnectionStrings:DefaultConnection = {conn}");
}
catch { }

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
if (Directory.Exists(wwwrootPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(wwwrootPath),
        RequestPath = ""
    });
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();

app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = System.Text.Json.JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description,
                exception = e.Value.Exception?.Message,
                duration = e.Value.Duration.ToString()
            }),
            totalDuration = report.TotalDuration.ToString()
        });
        await context.Response.WriteAsync(result);
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

namespace GoalRoad
{
    public partial class Program { }
}