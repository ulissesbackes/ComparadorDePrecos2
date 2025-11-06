using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComparadorPrecos.Core.Models
{
    public class ListaCompras
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public DateTime CriadaEm { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; } = null!;

        public virtual ICollection<ItemDesejado> ItensDesejados { get; set; } = new List<ItemDesejado>();
    }
}