using Microsoft.AspNetCore.Mvc;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Application.DTOs;
using GoalRoad.Application.Mappers;
using GoalRoad.Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using GoalRoad.Doc.Samples;
using Swashbuckle.AspNetCore.Annotations;

namespace GoalRoad.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FeedController : ControllerBase
    {
        private readonly IFeedUseCase _useCase;
        public FeedController(IFeedUseCase useCase) => _useCase = useCase;

        [HttpGet]
        public async Task<ActionResult<PageResultModel<IEnumerable<FeedDto>>>> GetAll(int offset = 0, int limit = 10)
        {
            var result = await _useCase.ObterTodasAsync(offset, limit);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FeedDto?>> GetById(int id)
        {
            var item = await _useCase.ObterPorIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<FeedDto?>> Post(FeedDto dto)
        {
            var created = await _useCase.SalvarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created?.IdUsuario, version = "1.0" }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<FeedDto?>> Put(int id, FeedDto dto)
        {
            if (id != dto.IdUsuario) return BadRequest();
            var updated = await _useCase.AtualizarAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<FeedDto?>> Delete(int id)
        {
            var deleted = await _useCase.DeletarAsync(id);
            if (deleted == null) return NotFound();
            return Ok(deleted);
        }

        [HttpPost("generate/{userId:int}/{carreiraId:int}")]
        [SwaggerOperation(Summary = "Gera e persiste o feed do usu�rio a partir de um roadmap")]
        [SwaggerResponseExample(statusCode: 200, typeof(FeedGenerateRequestSample))]
        public async Task<ActionResult<FeedDto>> Generate(int userId, int carreiraId, [FromQuery] int top = 20)
        {
            if (carreiraId <= 0 || userId <= 0) return BadRequest("userId and carreiraId must be > 0");

            try
            {
                var feed = await _useCase.GerarESalvarFeedParaUsuarioAsync(userId, carreiraId, top);
                return Ok(feed);
            }
            catch (Exception ex)
            {
                return Problem(title: "Error generating feed", detail: ex.Message);
            }
        }

        [HttpGet("treinar")]
        [SwaggerOperation(Summary = "Treina o modelo de ML.NET para ranking de feed")]
        public async Task<IActionResult> Treinar()
        {
            try
            {
                await _useCase.TreinarModeloAsync();
                return Ok(new { data = "Modelo treinado com sucesso", status = StatusCodes.Status200OK });
            }
            catch (Exception ex)
            {
                return Problem(title: "Erro ao treinar modelo", detail: ex.Message);
            }
        }
    }
}
