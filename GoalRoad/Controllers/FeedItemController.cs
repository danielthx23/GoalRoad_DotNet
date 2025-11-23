using Microsoft.AspNetCore.Mvc;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FeedItemController : ControllerBase
    {
        private readonly IFeedItemUseCase _useCase;
        public FeedItemController(IFeedItemUseCase useCase) => _useCase = useCase;

        [HttpGet]
        public async Task<ActionResult<PageResultModel<IEnumerable<FeedItemDto>>>> GetAll(int offset = 0, int limit = 10)
        {
            var result = await _useCase.ObterTodasAsync(offset, limit);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FeedItemDto?>> GetById(int id)
        {
            var item = await _useCase.ObterPorIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<FeedItemDto?>> Post(FeedItemDto dto)
        {
            var created = await _useCase.SalvarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created?.Id, version = "1.0" }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<FeedItemDto?>> Put(int id, FeedItemDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _useCase.AtualizarAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<FeedItemDto?>> Delete(int id)
        {
            var deleted = await _useCase.DeletarAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }
    }
}
