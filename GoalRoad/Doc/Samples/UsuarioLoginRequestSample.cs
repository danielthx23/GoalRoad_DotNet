using GoalRoad.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace GoalRoad.Doc.Samples;

public class UsuarioLoginRequestSample  : IExamplesProvider<UsuarioLoginDto>
{
    public UsuarioLoginDto GetExamples()
    {
        return new UsuarioLoginDto
        {
            Email = "joao.silva@example.com",
            Senha = "P@ssw0rd!"
        };
    }
}