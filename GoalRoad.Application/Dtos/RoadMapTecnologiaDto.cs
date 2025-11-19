namespace GoalRoad.Application.DTOs
{
    public class RoadMapTecnologiaDto
    {
        public int IdRoadMap { get; set; }
        public int IdTecnologia { get; set; }
        public int StepOrder { get; set; }
        public TecnologiaDto? Tecnologia { get; set; }
    }
}