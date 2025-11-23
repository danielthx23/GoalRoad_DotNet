namespace GoalRoad.Domain.Models
{
    public class PageResultWithLinks<T>
    {
        public T? Data { get; set; }
        public int Total { get; set; }
        public List<Link> Links { get; set; } = new List<Link>();

        public void AddLink(string href, string rel, string method)
        {
            Links.Add(new Link(href, rel, method));
        }
    }
}

