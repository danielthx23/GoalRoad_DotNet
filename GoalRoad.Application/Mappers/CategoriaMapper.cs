using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Entities;

namespace GoalRoad.Application.Mappers
{
    public static class CategoriaMapper
    {
        public static CategoriaDto ToDto(this CategoriaEntity e) => new()
        {
            IdCategoria = e.IdCategoria,
            NomeCategoria = e.NomeCategoria,
            DescricaoCategoria = e.DescricaoCategoria
        };

        public static CategoriaEntity ToEntity(this CategoriaDto d) => new()
        {
            IdCategoria = d.IdCategoria,
            NomeCategoria = d.NomeCategoria,
            DescricaoCategoria = d.DescricaoCategoria
        };
    }
}


