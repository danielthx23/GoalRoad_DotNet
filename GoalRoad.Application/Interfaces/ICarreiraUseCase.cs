using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases.Interfaces
{
    public interface ICarreiraUseCase
    {
        Task<PageResultModel<IEnumerable<CarreiraDto>>> ObterTodasAsync(int offset = 0, int limit = 10);
        Task<CarreiraDto?> ObterPorIdAsync(int id);
        Task<CarreiraDto?> SalvarAsync(CarreiraDto dto);
        Task<CarreiraDto?> AtualizarAsync(CarreiraDto dto);
        Task<CarreiraDto?> DeletarAsync(int id);
    }
}
