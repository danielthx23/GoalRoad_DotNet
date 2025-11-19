using GoalRoad.Application.DTOs;
using GoalRoad.Application.Mappers;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases
{
    public class LocalizacaoUseCase : ILocalizacaoUseCase
    {
        private readonly ILocalizacaoRepository _repo;

        public LocalizacaoUseCase(ILocalizacaoRepository repo)
        {
            _repo = repo;
        }

        public async Task<PageResultModel<IEnumerable<LocalizacaoDto>>> ObterTodasAsync(int offset = 0, int limit = 10)
        {
            var result = await _repo.ObterTodasAsync(offset, limit);
            return new PageResultModel<IEnumerable<LocalizacaoDto>> { Data = result.Data?.Select(l => l.ToDto()).ToList(), Total = result.Total };
        }

        public async Task<LocalizacaoDto?> ObterPorIdAsync(int id)
        {
            var l = await _repo.ObterPorIdAsync(id);
            return l?.ToDto();
        }

        public async Task<LocalizacaoDto?> SalvarAsync(LocalizacaoDto dto)
        {
            var entity = dto.ToEntity();
            var saved = await _repo.SalvarAsync(entity);
            return saved?.ToDto();
        }

        public async Task<LocalizacaoDto?> AtualizarAsync(LocalizacaoDto dto)
        {
            var entity = dto.ToEntity();
            var updated = await _repo.AtualizarAsync(entity);
            return updated?.ToDto();
        }

        public async Task<LocalizacaoDto?> DeletarAsync(int id)
        {
            var deleted = await _repo.DeletarAsync(id);
            return deleted?.ToDto();
        }
    }
}




