using System.Collections.Generic;
using System.Threading.Tasks;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Models;

namespace GoalRoad.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<PageResultModel<IEnumerable<UsuarioEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3);
        Task<UsuarioEntity?> ObterPorIdAsync(int id);
        Task<UsuarioEntity?> ObterPorEmailAsync(string email);
        Task<UsuarioEntity?> SalvarAsync(UsuarioEntity entity);
        Task<UsuarioEntity?> AtualizarAsync(UsuarioEntity entity);
        Task<UsuarioEntity?> DeletarAsync(int id);
        Task<PageResultModel<IEnumerable<UsuarioEntity>>> ObterTodasAsync();
    }
}
