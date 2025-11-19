using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoalRoad.Domain.Entities
{
    [Table("GR_TECNOLOGIA")]
    public class TecnologiaEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTecnologia { get; set; }

        [Required]
        [MaxLength(100)]
        public required string NomeTecnologia { get; set; }

        [MaxLength(1000)]
        public string? DescricaoTecnologia { get; set; }

        [MaxLength(150)]
        public string? LogoTecnologia { get; set; }

        public List<FeedItemEntity> FeedItems { get; set; } = new();
    }
}
