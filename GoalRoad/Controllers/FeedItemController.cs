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
    public class FeedItemController : ControllerBase
    {
        private readonly IFeedItemUseCase _useCase;
        public FeedItemController(IFeedItemUseCase useCase) => _useCase = useCase;

        [HttpGet]
        public async Task<ActionResult<PageResultWithLinks<IEnumerable<HateoasResource<FeedItemDto>>>>> GetAll(int offset = 0, int limit = 10)
        {
            var result = await _useCase.ObterTodasAsync(offset, limit);
            var response = new PageResultWithLinks<IEnumerable<HateoasResource<FeedItemDto>>>
            {
                Data = result.Data?.Select(f => CreateHateoasResource(f)),
                Total = result.Total
            };
            
            response.AddLink(Url.Action(nameof(GetAll), new { offset, limit, version = "1.0" }) ?? "", "self", "GET");
            response.AddLink(Url.Action(nameof(Post), new { version = "1.0" }) ?? "", "create", "POST");
            
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<HateoasResource<FeedItemDto>>> GetById(int id)
        {
            var item = await _useCase.ObterPorIdAsync(id);
            if (item == null) return NotFound();
            
            var resource = CreateHateoasResource(item);
            return Ok(resource);
        }

        [HttpPost]
        public async Task<ActionResult<HateoasResource<FeedItemDto>>> Post(FeedItemDto dto)
        {
            var created = await _useCase.SalvarAsync(dto);
            var resource = CreateHateoasResource(created);
            return CreatedAtAction(nameof(GetById), new { id = created?.Id, version = "1.0" }, resource);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<HateoasResource<FeedItemDto>>> Put(int id, FeedItemDto dto)
        {
            if (id != dto.Id) return BadRequest();
            var updated = await _useCase.AtualizarAsync(dto);
            if (updated == null) return NotFound();
            
            var resource = CreateHateoasResource(updated);
            return Ok(resource);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<HateoasResource<FeedItemDto>>> Delete(int id)
        {
            var deleted = await _useCase.DeletarAsync(id);
            if (deleted == null) return NotFound();
            
            var resource = CreateHateoasResource(deleted);
            return Ok(resource);
        }

        private HateoasResource<FeedItemDto> CreateHateoasResource(FeedItemDto? feedItem)
        {
            if (feedItem == null) return new HateoasResource<FeedItemDto>(new FeedItemDto());
            
            var resource = new HateoasResource<FeedItemDto>(feedItem);
            resource.AddLink(Url.Action(nameof(GetById), new { id = feedItem.Id, version = "1.0" }) ?? "", "self", "GET");
            resource.AddLink(Url.Action(nameof(Put), new { id = feedItem.Id, version = "1.0" }) ?? "", "update", "PUT");
            resource.AddLink(Url.Action(nameof(Delete), new { id = feedItem.Id, version = "1.0" }) ?? "", "delete", "DELETE");
            resource.AddLink(Url.Action(nameof(GetAll), new { version = "1.0" }) ?? "", "collection", "GET");
            
            return resource;
        }
    }
}
