using System.ComponentModel.DataAnnotations.Schema;

namespace GoalRoad.Domain.Entities
{
    [Table("GR_ROADMAP_TECNOLOGIA")]
    public class RoadMapTecnologiaEntity
    {
        public int IdRoadMap { get; set; }
        [ForeignKey(nameof(IdRoadMap))]
        public RoadMapEntity? RoadMap { get; set; }

        public int IdTecnologia { get; set; }
        [ForeignKey(nameof(IdTecnologia))]
        public TecnologiaEntity? Tecnologia { get; set; }

        public int StepOrder { get; set; }
    }
}
