using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases.Interfaces
{
    public interface ICategoriaUseCase
    {
        Task<PageResultModel<IEnumerable<CategoriaDto>>> ObterTodasAsync(int offset = 0, int limit = 10);
        Task<CategoriaDto?> ObterPorIdAsync(int id);
        Task<CategoriaDto?> SalvarAsync(CategoriaDto dto);
        Task<CategoriaDto?> AtualizarAsync(CategoriaDto dto);
        Task<CategoriaDto?> DeletarAsync(int id);
    }
}
