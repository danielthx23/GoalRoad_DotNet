using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Versioning;
using GoalRoad.Infrastructure.Data.AppData;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Infrastructure.Data.Repositories;
using GoalRoad.Infrastructure.HealthCheck;
using GoalRoad.Application.UseCases;
using GoalRoad.Application.UseCases.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using DotNetEnv;

namespace GoalRoad.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // NOTE: .env should be loaded by Program.cs before calling AddInfrastructure. Keep a light diagnostic here.
        try
        {
            var loaded = Environment.GetEnvironmentVariable("DOTNETENV_LOADED");
            // no-op: just a diagnostic placeholder
        }
        catch { }
        
        // If running tests, use InMemory database to avoid requiring SQL Server
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? configuration["ASPNETCORE_ENVIRONMENT"];
        // Diagnostic output to help understand DB selection
        try
        {
            var cfgConn = configuration.GetConnectionString("DefaultConnection");
            var envConn = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
            var sqlHost = Environment.GetEnvironmentVariable("SQLSERVER_HOST");
            var sqlUser = Environment.GetEnvironmentVariable("SQLSERVER_USER");
            Console.WriteLine($"[AddInfrastructure] ASPNETCORE_ENVIRONMENT={env ?? "<null>"}");
            Console.WriteLine($"[AddInfrastructure] Configuration ConnectionStrings:DefaultConnection={(string.IsNullOrWhiteSpace(cfgConn)?"<empty>":cfgConn)}");
            Console.WriteLine($"[AddInfrastructure] ENV ConnectionStrings__DefaultConnection={(string.IsNullOrWhiteSpace(envConn)?"<empty>":envConn)}");
            Console.WriteLine($"[AddInfrastructure] SQLSERVER_HOST={(sqlHost==null?"<null":sqlHost)} SQLSERVER_USER={(sqlUser==null?"<null>":sqlUser)}");
        }
        catch { }
        if (string.Equals(env, "Testing", StringComparison.OrdinalIgnoreCase))
        {
            // Use fully-qualified method to ensure extension is found
            services.AddDbContext<ApplicationContext>(x => Microsoft.EntityFrameworkCore.InMemoryDbContextOptionsExtensions.UseInMemoryDatabase(x, "TestDb"));
        }
        else
        {

            // Configuração de conexão com SQL Server
            // Treat empty string from appsettings as missing so environment variables (e.g. from .env) are honored.
            string? connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTIONSTRING");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                var sqlHost = Environment.GetEnvironmentVariable("SQLSERVER_HOST") ?? "localhost";
                var sqlPort = Environment.GetEnvironmentVariable("SQLSERVER_PORT") ?? "1433";
                var sqlDatabase = Environment.GetEnvironmentVariable("SQLSERVER_DATABASE") ?? "GoalRoadDb";
                var sqlUser = Environment.GetEnvironmentVariable("SQLSERVER_USER");
                var sqlPassword = Environment.GetEnvironmentVariable("SQLSERVER_PASSWORD");
                var useIntegratedSecurity = string.Equals(Environment.GetEnvironmentVariable("SQLSERVER_INTEGRATED_SECURITY"), "true", StringComparison.OrdinalIgnoreCase);

                if (useIntegratedSecurity)
                {
                    connectionString = $"Server={sqlHost},{sqlPort};Database={sqlDatabase};Integrated Security=true;TrustServerCertificate=true;";
                }
                else if (!string.IsNullOrEmpty(sqlUser) && !string.IsNullOrEmpty(sqlPassword))
                {
                    connectionString = $"Server={sqlHost},{sqlPort};Database={sqlDatabase};User Id={sqlUser};Password={sqlPassword};TrustServerCertificate=true;";
                }
                else
                {
                    // No SQL connection configured: fall back to in-memory DB to allow tests/local runs
                    services.AddDbContext<ApplicationContext>(x => Microsoft.EntityFrameworkCore.InMemoryDbContextOptionsExtensions.UseInMemoryDatabase(x, "FallbackDb"));
                }
            }

            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                services.AddDbContext<ApplicationContext>(x =>
                {
                    x.UseSqlServer(connectionString, sqlOptions =>
                    {
                        // Enable retry on failure for transient faults (recommended for Azure SQL)
                        sqlOptions.EnableRetryOnFailure(maxRetryCount:5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        sqlOptions.CommandTimeout(60);
                    });
                });
            }
        }

        // Repositories registration - Always register, regardless of database type
        services.AddScoped<ICarreiraRepository, CarreiraRepository>();
        services.AddScoped<IRoadMapRepository, RoadMapRepository>();
        services.AddScoped<ITecnologiaRepository, TecnologiaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<ILocalizacaoRepository, LocalizacaoRepository>();
        services.AddScoped<IRoadMapTecnologiaRepository, RoadMapTecnologiaRepository>();
        services.AddScoped<IFeedRepository, FeedRepository>();
        services.AddScoped<IFeedItemRepository, FeedItemRepository>();

        // Health Checks
        services.AddScoped<DatabaseHealthCheck>();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Use Cases
        services.AddScoped<IAutenticarUseCase, AutenticarUseCase>();
        services.AddScoped<ICarreiraUseCase, CarreiraUseCase>();
        services.AddScoped<IRoadMapUseCase, RoadMapUseCase>();
        services.AddScoped<ITecnologiaUseCase, TecnologiaUseCase>();
        services.AddScoped<IUsuarioUseCase, UsuarioUseCase>();
        services.AddScoped<ICategoriaUseCase, CategoriaUseCase>();
        services.AddScoped<ILocalizacaoUseCase, LocalizacaoUseCase>();
        services.AddScoped<IRoadMapTecnologiaUseCase, RoadMapTecnologiaUseCase>();
        services.AddScoped<IFeedUseCase, FeedUseCase>();
        services.AddScoped<IFeedItemUseCase, FeedItemUseCase>();

        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        // MVC + Views + Razor Pages
        services.AddControllersWithViews()
            .AddRazorOptions(options =>
            {
                options.ViewLocationFormats.Clear();
                options.ViewLocationFormats.Add("/Views/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });

        services.AddRazorPages();

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // Swagger
        services.AddSwaggerGen(c =>
        {
            // Basic Swagger info
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "GoalRoad API",
                Version = "v1",
                Description = "API RESTful para gerenciamento de carreiras e roadmaps",
            });

            // JWT in Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description =
                    "JWT Authorization header usando o esquema Bearer. Exemplo: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            // enable examples filters
            c.ExampleFilters();
        });

        return services;
    }

    public static IServiceCollection AddApiVersioningConfig(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1,0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("version"),
                new HeaderApiVersionReader("X-Version"),
                new UrlSegmentApiVersionReader()
            );
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // Tenta ler de várias fontes: appsettings, variáveis de ambiente com __ ou sem
        // Prefer non-empty values from configuration or environment variables. Some config values may be present but empty,
        // so use IsNullOrWhiteSpace checks instead of simple null coalescing.
        string? jwtSecretKey = configuration["Jwt:Key"];
        if (string.IsNullOrWhiteSpace(jwtSecretKey)) jwtSecretKey = Environment.GetEnvironmentVariable("Jwt__Key");
        if (string.IsNullOrWhiteSpace(jwtSecretKey)) jwtSecretKey = configuration["Jwt:SecretKey"];
        if (string.IsNullOrWhiteSpace(jwtSecretKey)) jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
        if (string.IsNullOrWhiteSpace(jwtSecretKey)) jwtSecretKey = Environment.GetEnvironmentVariable("JWT_KEY");
        if (string.IsNullOrWhiteSpace(jwtSecretKey)) jwtSecretKey = "YourSuperSecretKeyThatShouldBeAtLeast32CharactersLongForHS256Algorithm";
         var jwtIssuer = configuration["Jwt:Issuer"]
             ?? Environment.GetEnvironmentVariable("Jwt__Issuer")
             ?? "GoalRoadAPI";
         var jwtAudience = configuration["Jwt:Audience"]
             ?? Environment.GetEnvironmentVariable("Jwt__Audience")
             ?? "GoalRoadClient";

         var keyBytes = Encoding.UTF8.GetBytes(jwtSecretKey);

         services.AddAuthentication(options =>
         {
             options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
             options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
         })
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = jwtIssuer,
                 ValidAudience = jwtAudience,
                 IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                 ClockSkew = TimeSpan.Zero
             };
         });

         services.AddAuthorization();

         return services;
     }

    public static IServiceCollection AddHealthChecksConfig(this IServiceCollection services)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var hc = services.AddHealthChecks();

        // Liveness
        hc.AddCheck("self", () => HealthCheckResult.Healthy(), tags: new[] { "live" });
        
        // Readiness
        if (!string.Equals(env, "Testing", StringComparison.OrdinalIgnoreCase))
        {
            hc.AddCheck<DatabaseHealthCheck>("database", tags: new[] { "ready" });
        }

        return services;
    }

}
