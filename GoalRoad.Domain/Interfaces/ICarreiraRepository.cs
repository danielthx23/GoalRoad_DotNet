using System.Collections.Generic;
using System.Threading.Tasks;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Models;

namespace GoalRoad.Domain.Interfaces
{
    public interface ICarreiraRepository
    {
        Task<PageResultModel<IEnumerable<CarreiraEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<CarreiraEntity?> ObterPorIdAsync(int id);
        Task<CarreiraEntity?> SalvarAsync(CarreiraEntity entity);
        Task<CarreiraEntity?> AtualizarAsync(CarreiraEntity entity);
        Task<CarreiraEntity?> DeletarAsync(int id);
    }
}
