using GoalRoad.Application.DTOs;
using GoalRoad.Application.Mappers;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases
{
    public class UsuarioUseCase : IUsuarioUseCase
    {
        private readonly IUsuarioRepository _repo;

        public UsuarioUseCase(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<PageResultModel<IEnumerable<UsuarioDto>>> ObterTodasAsync(int offset = 0, int limit = 10)
        {
            var result = await _repo.ObterTodasAsync(offset, limit);
            return new PageResultModel<IEnumerable<UsuarioDto>> { Data = result.Data?.Select(u => u.ToUsuarioDto()).ToList(), Total = result.Total };
        }

        public async Task<UsuarioDto?> ObterPorIdAsync(int id)
        {
            var u = await _repo.ObterPorIdAsync(id);
            return u?.ToUsuarioDto();
        }

        public async Task<UsuarioDto?> SalvarAsync(UsuarioDto dto)
        {
            // Create minimal UsuarioEntity from DTO
            var entity = new GoalRoad.Domain.Entities.UsuarioEntity
            {
                NomeUsuario = dto.NomeUsuario,
                SenhaUsuario = dto.SenhaUsuario,
                EmailUsuario = dto.EmailUsuario,
                TokenUsuario = string.Empty,
                CriadoEmUsuario = DateTime.UtcNow
            };
            var saved = await _repo.SalvarAsync(entity);
            return saved?.ToUsuarioDto();
        }

        public async Task<UsuarioDto?> AtualizarAsync(UsuarioDto dto)
        {
            var entity = new GoalRoad.Domain.Entities.UsuarioEntity
            {
                IdUsuario = dto.IdUsuario,
                NomeUsuario = dto.NomeUsuario,
                SenhaUsuario = dto.SenhaUsuario,
                EmailUsuario = dto.EmailUsuario,
                TokenUsuario = string.Empty,
                CriadoEmUsuario = dto.CriadoEmUsuario
            };
            var updated = await _repo.AtualizarAsync(entity);
            return updated?.ToUsuarioDto();
        }

        public async Task<UsuarioDto?> DeletarAsync(int id)
        {
            var deleted = await _repo.DeletarAsync(id);
            return deleted?.ToUsuarioDto();
        }
    }
}