namespace GoalRoad.Domain.Models
{
    public class PageResultModel<T>
    {
        public T? Data { get; set; }
        public int Total { get; set; }
    }
}
