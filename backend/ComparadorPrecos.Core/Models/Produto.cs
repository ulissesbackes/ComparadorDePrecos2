using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComparadorPrecos.Core.Models
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Marca { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecoAtual { get; set; }

        [Required]
        [StringLength(100)]
        public string Mercado { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Url { get; set; } = string.Empty;

        [StringLength(500)]
        public string? UrlImagem { get; set; }

        [Required]
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<OpcaoCompra> OpcoesCompra { get; set; } = new List<OpcaoCompra>();
    }
}