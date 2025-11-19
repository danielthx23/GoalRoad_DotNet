using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Entities;

namespace GoalRoad.Application.Mappers
{
    public static class LocalizacaoMapper
    {
        public static LocalizacaoDto ToDto(this LocalizacaoEntity e) => new()
        {
            IdEndereco = e.IdEndereco,
            Logradouro = e.Logradouro,
            Numero = e.Numero,
            Complemento = e.Complemento,
            Bairro = e.Bairro,
            Cidade = e.Cidade,
            Estado = e.Estado,
            CEP = e.CEP,
            Referencia = e.Referencia
        };

        public static LocalizacaoEntity ToEntity(this LocalizacaoDto d) => new()
        {
            IdEndereco = d.IdEndereco,
            Logradouro = d.Logradouro,
            Numero = d.Numero,
            Complemento = d.Complemento,
            Bairro = d.Bairro,
            Cidade = d.Cidade,
            Estado = d.Estado,
            CEP = d.CEP,
            Referencia = d.Referencia
        };
    }
}


