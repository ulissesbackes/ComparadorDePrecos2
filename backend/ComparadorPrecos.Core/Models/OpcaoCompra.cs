using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComparadorPrecos.Core.Models
{
    public class OpcaoCompra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ItemDesejadoId { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(300)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("ItemDesejadoId")]
        public virtual ItemDesejado ItemDesejado { get; set; } = null!;

        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; } = null!;
    }
}