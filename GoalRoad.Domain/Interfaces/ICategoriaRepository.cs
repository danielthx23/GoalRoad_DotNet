using System.Collections.Generic;
using System.Threading.Tasks;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Models;

namespace GoalRoad.Domain.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<PageResultModel<IEnumerable<CategoriaEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<CategoriaEntity?> ObterPorIdAsync(int id);
        Task<CategoriaEntity?> SalvarAsync(CategoriaEntity entity);
        Task<CategoriaEntity?> AtualizarAsync(CategoriaEntity entity);
        Task<CategoriaEntity?> DeletarAsync(int id);
        Task<PageResultModel<IEnumerable<CategoriaEntity>>> ObterTodasAsync();
    }
}
