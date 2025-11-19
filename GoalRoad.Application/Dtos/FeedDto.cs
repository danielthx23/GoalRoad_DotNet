namespace GoalRoad.Application.DTOs
{
    public class FeedDto
    {
        public int IdUsuario { get; set; }
        public List<FeedItemDto> Items { get; set; } = new();
    }
}


