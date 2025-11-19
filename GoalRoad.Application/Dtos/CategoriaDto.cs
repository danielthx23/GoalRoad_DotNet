namespace GoalRoad.Application.DTOs
{
    public class CategoriaDto
    {
        public int IdCategoria { get; set; }
        public string NomeCategoria { get; set; } = string.Empty;
        public string? DescricaoCategoria { get; set; }
    }
}