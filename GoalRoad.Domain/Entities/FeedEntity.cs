using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GoalRoad.Domain.Entities
{
    [Table("GR_FEED")]
    public class FeedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        public UsuarioEntity? Usuario { get; set; }
        public List<FeedItemEntity> Items { get; set; } = new();
    }
}
