using Microsoft.AspNetCore.Mvc.RazorPages;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Pages.Categorias;

public class IndexModel : PageModel
{
    private readonly ICategoriaUseCase _useCase;
    public PageResultModel<IEnumerable<CategoriaDto>>? Categorias { get; set; }

    public IndexModel(ICategoriaUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task OnGetAsync()
    {
        Categorias = await _useCase.ObterTodasAsync(0, 100);
    }
}
