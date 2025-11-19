using Microsoft.AspNetCore.Mvc;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Application.DTOs;

namespace GoalRoad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAutenticarUseCase _autenticarUseCase;
        public AuthController(IAutenticarUseCase autenticarUseCase) => _autenticarUseCase = autenticarUseCase;

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UsuarioLoginDto dto)
        {
            var token = await _autenticarUseCase.AutenticarAsync(dto.Email, dto.Senha);
            if (token == null) return Unauthorized();
            return Ok(token);
        }
    }
}
