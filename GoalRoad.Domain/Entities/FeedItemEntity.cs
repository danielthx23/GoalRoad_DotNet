using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoalRoad.Domain.Entities
{
    [Table("GR_FEED_ITEM")]
    public class FeedItemEntity
    {
        [Key]
        public int Id { get; set; }

        public int IdUsuario { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        public FeedEntity? Feed { get; set; }

        [MaxLength(50)]
        public string? TipoItem { get; set; }

        public int? FonteId { get; set; }

        [MaxLength(200)]
        public string? Titulo { get; set; }

        [MaxLength(1000)]
        public string? Descricao { get; set; }

        [MaxLength(500)]
        public string? Url { get; set; }

        public int? IdTecnologia { get; set; }
        [ForeignKey(nameof(IdTecnologia))]
        public TecnologiaEntity? Tecnologia { get; set; }

        public double Relevancia { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}
