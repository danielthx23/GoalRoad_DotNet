using GoalRoad.Application.DTOs;
using GoalRoad.Application.Mappers;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases
{
    public class RoadMapTecnologiaUseCase : IRoadMapTecnologiaUseCase
    {
        private readonly IRoadMapTecnologiaRepository _repo;

        public RoadMapTecnologiaUseCase(IRoadMapTecnologiaRepository repo)
        {
            _repo = repo;
        }

        public async Task<PageResultModel<IEnumerable<RoadMapTecnologiaDto>>> ObterTodasAsync(int offset = 0, int limit = 10)
        {
            var result = await _repo.ObterTodasAsync(offset, limit);
            return new PageResultModel<IEnumerable<RoadMapTecnologiaDto>> { Data = result.Data?.Select(rt => rt.ToDto()).ToList(), Total = result.Total };
        }

        public async Task<RoadMapTecnologiaDto?> ObterPorIdAsync(int id1, int id2)
        {
            var rt = await _repo.ObterPorIdAsync(id1, id2);
            return rt?.ToDto();
        }

        public async Task<RoadMapTecnologiaDto?> SalvarAsync(RoadMapTecnologiaDto dto)
        {
            var entity = dto.ToEntity();
            var saved = await _repo.SalvarAsync(entity);
            return saved?.ToDto();
        }

        public async Task<RoadMapTecnologiaDto?> AtualizarAsync(RoadMapTecnologiaDto dto)
        {
            var entity = dto.ToEntity();
            var updated = await _repo.AtualizarAsync(entity);
            return updated?.ToDto();
        }

        public async Task<RoadMapTecnologiaDto?> DeletarAsync(int id1, int id2)
        {
            var deleted = await _repo.DeletarAsync(id1, id2);
            return deleted?.ToDto();
        }
    }
}


