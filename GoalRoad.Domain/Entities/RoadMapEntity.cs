using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoalRoad.Domain.Entities
{
    [Table("GR_ROADMAP")]
    public class RoadMapEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCarreira { get; set; }

        [ForeignKey(nameof(IdCarreira))]
        public CarreiraEntity? Carreira { get; set; }

        public List<RoadMapTecnologiaEntity> Tecnologias { get; set; } = new();
    }
}