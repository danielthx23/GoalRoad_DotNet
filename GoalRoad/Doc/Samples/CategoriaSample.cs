using Swashbuckle.AspNetCore.Filters;
using GoalRoad.Application.DTOs;

namespace GoalRoad.Doc.Samples
{
    public class CategoriaResponseListSample : IExamplesProvider<IEnumerable<CategoriaDto>>
    {
        public IEnumerable<CategoriaDto> GetExamples()
        {
            return new List<CategoriaDto>
            {
                new CategoriaDto
                {
                    IdCategoria = 1,
                    NomeCategoria = "Desenvolvimento",
                    DescricaoCategoria = "Conteúdos relacionados a desenvolvimento de software"
                },
                new CategoriaDto
                {
                    IdCategoria = 2,
                    NomeCategoria = "DevOps",
                    DescricaoCategoria = "Conteúdos relacionados a DevOps e infraestrutura"
                }
            };
        }
    }

    public class CategoriaResponseSample : IExamplesProvider<CategoriaDto>
    {
        public CategoriaDto GetExamples()
        {
            return new CategoriaDto
            {
                IdCategoria = 1,
                NomeCategoria = "Desenvolvimento",
                DescricaoCategoria = "Conteúdos relacionados a desenvolvimento de software"
            };
        }
    }

    public class CategoriaRequestSample : IExamplesProvider<CategoriaDto>
    {
        public CategoriaDto GetExamples()
        {
            return new CategoriaDto
            {
                NomeCategoria = "Desenvolvimento",
                DescricaoCategoria = "Conteúdos relacionados a desenvolvimento de software"
            };
        }
    }
}
