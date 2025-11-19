using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases.Interfaces
{
    public interface IUsuarioUseCase
    {
        Task<PageResultModel<IEnumerable<UsuarioDto>>> ObterTodasAsync(int offset = 0, int limit = 10);
        Task<UsuarioDto?> ObterPorIdAsync(int id);
        Task<UsuarioDto?> SalvarAsync(UsuarioDto dto);
        Task<UsuarioDto?> AtualizarAsync(UsuarioDto dto);
        Task<UsuarioDto?> DeletarAsync(int id);
    }
}
