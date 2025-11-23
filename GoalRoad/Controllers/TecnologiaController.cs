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
    public class TecnologiaController : ControllerBase
    {
        private readonly ITecnologiaUseCase _useCase;
        public TecnologiaController(ITecnologiaUseCase useCase) => _useCase = useCase;

        [HttpGet]
        public async Task<ActionResult<PageResultWithLinks<IEnumerable<HateoasResource<TecnologiaDto>>>>> GetAll(int offset = 0, int limit = 10)
        {
            var result = await _useCase.ObterTodasAsync(offset, limit);
            var response = new PageResultWithLinks<IEnumerable<HateoasResource<TecnologiaDto>>>
            {
                Data = result.Data?.Select(t => CreateHateoasResource(t)),
                Total = result.Total
            };
            
            response.AddLink(Url.Action(nameof(GetAll), new { offset, limit, version = "1.0" }) ?? "", "self", "GET");
            response.AddLink(Url.Action(nameof(GetAll), new { version = "1.0" }) ?? "", "all", "GET");
            response.AddLink(Url.Action(nameof(Post), new { version = "1.0" }) ?? "", "create", "POST");
            
            return Ok(response);
        }

        [HttpGet("all")]
        public async Task<ActionResult<PageResultWithLinks<IEnumerable<HateoasResource<TecnologiaDto>>>>> GetAll()
        {
            var result = await _useCase.ObterTodasAsync();
            var response = new PageResultWithLinks<IEnumerable<HateoasResource<TecnologiaDto>>>
            {
                Data = result.Data?.Select(t => CreateHateoasResource(t)),
                Total = result.Total
            };
            
            response.AddLink(Url.Action(nameof(GetAll), new { version = "1.0" }) ?? "", "self", "GET");
            response.AddLink(Url.Action(nameof(Post), new { version = "1.0" }) ?? "", "create", "POST");
            
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<HateoasResource<TecnologiaDto>>> GetById(int id)
        {
            var item = await _useCase.ObterPorIdAsync(id);
            if (item == null) return NotFound();
            
            var resource = CreateHateoasResource(item);
            return Ok(resource);
        }

        [HttpPost]
        public async Task<ActionResult<HateoasResource<TecnologiaDto>>> Post(TecnologiaDto dto)
        {
            var created = await _useCase.SalvarAsync(dto);
            var resource = CreateHateoasResource(created);
            return CreatedAtAction(nameof(GetById), new { id = created?.IdTecnologia, version = "1.0" }, resource);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<HateoasResource<TecnologiaDto>>> Put(int id, TecnologiaDto dto)
        {
            if (id != dto.IdTecnologia) return BadRequest();
            var updated = await _useCase.AtualizarAsync(dto);
            if (updated == null) return NotFound();
            
            var resource = CreateHateoasResource(updated);
            return Ok(resource);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<HateoasResource<TecnologiaDto>>> Delete(int id)
        {
            var deleted = await _useCase.DeletarAsync(id);
            if (deleted == null) return NotFound();
            
            var resource = CreateHateoasResource(deleted);
            return Ok(resource);
        }

        private HateoasResource<TecnologiaDto> CreateHateoasResource(TecnologiaDto? tecnologia)
        {
            if (tecnologia == null) return new HateoasResource<TecnologiaDto>(new TecnologiaDto());
            
            var resource = new HateoasResource<TecnologiaDto>(tecnologia);
            resource.AddLink(Url.Action(nameof(GetById), new { id = tecnologia.IdTecnologia, version = "1.0" }) ?? "", "self", "GET");
            resource.AddLink(Url.Action(nameof(Put), new { id = tecnologia.IdTecnologia, version = "1.0" }) ?? "", "update", "PUT");
            resource.AddLink(Url.Action(nameof(Delete), new { id = tecnologia.IdTecnologia, version = "1.0" }) ?? "", "delete", "DELETE");
            resource.AddLink(Url.Action(nameof(GetAll), new { version = "1.0" }) ?? "", "collection", "GET");
            
            return resource;
        }
    }
}
