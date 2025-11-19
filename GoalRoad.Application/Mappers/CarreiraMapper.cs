using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Entities;

namespace GoalRoad.Application.Mappers
{
    public static class CarreiraMapper
    {
        public static CarreiraDto ToDto(this CarreiraEntity e) => new()
        {
            IdCarreira = e.IdCarreira,
            TituloCarreira = e.TituloCarreira,
            DescricaoCarreira = e.DescricaoCarreira,
            LogoCarreira = e.LogoCarreira,
            IdCategoria = e.IdCategoria
        };

        public static CarreiraEntity ToEntity(this CarreiraDto d) => new()
        {
            IdCarreira = d.IdCarreira,
            TituloCarreira = d.TituloCarreira,
            DescricaoCarreira = d.DescricaoCarreira,
            LogoCarreira = d.LogoCarreira,
            IdCategoria = d.IdCategoria
        };
    }
}