using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Entities;

namespace GoalRoad.Application.Mappers
{
    public static class FeedItemMapper
    {
        public static FeedItemDto ToDto(this FeedItemEntity e) => new()
        {
            Id = e.Id,
            IdUsuario = e.IdUsuario,
            TipoItem = e.TipoItem,
            FonteId = e.FonteId,
            Titulo = e.Titulo,
            Descricao = e.Descricao,
            Url = e.Url,
            IdTecnologia = e.IdTecnologia,
            Tecnologia = e.Tecnologia?.ToDto(),
            Relevancia = e.Relevancia,
            DataCriacao = e.DataCriacao
        };

        public static FeedItemEntity ToEntity(this FeedItemDto d) => new()
        {
            Id = d.Id,
            IdUsuario = d.IdUsuario,
            TipoItem = d.TipoItem,
            FonteId = d.FonteId,
            Titulo = d.Titulo,
            Descricao = d.Descricao,
            Url = d.Url,
            IdTecnologia = d.IdTecnologia,
            Relevancia = d.Relevancia,
            DataCriacao = d.DataCriacao
        };
    }
}


