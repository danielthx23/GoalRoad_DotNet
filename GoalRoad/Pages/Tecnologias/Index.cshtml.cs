using Microsoft.AspNetCore.Mvc.RazorPages;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Pages.Tecnologias;

public class IndexModel : PageModel
{
    private readonly ITecnologiaUseCase _useCase;
    public PageResultModel<IEnumerable<TecnologiaDto>>? Tecnologias { get; set; }

    public IndexModel(ITecnologiaUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task OnGetAsync()
    {
        Tecnologias = await _useCase.ObterTodasAsync(0, 100);
    }
}
