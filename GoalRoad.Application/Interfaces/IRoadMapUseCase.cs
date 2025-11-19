using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases.Interfaces
{
    public interface IRoadMapUseCase
    {
        Task<PageResultModel<IEnumerable<RoadMapDto>>> ObterTodasAsync(int offset = 0, int limit = 10);
        Task<RoadMapDto?> ObterPorIdAsync(int id);
        Task<RoadMapDto?> SalvarAsync(RoadMapDto dto);
        Task<RoadMapDto?> AtualizarAsync(RoadMapDto dto);
        Task<RoadMapDto?> DeletarAsync(int id);
    }
}
