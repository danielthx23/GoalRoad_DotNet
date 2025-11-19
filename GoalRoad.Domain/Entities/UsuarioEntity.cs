using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoalRoad.Domain.Entities
{
    [Table("GR_USUARIO")]
    public class UsuarioEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        [Required]
        public required string NomeUsuario { get; set; }

        [Required]
        public required string SenhaUsuario { get; set; }

        [Required]
        [EmailAddress]
        public required string EmailUsuario { get; set; }

        public required string TokenUsuario { get; set; }

        public DateTime CriadoEmUsuario { get; set; }

        // Navigation to localizacao (optional)
        public int? IdEndereco { get; set; }

        [ForeignKey(nameof(IdEndereco))]
        public LocalizacaoEntity? Localizacao { get; set; }

        // Navigation to the user's personalized feed (one-to-one)
        public FeedEntity? Feed { get; set; }
    }
}