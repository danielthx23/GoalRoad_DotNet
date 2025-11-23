using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using GoalRoad.Doc.Samples;

namespace GoalRoad.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioUseCase _useCase;
        public UsuarioController(IUsuarioUseCase useCase) => _useCase = useCase;

        [HttpGet]
        public async Task<ActionResult<PageResultModel<IEnumerable<UsuarioDto>>>> GetAll(int offset = 0, int limit = 10)
        {
            var result = await _useCase.ObterTodasAsync(offset, limit);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<PageResultModel<IEnumerable<UsuarioDto>>>> GetAll()
        {
            var result = await _useCase.ObterTodasAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioDto?>> GetById(int id)
        {
            var item = await _useCase.ObterPorIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [AllowAnonymous]
        [SwaggerRequestExample(typeof(UsuarioDto), typeof(UsuarioRequestSample))]
        public async Task<ActionResult<UsuarioDto?>> Post(UsuarioDto dto)
        {
            var created = await _useCase.SalvarAsync(dto);
            if (created == null) return BadRequest();
            return CreatedAtAction(nameof(GetById), new { id = created.IdUsuario, version = "1.0" }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UsuarioDto?>> Put(int id, UsuarioDto dto)
        {
            dto.IdUsuario = id;
            var updated = await _useCase.AtualizarAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UsuarioDto?>> Delete(int id)
        {
            var deleted = await _useCase.DeletarAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
