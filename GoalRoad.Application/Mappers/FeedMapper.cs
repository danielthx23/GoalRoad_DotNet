using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Entities;

namespace GoalRoad.Application.Mappers
{
    public static class FeedMapper
    {
        public static FeedDto ToDto(this FeedEntity e) => new()
        {
            IdUsuario = e.IdUsuario,
            Items = e.Items?.Select(i => i.ToDto()).ToList() ?? new()
        };

        public static FeedEntity ToEntity(this FeedDto d) => new()
        {
            IdUsuario = d.IdUsuario,
            Items = d.Items?.Select(i => i.ToEntity()).ToList() ?? new()
        };
    }
}


