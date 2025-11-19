using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Entities;

namespace GoalRoad.Application.Mappers
{
    public static class RoadMapMapper
    {
        public static RoadMapDto ToDto(this RoadMapEntity e) => new()
        {
            IdCarreira = e.IdCarreira,
            Tecnologias = e.Tecnologias?.Select(rt => rt.ToDto()).ToList() ?? new()
        };

        public static RoadMapEntity ToEntity(this RoadMapDto d) => new()
        {
            IdCarreira = d.IdCarreira,
            Tecnologias = d.Tecnologias?.Select(rt => rt.ToEntity()).ToList() ?? new()
        };
    }
}