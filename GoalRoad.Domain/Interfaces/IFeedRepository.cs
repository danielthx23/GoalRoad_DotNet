using System.Collections.Generic;
using System.Threading.Tasks;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Models;

namespace GoalRoad.Domain.Interfaces
{
    public interface IFeedRepository
    {
        Task<PageResultModel<IEnumerable<FeedEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<FeedEntity?> ObterPorIdAsync(int id);
        Task<FeedEntity?> SalvarAsync(FeedEntity entity);
        Task<FeedEntity?> AtualizarAsync(FeedEntity entity);
        Task<FeedEntity?> DeletarAsync(int id);
        Task<PageResultModel<IEnumerable<FeedEntity>>> ObterTodasAsync();
    }
}
