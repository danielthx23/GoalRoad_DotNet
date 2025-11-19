using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Entities;

namespace GoalRoad.Application.Mappers
{
    public static class UsuarioMapper
    {
        public static UsuarioLoginDto ToLoginDto(this UsuarioEntity e) => new()
        {
            Email = e.EmailUsuario,
            Senha = e.SenhaUsuario
        };
        
        public static UsuarioDto ToUsuarioDto(this UsuarioEntity e) => new()
        {
            IdUsuario = e.IdUsuario,
            NomeUsuario = e.NomeUsuario,
            EmailUsuario = e.EmailUsuario,
            SenhaUsuario = e.SenhaUsuario,
            CriadoEmUsuario = e.CriadoEmUsuario
        };

        // Additional mappers can be added here
    }
}