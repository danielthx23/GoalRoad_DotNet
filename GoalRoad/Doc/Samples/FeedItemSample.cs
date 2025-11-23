using Swashbuckle.AspNetCore.Filters;
using GoalRoad.Application.DTOs;

namespace GoalRoad.Doc.Samples
{
    public class FeedItemResponseListSample : IExamplesProvider<IEnumerable<FeedItemDto>>
    {
        public IEnumerable<FeedItemDto> GetExamples()
        {
            return new List<FeedItemDto>
            {
                new FeedItemDto
                {
                    Id = 101,
                    IdUsuario = 1,
                    TipoItem = "article",
                    FonteId = 5,
                    Titulo = "Novas features em C# 12",
                    Descricao = "Resumo das novidades do C# 12...",
                    Url = "https://example.com/csharp12",
                    IdTecnologia = 1,
                    Relevancia = 0.92,
                    DataCriacao = DateTime.UtcNow
                },
                new FeedItemDto
                {
                    Id = 102,
                    IdUsuario = 1,
                    TipoItem = "video",
                    FonteId = 3,
                    Titulo = "ASP.NET Core Tutorial",
                    Descricao = "Aprenda ASP.NET Core do zero...",
                    Url = "https://example.com/aspnet-tutorial",
                    IdTecnologia = 2,
                    Relevancia = 0.85,
                    DataCriacao = DateTime.UtcNow
                }
            };
        }
    }

    public class FeedItemResponseSample : IExamplesProvider<FeedItemDto>
    {
        public FeedItemDto GetExamples()
        {
            return new FeedItemDto
            {
                Id = 101,
                IdUsuario = 1,
                TipoItem = "article",
                FonteId = 5,
                Titulo = "Novas features em C# 12",
                Descricao = "Resumo das novidades do C# 12...",
                Url = "https://example.com/csharp12",
                IdTecnologia = 1,
                Relevancia = 0.92,
                DataCriacao = DateTime.UtcNow
            };
        }
    }

    public class FeedItemRequestSample : IExamplesProvider<FeedItemDto>
    {
        public FeedItemDto GetExamples()
        {
            return new FeedItemDto
            {
                IdUsuario = 1,
                TipoItem = "article",
                FonteId = 5,
                Titulo = "Novas features em C# 12",
                Descricao = "Resumo das novidades do C# 12...",
                Url = "https://example.com/csharp12",
                IdTecnologia = 1,
                Relevancia = 0.92,
                DataCriacao = DateTime.UtcNow
            };
        }
    }
}
