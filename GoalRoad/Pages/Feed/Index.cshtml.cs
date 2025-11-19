using Microsoft.AspNetCore.Mvc.RazorPages;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Models;

namespace GoalRoad.Pages.Feed;

public class IndexModel : PageModel
{
    private readonly IFeedUseCase _useCase;
    public PageResultModel<IEnumerable<FeedDto>>? Feeds { get; set; }

    public IndexModel(IFeedUseCase useCase)
    {
        _useCase = useCase;
    }

    public async Task OnGetAsync()
    {
        Feeds = await _useCase.ObterTodasAsync(0, 100);
    }
}
