using Microsoft.AspNetCore.Mvc;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarreiraController : ControllerBase
    {
        private readonly ICarreiraUseCase _useCase;
        public CarreiraController(ICarreiraUseCase useCase) => _useCase = useCase;

        [HttpGet]
        public async Task<ActionResult<PageResultModel<IEnumerable<CarreiraDto>>>> GetAll(int offset = 0, int limit = 10)
        {
            var result = await _useCase.ObterTodasAsync(offset, limit);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<PageResultModel<IEnumerable<CarreiraDto>>>> GetAll()
        {
            var result = await _useCase.ObterTodasAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarreiraDto?>> GetById(int id)
        {
            var item = await _useCase.ObterPorIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<CarreiraDto?>> Post(CarreiraDto dto)
        {
            var created = await _useCase.SalvarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created?.IdCarreira }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CarreiraDto?>> Put(int id, CarreiraDto dto)
        {
            if (id != dto.IdCarreira) return BadRequest();
            var updated = await _useCase.AtualizarAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CarreiraDto?>> Delete(int id)
        {
            var deleted = await _useCase.DeletarAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
