using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoalRoad.Domain.Entities
{
    [Table("GR_CATEGORIA")]
    public class CategoriaEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCategoria { get; set; }

        [Required]
        [MaxLength(100)]
        public required string NomeCategoria { get; set; }

        [MaxLength(500)]
        public string? DescricaoCategoria { get; set; }

        public List<CarreiraEntity> Carreiras { get; set; } = new();
    }
}
