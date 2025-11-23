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
        public async Task<ActionResult<PageResultWithLinks<IEnumerable<HateoasResource<UsuarioDto>>>>> GetAll(int offset = 0, int limit = 10)
        {
            var result = await _useCase.ObterTodasAsync(offset, limit);
            var response = new PageResultWithLinks<IEnumerable<HateoasResource<UsuarioDto>>>
            {
                Data = result.Data?.Select(u => CreateHateoasResource(u)),
                Total = result.Total
            };
            
            response.AddLink(Url.Action(nameof(GetAll), new { offset, limit, version = "1.0" }) ?? "", "self", "GET");
            response.AddLink(Url.Action(nameof(GetAll), new { version = "1.0" }) ?? "", "all", "GET");
            response.AddLink(Url.Action(nameof(Post), new { version = "1.0" }) ?? "", "create", "POST");
            
            return Ok(response);
        }

        [HttpGet("all")]
        public async Task<ActionResult<PageResultWithLinks<IEnumerable<HateoasResource<UsuarioDto>>>>> GetAll()
        {
            var result = await _useCase.ObterTodasAsync();
            var response = new PageResultWithLinks<IEnumerable<HateoasResource<UsuarioDto>>>
            {
                Data = result.Data?.Select(u => CreateHateoasResource(u)),
                Total = result.Total
            };
            
            response.AddLink(Url.Action(nameof(GetAll), new { version = "1.0" }) ?? "", "self", "GET");
            response.AddLink(Url.Action(nameof(Post), new { version = "1.0" }) ?? "", "create", "POST");
            
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<HateoasResource<UsuarioDto>>> GetById(int id)
        {
            var item = await _useCase.ObterPorIdAsync(id);
            if (item == null) return NotFound();
            
            var resource = CreateHateoasResource(item);
            return Ok(resource);
        }

        [HttpPost]
        [AllowAnonymous]
        [SwaggerRequestExample(typeof(UsuarioDto), typeof(UsuarioRequestSample))]
        public async Task<ActionResult<HateoasResource<UsuarioDto>>> Post(UsuarioDto dto)
        {
            var created = await _useCase.SalvarAsync(dto);
            if (created == null) return BadRequest();
            var resource = CreateHateoasResource(created);
            return CreatedAtAction(nameof(GetById), new { id = created.IdUsuario, version = "1.0" }, resource);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<HateoasResource<UsuarioDto>>> Put(int id, UsuarioDto dto)
        {
            dto.IdUsuario = id;
            var updated = await _useCase.AtualizarAsync(dto);
            if (updated == null) return NotFound();
            
            var resource = CreateHateoasResource(updated);
            return Ok(resource);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<HateoasResource<UsuarioDto>>> Delete(int id)
        {
            var deleted = await _useCase.DeletarAsync(id);
            if (deleted == null) return NotFound();
            
            var resource = CreateHateoasResource(deleted);
            return Ok(resource);
        }

        private HateoasResource<UsuarioDto> CreateHateoasResource(UsuarioDto? usuario)
        {
            if (usuario == null) return new HateoasResource<UsuarioDto>(new UsuarioDto { NomeUsuario = "", SenhaUsuario = "", EmailUsuario = "" });
            
            var resource = new HateoasResource<UsuarioDto>(usuario);
            resource.AddLink(Url.Action(nameof(GetById), new { id = usuario.IdUsuario, version = "1.0" }) ?? "", "self", "GET");
            resource.AddLink(Url.Action(nameof(Put), new { id = usuario.IdUsuario, version = "1.0" }) ?? "", "update", "PUT");
            resource.AddLink(Url.Action(nameof(Delete), new { id = usuario.IdUsuario, version = "1.0" }) ?? "", "delete", "DELETE");
            resource.AddLink(Url.Action(nameof(GetAll), new { version = "1.0" }) ?? "", "collection", "GET");
            
            return resource;
        }
    }
}
