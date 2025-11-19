using System.Collections.Generic;
using System.Threading.Tasks;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Models;

namespace GoalRoad.Domain.Interfaces
{
    public interface IFeedItemRepository
    {
        Task<PageResultModel<IEnumerable<FeedItemEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<FeedItemEntity?> ObterPorIdAsync(int id);
        Task<FeedItemEntity?> SalvarAsync(FeedItemEntity entity);
        Task<FeedItemEntity?> AtualizarAsync(FeedItemEntity entity);
        Task<FeedItemEntity?> DeletarAsync(int id);
        Task<PageResultModel<IEnumerable<FeedItemEntity>>> ObterTodasAsync();
    }
}
