using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases.Interfaces
{
    public interface ILocalizacaoUseCase
    {
        Task<PageResultModel<IEnumerable<LocalizacaoDto>>> ObterTodasAsync(int offset = 0, int limit = 10);
        Task<LocalizacaoDto?> ObterPorIdAsync(int id);
        Task<LocalizacaoDto?> SalvarAsync(LocalizacaoDto dto);
        Task<LocalizacaoDto?> AtualizarAsync(LocalizacaoDto dto);
        Task<LocalizacaoDto?> DeletarAsync(int id);
    }
}
