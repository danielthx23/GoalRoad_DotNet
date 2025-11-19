using System.Collections.Generic;
using System.Threading.Tasks;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Models;

namespace GoalRoad.Domain.Interfaces
{
    public interface IRoadMapTecnologiaRepository
    {
        Task<PageResultModel<IEnumerable<RoadMapTecnologiaEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<RoadMapTecnologiaEntity?> ObterPorIdAsync(int id1, int id2);
        Task<RoadMapTecnologiaEntity?> SalvarAsync(RoadMapTecnologiaEntity entity);
        Task<RoadMapTecnologiaEntity?> AtualizarAsync(RoadMapTecnologiaEntity entity);
        Task<RoadMapTecnologiaEntity?> DeletarAsync(int id1, int id2);
        Task<PageResultModel<IEnumerable<RoadMapTecnologiaEntity>>> ObterTodasAsync();
    }
}
