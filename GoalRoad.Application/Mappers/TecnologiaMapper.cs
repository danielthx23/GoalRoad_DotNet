using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Entities;

namespace GoalRoad.Application.Mappers
{
    public static class TecnologiaMapper
    {
        public static TecnologiaDto ToDto(this TecnologiaEntity e) => new()
        {
            IdTecnologia = e.IdTecnologia,
            NomeTecnologia = e.NomeTecnologia,
            DescricaoTecnologia = e.DescricaoTecnologia,
            LogoTecnologia = e.LogoTecnologia
        };

        public static TecnologiaEntity ToEntity(this TecnologiaDto d) => new()
        {
            IdTecnologia = d.IdTecnologia,
            NomeTecnologia = d.NomeTecnologia,
            DescricaoTecnologia = d.DescricaoTecnologia,
            LogoTecnologia = d.LogoTecnologia
        };
    }
}