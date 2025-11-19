using GoalRoad.Application.DTOs;
using GoalRoad.Application.Mappers;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases
{
    public class CategoriaUseCase : ICategoriaUseCase
    {
        private readonly ICategoriaRepository _repo;

        public CategoriaUseCase(ICategoriaRepository repo)
        {
            _repo = repo;
        }

        public async Task<PageResultModel<IEnumerable<CategoriaDto>>> ObterTodasAsync(int offset = 0, int limit = 10)
        {
            var result = await _repo.ObterTodasAsync(offset, limit);
            return new PageResultModel<IEnumerable<CategoriaDto>> { Data = result.Data?.Select(c => c.ToDto()).ToList(), Total = result.Total };
        }

        public async Task<CategoriaDto?> ObterPorIdAsync(int id)
        {
            var c = await _repo.ObterPorIdAsync(id);
            return c?.ToDto();
        }

        public async Task<CategoriaDto?> SalvarAsync(CategoriaDto dto)
        {
            var entity = dto.ToEntity();
            var saved = await _repo.SalvarAsync(entity);
            return saved?.ToDto();
        }

        public async Task<CategoriaDto?> AtualizarAsync(CategoriaDto dto)
        {
            var entity = dto.ToEntity();
            var updated = await _repo.AtualizarAsync(entity);
            return updated?.ToDto();
        }

        public async Task<CategoriaDto?> DeletarAsync(int id)
        {
            var deleted = await _repo.DeletarAsync(id);
            return deleted?.ToDto();
        }
    }
}




