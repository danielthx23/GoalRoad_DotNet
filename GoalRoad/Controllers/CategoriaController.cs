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
        public async Task<ActionResult<PageResultWithLinks<IEnumerable<HateoasResource<CategoriaDto>>>>> GetAll(int offset = 0, int limit = 10)
        {
            var result = await _useCase.ObterTodasAsync(offset, limit);
            var response = new PageResultWithLinks<IEnumerable<HateoasResource<CategoriaDto>>>
            {
                Data = result.Data?.Select(c => CreateHateoasResource(c)),
                Total = result.Total
            };
            
            response.AddLink(Url.Action(nameof(GetAll), new { offset, limit, version = "1.0" }) ?? "", "self", "GET");
            response.AddLink(Url.Action(nameof(GetAll), new { version = "1.0" }) ?? "", "all", "GET");
            response.AddLink(Url.Action(nameof(Post), new { version = "1.0" }) ?? "", "create", "POST");
            
            return Ok(response);
        }

        [HttpGet("all")]
        public async Task<ActionResult<PageResultWithLinks<IEnumerable<HateoasResource<CategoriaDto>>>>> GetAll()
        {
            var result = await _useCase.ObterTodasAsync();
            var response = new PageResultWithLinks<IEnumerable<HateoasResource<CategoriaDto>>>
            {
                Data = result.Data?.Select(c => CreateHateoasResource(c)),
                Total = result.Total
            };
            
            response.AddLink(Url.Action(nameof(GetAll), new { version = "1.0" }) ?? "", "self", "GET");
            response.AddLink(Url.Action(nameof(Post), new { version = "1.0" }) ?? "", "create", "POST");
            
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<HateoasResource<CategoriaDto>>> GetById(int id)
        {
            var item = await _useCase.ObterPorIdAsync(id);
            if (item == null) return NotFound();
            
            var resource = CreateHateoasResource(item);
            return Ok(resource);
        }

        [HttpPost]
        public async Task<ActionResult<HateoasResource<CategoriaDto>>> Post(CategoriaDto dto)
        {
            try
            {
                var created = await _useCase.SalvarAsync(dto);
                var resource = CreateHateoasResource(created);
                return CreatedAtAction(nameof(GetById), new { id = created?.IdCategoria, version = "1.0" }, resource);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<HateoasResource<CategoriaDto>>> Put(int id, CategoriaDto dto)
        {
            if (id != dto.IdCategoria) return BadRequest();
            try
            {
                var updated = await _useCase.AtualizarAsync(dto);
                if (updated == null) return NotFound();
                
                var resource = CreateHateoasResource(updated);
                return Ok(resource);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<HateoasResource<CategoriaDto>>> Delete(int id)
        {
            var deleted = await _useCase.DeletarAsync(id);
            if (deleted == null) return NotFound();
            
            var resource = CreateHateoasResource(deleted);
            return Ok(resource);
        }

        private HateoasResource<CategoriaDto> CreateHateoasResource(CategoriaDto? categoria)
        {
            if (categoria == null) return new HateoasResource<CategoriaDto>(new CategoriaDto());
            
            var resource = new HateoasResource<CategoriaDto>(categoria);
            resource.AddLink(Url.Action(nameof(GetById), new { id = categoria.IdCategoria, version = "1.0" }) ?? "", "self", "GET");
            resource.AddLink(Url.Action(nameof(Put), new { id = categoria.IdCategoria, version = "1.0" }) ?? "", "update", "PUT");
            resource.AddLink(Url.Action(nameof(Delete), new { id = categoria.IdCategoria, version = "1.0" }) ?? "", "delete", "DELETE");
            resource.AddLink(Url.Action(nameof(GetAll), new { version = "1.0" }) ?? "", "collection", "GET");
            
            return resource;
        }
    }
}

