using GoalRoad.Application.DTOs;
using GoalRoad.Application.Mappers;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases
{
    public class RoadMapUseCase : IRoadMapUseCase
    {
        private readonly IRoadMapRepository _repo;

        public RoadMapUseCase(IRoadMapRepository repo)
        {
            _repo = repo;
        }

        public async Task<PageResultModel<IEnumerable<RoadMapDto>>> ObterTodasAsync(int offset = 0, int limit = 10)
        {
            var result = await _repo.ObterTodasAsync(offset, limit);
            return new PageResultModel<IEnumerable<RoadMapDto>> { Data = result.Data?.Select(r => r.ToDto()).ToList(), Total = result.Total };
        }

        public async Task<RoadMapDto?> ObterPorIdAsync(int id)
        {
            var r = await _repo.ObterPorIdAsync(id);
            return r?.ToDto();
        }

        public async Task<RoadMapDto?> SalvarAsync(RoadMapDto dto)
        {
            var entity = dto.ToEntity();
            var saved = await _repo.SalvarAsync(entity);
            return saved?.ToDto();
        }

        public async Task<RoadMapDto?> AtualizarAsync(RoadMapDto dto)
        {
            var entity = dto.ToEntity();
            var updated = await _repo.AtualizarAsync(entity);
            return updated?.ToDto();
        }

        public async Task<RoadMapDto?> DeletarAsync(int id)
        {
            var deleted = await _repo.DeletarAsync(id);
            return deleted?.ToDto();
        }
    }
}