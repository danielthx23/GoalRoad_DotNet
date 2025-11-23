using Swashbuckle.AspNetCore.Filters;
using GoalRoad.Application.DTOs;

namespace GoalRoad.Doc.Samples
{
    public class CarreiraResponseListSample : IExamplesProvider<IEnumerable<CarreiraDto>>
    {
        public IEnumerable<CarreiraDto> GetExamples()
        {
            return new List<CarreiraDto>
            {
                new CarreiraDto
                {
                    IdCarreira = 1,
                    TituloCarreira = "Desenvolvedor .NET",
                    DescricaoCarreira = "Roadmap para se tornar um desenvolvedor .NET sênior",
                    LogoCarreira = "https://example.com/logo.png",
                    IdCategoria = 1
                },
                new CarreiraDto
                {
                    IdCarreira = 2,
                    TituloCarreira = "Desenvolvedor Full Stack",
                    DescricaoCarreira = "Roadmap para se tornar um desenvolvedor Full Stack",
                    LogoCarreira = "https://example.com/logo2.png",
                    IdCategoria = 1
                }
            };
        }
    }

    public class CarreiraResponseSample : IExamplesProvider<CarreiraDto>
    {
        public CarreiraDto GetExamples()
        {
            return new CarreiraDto
            {
                IdCarreira = 1,
                TituloCarreira = "Desenvolvedor .NET",
                DescricaoCarreira = "Roadmap para se tornar um desenvolvedor .NET sênior",
                LogoCarreira = "https://example.com/logo.png",
                IdCategoria = 1
            };
        }
    }

    public class CarreiraRequestSample : IExamplesProvider<CarreiraDto>
    {
        public CarreiraDto GetExamples()
        {
            return new CarreiraDto
            {
                TituloCarreira = "Desenvolvedor .NET",
                DescricaoCarreira = "Roadmap para se tornar um desenvolvedor .NET sênior",
                LogoCarreira = "https://example.com/logo.png",
                IdCategoria = 1
            };
        }
    }
}
