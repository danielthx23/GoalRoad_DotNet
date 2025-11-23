using Microsoft.AspNetCore.Mvc;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RoadMapController : ControllerBase
    {
        private readonly IRoadMapUseCase _useCase;
        public RoadMapController(IRoadMapUseCase useCase) => _useCase = useCase;

        [HttpGet]
        public async Task<ActionResult<PageResultModel<IEnumerable<RoadMapDto>>>> GetAll(int offset = 0, int limit = 10)
        {
            var result = await _useCase.ObterTodasAsync(offset, limit);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<PageResultModel<IEnumerable<RoadMapDto>>>> GetAll()
        {
            var result = await _useCase.ObterTodasAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoadMapDto?>> GetById(int id)
        {
            var item = await _useCase.ObterPorIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<RoadMapDto?>> Post(RoadMapDto dto)
        {
            var created = await _useCase.SalvarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created?.IdCarreira, version = "1.0" }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<RoadMapDto?>> Put(int id, RoadMapDto dto)
        {
            if (id != dto.IdCarreira) return BadRequest();
            var updated = await _useCase.AtualizarAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<RoadMapDto?>> Delete(int id)
        {
            var deleted = await _useCase.DeletarAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
