namespace GoalRoad.Application.DTOs
{
    public class FeedItemDto
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string? TipoItem { get; set; }
        public int? FonteId { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public string? Url { get; set; }
        public int? IdTecnologia { get; set; }
        public TecnologiaDto? Tecnologia { get; set; }
        public double Relevancia { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}


