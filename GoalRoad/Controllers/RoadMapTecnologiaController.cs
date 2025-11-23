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
    public class RoadMapTecnologiaController : ControllerBase
    {
        private readonly IRoadMapTecnologiaUseCase _useCase;
        public RoadMapTecnologiaController(IRoadMapTecnologiaUseCase useCase) => _useCase = useCase;

        [HttpGet]
        public async Task<ActionResult<PageResultWithLinks<IEnumerable<HateoasResource<RoadMapTecnologiaDto>>>>> GetAll(int offset = 0, int limit = 10)
        {
            var result = await _useCase.ObterTodasAsync(offset, limit);
            var response = new PageResultWithLinks<IEnumerable<HateoasResource<RoadMapTecnologiaDto>>>
            {
                Data = result.Data?.Select(r => CreateHateoasResource(r)),
                Total = result.Total
            };
            
            response.AddLink(Url.Action(nameof(GetAll), new { offset, limit, version = "1.0" }) ?? "", "self", "GET");
            response.AddLink(Url.Action(nameof(Post), new { version = "1.0" }) ?? "", "create", "POST");
            
            return Ok(response);
        }

        [HttpGet("{id1:int}/{id2:int}")]
        public async Task<ActionResult<HateoasResource<RoadMapTecnologiaDto>>> GetById(int id1, int id2)
        {
            var item = await _useCase.ObterPorIdAsync(id1, id2);
            if (item == null) return NotFound();
            
            var resource = CreateHateoasResource(item);
            return Ok(resource);
        }

        [HttpPost]
        public async Task<ActionResult<HateoasResource<RoadMapTecnologiaDto>>> Post(RoadMapTecnologiaDto dto)
        {
            var created = await _useCase.SalvarAsync(dto);
            var resource = CreateHateoasResource(created);
            return CreatedAtAction(nameof(GetById), new { id1 = created?.IdRoadMap, id2 = created?.IdTecnologia, version = "1.0" }, resource);
        }

        [HttpPut("{id1:int}/{id2:int}")]
        public async Task<ActionResult<HateoasResource<RoadMapTecnologiaDto>>> Put(int id1, int id2, RoadMapTecnologiaDto dto)
        {
            if (id1 != dto.IdRoadMap || id2 != dto.IdTecnologia) return BadRequest();
            var updated = await _useCase.AtualizarAsync(dto);
            if (updated == null) return NotFound();
            
            var resource = CreateHateoasResource(updated);
            return Ok(resource);
        }

        [HttpDelete("{id1:int}/{id2:int}")]
        public async Task<ActionResult<HateoasResource<RoadMapTecnologiaDto>>> Delete(int id1, int id2)
        {
            var deleted = await _useCase.DeletarAsync(id1, id2);
            if (deleted == null) return NotFound();
            
            var resource = CreateHateoasResource(deleted);
            return Ok(resource);
        }

        private HateoasResource<RoadMapTecnologiaDto> CreateHateoasResource(RoadMapTecnologiaDto? roadMapTecnologia)
        {
            if (roadMapTecnologia == null) return new HateoasResource<RoadMapTecnologiaDto>(new RoadMapTecnologiaDto());
            
            var resource = new HateoasResource<RoadMapTecnologiaDto>(roadMapTecnologia);
            resource.AddLink(Url.Action(nameof(GetById), new { id1 = roadMapTecnologia.IdRoadMap, id2 = roadMapTecnologia.IdTecnologia, version = "1.0" }) ?? "", "self", "GET");
            resource.AddLink(Url.Action(nameof(Put), new { id1 = roadMapTecnologia.IdRoadMap, id2 = roadMapTecnologia.IdTecnologia, version = "1.0" }) ?? "", "update", "PUT");
            resource.AddLink(Url.Action(nameof(Delete), new { id1 = roadMapTecnologia.IdRoadMap, id2 = roadMapTecnologia.IdTecnologia, version = "1.0" }) ?? "", "delete", "DELETE");
            resource.AddLink(Url.Action(nameof(GetAll), new { version = "1.0" }) ?? "", "collection", "GET");
            
            return resource;
        }
    }
}
