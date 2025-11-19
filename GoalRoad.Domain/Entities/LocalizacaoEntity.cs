using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoalRoad.Domain.Entities
{
    [Table("GR_ENDERECO")]
    public class LocalizacaoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEndereco { get; set; }

        [Required]
        [MaxLength(150)]
        public required string Logradouro { get; set; }  

        [Required]
        [MaxLength(20)]
        public required string Numero { get; set; }

        [MaxLength(100)]
        public string? Complemento { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Bairro { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Cidade { get; set; }

        [Required]
        [MaxLength(2)]
        public required string Estado { get; set; }  // Ex: "SP", "RJ"

        [Required]
        [MaxLength(10)]
        public required string CEP { get; set; }

        [MaxLength(100)]
        public required string Referencia { get; set; }  // Ponto de referência (opcional)
    }
}