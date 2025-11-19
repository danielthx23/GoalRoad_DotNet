using System.Collections.Generic;
using System.Threading.Tasks;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Models;

namespace GoalRoad.Domain.Interfaces
{
    public interface ITecnologiaRepository
    {
        Task<PageResultModel<IEnumerable<TecnologiaEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<TecnologiaEntity?> ObterPorIdAsync(int id);
        Task<TecnologiaEntity?> SalvarAsync(TecnologiaEntity entity);
        Task<TecnologiaEntity?> AtualizarAsync(TecnologiaEntity entity);
        Task<TecnologiaEntity?> DeletarAsync(int id);
        Task<PageResultModel<IEnumerable<TecnologiaEntity>>> ObterTodasAsync();
    }
}
