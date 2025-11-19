using Swashbuckle.AspNetCore.Filters;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Entities;

namespace GoalRoad.Doc.Samples
{
    public class TecnologiaResponseListSample : IExamplesProvider<IEnumerable<TecnologiaEntity>>
    {
        public IEnumerable<TecnologiaEntity> GetExamples()
        {
            return new List<TecnologiaEntity>
            {
                new TecnologiaEntity
                {
                    IdTecnologia = 1,
                    NomeTecnologia = "C#",
                    DescricaoTecnologia = "Linguagem de programação da Microsoft",
                    LogoTecnologia = "https://example.com/csharp.png"
                },
                new TecnologiaEntity
                {
                    IdTecnologia = 2,
                    NomeTecnologia = "ASP.NET Core",
                    DescricaoTecnologia = "Framework web da Microsoft",
                    LogoTecnologia = "https://example.com/aspnet.png"
                }
            };
        }
    }

    public class TecnologiaResponseSample : IExamplesProvider<TecnologiaEntity>
    {
        public TecnologiaEntity GetExamples()
        {
            return new TecnologiaEntity
            {
                IdTecnologia = 1,
                NomeTecnologia = "C#",
                DescricaoTecnologia = "Linguagem de programação da Microsoft",
                LogoTecnologia = "https://example.com/csharp.png"
            };
        }
    }

    public class TecnologiaRequestSample : IExamplesProvider<TecnologiaDto>
    {
        public TecnologiaDto GetExamples()
        {
            return new TecnologiaDto
            {
                IdTecnologia = 0,
                NomeTecnologia = "C#",
                DescricaoTecnologia = "Linguagem de programação da Microsoft",
                LogoTecnologia = "https://example.com/csharp.png"
            };
        }
    }
}
