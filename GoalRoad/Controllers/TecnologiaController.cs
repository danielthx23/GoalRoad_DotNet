using Microsoft.AspNetCore.Mvc;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TecnologiaController : ControllerBase
    {
        private readonly ITecnologiaUseCase _useCase;
        public TecnologiaController(ITecnologiaUseCase useCase) => _useCase = useCase;

        [HttpGet]
        public async Task<ActionResult<PageResultModel<IEnumerable<TecnologiaDto>>>> GetAll(int offset = 0, int limit = 10)
        {
            var result = await _useCase.ObterTodasAsync(offset, limit);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<PageResultModel<IEnumerable<TecnologiaDto>>>> GetAll()
        {
            var result = await _useCase.ObterTodasAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TecnologiaDto?>> GetById(int id)
        {
            var item = await _useCase.ObterPorIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<TecnologiaDto?>> Post(TecnologiaDto dto)
        {
            var created = await _useCase.SalvarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created?.IdTecnologia }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TecnologiaDto?>> Put(int id, TecnologiaDto dto)
        {
            if (id != dto.IdTecnologia) return BadRequest();
            var updated = await _useCase.AtualizarAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<TecnologiaDto?>> Delete(int id)
        {
            var deleted = await _useCase.DeletarAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
