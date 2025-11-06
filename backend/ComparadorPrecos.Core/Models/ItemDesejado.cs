using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComparadorPrecos.Core.Models
{
    public class ItemDesejado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descricao { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int ListaComprasId { get; set; }

        [Required]
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("ListaComprasId")]
        public virtual ListaCompras ListaCompras { get; set; } = null!;

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; } = null!;

        public virtual ICollection<OpcaoCompra> OpcoesCompra { get; set; } = new List<OpcaoCompra>();
    }
}