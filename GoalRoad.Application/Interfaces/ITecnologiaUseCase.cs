using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases.Interfaces
{
    public interface ITecnologiaUseCase
    {
        Task<PageResultModel<IEnumerable<TecnologiaDto>>> ObterTodasAsync(int offset = 0, int limit = 10);
        Task<TecnologiaDto?> ObterPorIdAsync(int id);
        Task<TecnologiaDto?> SalvarAsync(TecnologiaDto dto);
        Task<TecnologiaDto?> AtualizarAsync(TecnologiaDto dto);
        Task<TecnologiaDto?> DeletarAsync(int id);
    }
}
