using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaUseCase _useCase;
        public CategoriaController(ICategoriaUseCase useCase) => _useCase = useCase;

        [HttpGet]
        public async Task<ActionResult<PageResultModel<IEnumerable<CategoriaDto>>>> GetAll(int offset = 0, int limit = 10)
        {
            var result = await _useCase.ObterTodasAsync(offset, limit);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<PageResultModel<IEnumerable<CategoriaDto>>>> GetAll()
        {
            var result = await _useCase.ObterTodasAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaDto?>> GetById(int id)
        {
            var item = await _useCase.ObterPorIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDto?>> Post(CategoriaDto dto)
        {
            try
            {
                var created = await _useCase.SalvarAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created?.IdCategoria, version = "1.0" }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaDto?>> Put(int id, CategoriaDto dto)
        {
            if (id != dto.IdCategoria) return BadRequest();
            try
            {
                var updated = await _useCase.AtualizarAsync(dto);
                if (updated == null) return NotFound();
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoriaDto?>> Delete(int id)
        {
            var deleted = await _useCase.DeletarAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}

