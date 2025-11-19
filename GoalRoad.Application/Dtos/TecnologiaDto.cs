namespace GoalRoad.Application.DTOs
{
    public class TecnologiaDto
    {
        public int IdTecnologia { get; set; }
        public string NomeTecnologia { get; set; } = string.Empty;
        public string? DescricaoTecnologia { get; set; }
        public string? LogoTecnologia { get; set; }
    }
}