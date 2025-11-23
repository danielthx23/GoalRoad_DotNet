using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using GoalRoad.Application.DTOs;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Domain.Models;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace GoalRoad.Tests.App
{
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public new const string Scheme = "TestAuth";

        public TestAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, "tester"),
                new Claim(ClaimTypes.Role, "admin"), // importante para passar no Authorize(Roles="admin")
            };

            var identity = new ClaimsIdentity(claims, Scheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public Mock<ICarreiraUseCase> CarreiraUseCaseMock { get; } = new();

        protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Cria o Mock do ICarreiraUseCase
                services.RemoveAll(typeof(ICarreiraUseCase));
                services.AddSingleton(CarreiraUseCaseMock.Object);

                // Autenticação fake
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = TestAuthHandler.Scheme;
                    options.DefaultChallengeScheme = TestAuthHandler.Scheme;
                })
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.Scheme, _ => { });
            });
        }
    }

    public class CarreiraControllerTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;

        public CarreiraControllerTest(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact(DisplayName = "ObterTodas - Retorna todos os dados")]
        [Trait("Controller", "Carreira")]
        public async Task ObterTodas_RetornarDados()
        {
            // Arrange
            var retornoCarreira = new PageResultModel<IEnumerable<CarreiraDto>>
            {
                Data = new List<CarreiraDto>
                {
                    new CarreiraDto
                    {
                        IdCarreira = 1,
                        TituloCarreira = "Desenvolvedor Full Stack",
                        DescricaoCarreira = "Desenvolvimento de aplicações web completas"
                    }
                },
                Total = 1
            };

            _factory.CarreiraUseCaseMock
                .Setup(x => x.ObterTodasAsync(0, 10))
                .ReturnsAsync(retornoCarreira);

            using var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("api/v1/Carreira?offset=0&limit=10");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
