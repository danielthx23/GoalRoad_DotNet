using GoalRoad.Application.DTOs;
using GoalRoad.Application.Mappers;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases
{
    public class FeedItemUseCase : IFeedItemUseCase
    {
        private readonly IFeedItemRepository _repo;

        public FeedItemUseCase(IFeedItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<PageResultModel<IEnumerable<FeedItemDto>>> ObterTodasAsync(int offset = 0, int limit = 10)
        {
            var result = await _repo.ObterTodasAsync(offset, limit);
            return new PageResultModel<IEnumerable<FeedItemDto>> { Data = result.Data?.Select(fi => fi.ToDto()).ToList(), Total = result.Total };
        }

        public async Task<FeedItemDto?> ObterPorIdAsync(int id)
        {
            var fi = await _repo.ObterPorIdAsync(id);
            return fi?.ToDto();
        }

        public async Task<FeedItemDto?> SalvarAsync(FeedItemDto dto)
        {
            var entity = dto.ToEntity();
            var saved = await _repo.SalvarAsync(entity);
            return saved?.ToDto();
        }

        public async Task<FeedItemDto?> AtualizarAsync(FeedItemDto dto)
        {
            var entity = dto.ToEntity();
            var updated = await _repo.AtualizarAsync(entity);
            return updated?.ToDto();
        }

        public async Task<FeedItemDto?> DeletarAsync(int id)
        {
            var deleted = await _repo.DeletarAsync(id);
            return deleted?.ToDto();
        }
    }
}




