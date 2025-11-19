using Swashbuckle.AspNetCore.Filters;

namespace GoalRoad.Doc.Samples
{
    public class FeedGenerateRequestSample : IExamplesProvider<object>
    {
        public object GetExamples()
        {
            return new { userId = 1, carreiraId = 1, top = 20 };
        }
    }
}
