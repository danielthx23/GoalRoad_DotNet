using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoalRoad.Domain.Entities
{
    [Table("GR_CARREIRA")]
    public class CarreiraEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCarreira { get; set; }

        [Required]
        [MaxLength(50)]
        public required string TituloCarreira { get; set; }

        [Required]
        [MaxLength(500)]
        public required string DescricaoCarreira { get; set; }

        [MaxLength(150)]
        public string? LogoCarreira { get; set; }

        public int? IdCategoria { get; set; }
        [ForeignKey(nameof(IdCategoria))]
        public CategoriaEntity? Categoria { get; set; }

        public RoadMapEntity? RoadMap { get; set; }
    }
}