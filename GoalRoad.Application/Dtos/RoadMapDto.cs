using System.Collections.Generic;

namespace GoalRoad.Application.DTOs
{
    public class RoadMapDto
    {
        public int IdCarreira { get; set; }
        public List<RoadMapTecnologiaDto> Tecnologias { get; set; } = new();
    }
}