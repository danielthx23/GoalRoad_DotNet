namespace GoalRoad.Domain.Models
{
    public class HateoasResource<T>
    {
        public T Data { get; set; }
        public List<Link> Links { get; set; } = new List<Link>();

        public HateoasResource(T data)
        {
            Data = data;
        }

        public void AddLink(string href, string rel, string method)
        {
            Links.Add(new Link(href, rel, method));
        }
    }
}

