using Microsoft.AspNetCore.Mvc.RazorPages;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Pages.Usuarios;

public class IndexModel : PageModel
{
    private readonly IUsuarioUseCase _useCase;
    public PageResultModel<IEnumerable<UsuarioDto>>? Usuarios { get; set; }

    public IndexModel(IUsuarioUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task OnGetAsync()
    {
        Usuarios = await _useCase.ObterTodasAsync(0, 100);
    }
}
