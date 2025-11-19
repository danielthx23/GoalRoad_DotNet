using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases.Interfaces
{
    public interface IFeedItemUseCase
    {
        Task<PageResultModel<IEnumerable<FeedItemDto>>> ObterTodasAsync(int offset = 0, int limit = 10);
        Task<FeedItemDto?> ObterPorIdAsync(int id);
        Task<FeedItemDto?> SalvarAsync(FeedItemDto dto);
        Task<FeedItemDto?> AtualizarAsync(FeedItemDto dto);
        Task<FeedItemDto?> DeletarAsync(int id);
    }
}
