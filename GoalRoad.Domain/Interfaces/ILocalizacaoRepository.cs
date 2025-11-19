using System.Collections.Generic;
using System.Threading.Tasks;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Models;

namespace GoalRoad.Domain.Interfaces
{
    public interface ILocalizacaoRepository
    {
        Task<PageResultModel<IEnumerable<LocalizacaoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<LocalizacaoEntity?> ObterPorIdAsync(int id);
        Task<LocalizacaoEntity?> SalvarAsync(LocalizacaoEntity entity);
        Task<LocalizacaoEntity?> AtualizarAsync(LocalizacaoEntity entity);
        Task<LocalizacaoEntity?> DeletarAsync(int id);
        Task<PageResultModel<IEnumerable<LocalizacaoEntity>>> ObterTodasAsync();
    }
}
