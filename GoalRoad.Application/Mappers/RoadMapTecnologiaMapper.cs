using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Entities;

namespace GoalRoad.Application.Mappers
{
    public static class RoadMapTecnologiaMapper
    {
        public static RoadMapTecnologiaDto ToDto(this RoadMapTecnologiaEntity e) => new()
        {
            IdRoadMap = e.IdRoadMap,
            IdTecnologia = e.IdTecnologia,
            StepOrder = e.StepOrder,
            Tecnologia = e.Tecnologia?.ToDto()
        };

        public static RoadMapTecnologiaEntity ToEntity(this RoadMapTecnologiaDto d) => new()
        {
            IdRoadMap = d.IdRoadMap,
            IdTecnologia = d.IdTecnologia,
            StepOrder = d.StepOrder
        };
    }
}