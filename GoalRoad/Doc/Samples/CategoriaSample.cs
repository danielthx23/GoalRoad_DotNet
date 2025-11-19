using Swashbuckle.AspNetCore.Filters;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Entities;

namespace GoalRoad.Doc.Samples
{
    public class CategoriaResponseListSample : IExamplesProvider<IEnumerable<CategoriaEntity>>
    {
        public IEnumerable<CategoriaEntity> GetExamples()
        {
            return new List<CategoriaEntity>
            {
                new CategoriaEntity
                {
                    IdCategoria = 1,
                    NomeCategoria = "Desenvolvimento",
                    DescricaoCategoria = "Conteúdos relacionados a desenvolvimento de software"
                },
                new CategoriaEntity
                {
                    IdCategoria = 2,
                    NomeCategoria = "DevOps",
                    DescricaoCategoria = "Conteúdos relacionados a DevOps e infraestrutura"
                }
            };
        }
    }

    public class CategoriaResponseSample : IExamplesProvider<CategoriaEntity>
    {
        public CategoriaEntity GetExamples()
        {
            return new CategoriaEntity
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
                IdCategoria = 0,
                NomeCategoria = "Desenvolvimento",
                DescricaoCategoria = "Conteúdos relacionados a desenvolvimento de software"
            };
        }
    }
}
