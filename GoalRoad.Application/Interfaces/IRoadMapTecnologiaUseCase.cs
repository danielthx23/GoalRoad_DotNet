using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Application.UseCases.Interfaces
{
    public interface IRoadMapTecnologiaUseCase
    {
        Task<PageResultModel<IEnumerable<RoadMapTecnologiaDto>>> ObterTodasAsync(int offset = 0, int limit = 10);
        Task<RoadMapTecnologiaDto?> ObterPorIdAsync(int id1, int id2);
        Task<RoadMapTecnologiaDto?> SalvarAsync(RoadMapTecnologiaDto dto);
        Task<RoadMapTecnologiaDto?> AtualizarAsync(RoadMapTecnologiaDto dto);
        Task<RoadMapTecnologiaDto?> DeletarAsync(int id1, int id2);
    }
}
