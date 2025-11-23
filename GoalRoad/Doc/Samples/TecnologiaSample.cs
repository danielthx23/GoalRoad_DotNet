using Swashbuckle.AspNetCore.Filters;
using GoalRoad.Application.DTOs;

namespace GoalRoad.Doc.Samples
{
    public class TecnologiaResponseListSample : IExamplesProvider<IEnumerable<TecnologiaDto>>
    {
        public IEnumerable<TecnologiaDto> GetExamples()
        {
            return new List<TecnologiaDto>
            {
                new TecnologiaDto
                {
                    IdTecnologia = 1,
                    NomeTecnologia = "C#",
                    DescricaoTecnologia = "Linguagem de programação da Microsoft",
                    LogoTecnologia = "https://example.com/csharp.png"
                },
                new TecnologiaDto
                {
                    IdTecnologia = 2,
                    NomeTecnologia = "ASP.NET Core",
                    DescricaoTecnologia = "Framework web da Microsoft",
                    LogoTecnologia = "https://example.com/aspnet.png"
                }
            };
        }
    }

    public class TecnologiaResponseSample : IExamplesProvider<TecnologiaDto>
    {
        public TecnologiaDto GetExamples()
        {
            return new TecnologiaDto
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
                NomeTecnologia = "C#",
                DescricaoTecnologia = "Linguagem de programação da Microsoft",
                LogoTecnologia = "https://example.com/csharp.png"
            };
        }
    }
}
