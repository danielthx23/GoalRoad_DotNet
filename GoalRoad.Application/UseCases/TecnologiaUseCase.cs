using GoalRoad.Application.DTOs;
using GoalRoad.Application.Mappers;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases
{
    public class TecnologiaUseCase : ITecnologiaUseCase
    {
        private readonly ITecnologiaRepository _repo;

        public TecnologiaUseCase(ITecnologiaRepository repo)
        {
            _repo = repo;
        }

        public async Task<PageResultModel<IEnumerable<TecnologiaDto>>> ObterTodasAsync(int offset = 0, int limit = 10)
        {
            var result = await _repo.ObterTodasAsync(offset, limit);
            return new PageResultModel<IEnumerable<TecnologiaDto>> { Data = result.Data?.Select(t => t.ToDto()).ToList(), Total = result.Total };
        }

        public async Task<TecnologiaDto?> ObterPorIdAsync(int id)
        {
            var t = await _repo.ObterPorIdAsync(id);
            return t?.ToDto();
        }

        public async Task<TecnologiaDto?> SalvarAsync(TecnologiaDto dto)
        {
            var entity = dto.ToEntity();
            var saved = await _repo.SalvarAsync(entity);
            return saved?.ToDto();
        }

        public async Task<TecnologiaDto?> AtualizarAsync(TecnologiaDto dto)
        {
            var entity = dto.ToEntity();
            var updated = await _repo.AtualizarAsync(entity);
            return updated?.ToDto();
        }

        public async Task<TecnologiaDto?> DeletarAsync(int id)
        {
            var deleted = await _repo.DeletarAsync(id);
            return deleted?.ToDto();
        }
    }
}