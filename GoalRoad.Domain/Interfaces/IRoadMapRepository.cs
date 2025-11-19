using System.Collections.Generic;
using System.Threading.Tasks;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Models;

namespace GoalRoad.Domain.Interfaces
{
    public interface IRoadMapRepository
    {
        Task<PageResultModel<IEnumerable<RoadMapEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<RoadMapEntity?> ObterPorIdAsync(int id);
        Task<RoadMapEntity?> SalvarAsync(RoadMapEntity entity);
        Task<RoadMapEntity?> AtualizarAsync(RoadMapEntity entity);
        Task<RoadMapEntity?> DeletarAsync(int id);
        Task<PageResultModel<IEnumerable<RoadMapEntity>>> ObterTodasAsync();
    }
}
