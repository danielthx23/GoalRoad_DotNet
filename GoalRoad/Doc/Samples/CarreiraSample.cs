using Swashbuckle.AspNetCore.Filters;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Entities;

namespace GoalRoad.Doc.Samples
{
    public class CarreiraResponseListSample : IExamplesProvider<IEnumerable<CarreiraEntity>>
    {
        public IEnumerable<CarreiraEntity> GetExamples()
        {
            return new List<CarreiraEntity>
            {
                new CarreiraEntity
                {
                    IdCarreira = 1,
                    TituloCarreira = "Desenvolvedor .NET",
                    DescricaoCarreira = "Roadmap para se tornar um desenvolvedor .NET sênior",
                    LogoCarreira = "https://example.com/logo.png",
                    IdCategoria = 1
                },
                new CarreiraEntity
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

    public class CarreiraResponseSample : IExamplesProvider<CarreiraEntity>
    {
        public CarreiraEntity GetExamples()
        {
            return new CarreiraEntity
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
                IdCarreira = 0,
                TituloCarreira = "Desenvolvedor .NET",
                DescricaoCarreira = "Roadmap para se tornar um desenvolvedor .NET sênior",
                LogoCarreira = "https://example.com/logo.png",
                IdCategoria = 1
            };
        }
    }
}
