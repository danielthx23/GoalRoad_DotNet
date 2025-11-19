using Microsoft.AspNetCore.Mvc.RazorPages;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Pages.Carreiras;

public class IndexModel : PageModel
{
    private readonly ICarreiraUseCase _useCase;
    public PageResultModel<IEnumerable<CarreiraDto>>? Carreiras { get; set; }

    public IndexModel(ICarreiraUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task OnGetAsync()
    {
        Carreiras = await _useCase.ObterTodasAsync(0, 100);
    }
}
