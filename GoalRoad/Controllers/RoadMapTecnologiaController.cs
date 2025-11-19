using Microsoft.AspNetCore.Mvc;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoadMapTecnologiaController : ControllerBase
    {
        private readonly IRoadMapTecnologiaUseCase _useCase;
        public RoadMapTecnologiaController(IRoadMapTecnologiaUseCase useCase) => _useCase = useCase;

        [HttpGet]
        public async Task<ActionResult<PageResultModel<IEnumerable<RoadMapTecnologiaDto>>>> GetAll(int offset = 0, int limit = 10)
        {
            var result = await _useCase.ObterTodasAsync(offset, limit);
            return Ok(result);
        }

        [HttpGet("{id1:int}/{id2:int}")]
        public async Task<ActionResult<RoadMapTecnologiaDto?>> GetById(int id1, int id2)
        {
            var item = await _useCase.ObterPorIdAsync(id1, id2);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<RoadMapTecnologiaDto?>> Post(RoadMapTecnologiaDto dto)
        {
            var created = await _useCase.SalvarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id1 = created?.IdRoadMap, id2 = created?.IdTecnologia }, created);
        }

        [HttpPut("{id1:int}/{id2:int}")]
        public async Task<ActionResult<RoadMapTecnologiaDto?>> Put(int id1, int id2, RoadMapTecnologiaDto dto)
        {
            if (id1 != dto.IdRoadMap || id2 != dto.IdTecnologia) return BadRequest();
            var updated = await _useCase.AtualizarAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id1:int}/{id2:int}")]
        public async Task<ActionResult<RoadMapTecnologiaDto?>> Delete(int id1, int id2)
        {
            var deleted = await _useCase.DeletarAsync(id1, id2);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
