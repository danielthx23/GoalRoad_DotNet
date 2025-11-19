using System.Collections.Generic;

namespace GoalRoad.Application.DTOs
{
    public class CarreiraDto
    {
        public int IdCarreira { get; set; }
        public string TituloCarreira { get; set; } = string.Empty;
        public string DescricaoCarreira { get; set; } = string.Empty;
        public string? LogoCarreira { get; set; }
        public int? IdCategoria { get; set; }
        public CategoriaDto? Categoria { get; set; }
        public RoadMapDto? RoadMap { get; set; }
    }
}