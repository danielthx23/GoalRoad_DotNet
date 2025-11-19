using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases.Interfaces
{
    public interface IFeedUseCase
    {
        Task<PageResultModel<IEnumerable<FeedDto>>> ObterTodasAsync(int offset = 0, int limit = 10);
        Task<FeedDto?> ObterPorIdAsync(int id);
        Task<FeedDto?> SalvarAsync(FeedDto dto);
        Task<FeedDto?> AtualizarAsync(FeedDto dto);
        Task<FeedDto?> DeletarAsync(int id);
        Task<FeedDto> GerarESalvarFeedParaUsuarioAsync(int userId, int idCarreira, int top = 20);
        Task TreinarModeloAsync();
    }
}
