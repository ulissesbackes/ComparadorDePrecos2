using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComparadorPrecos.Core.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string SenhaHash { get; set; } = string.Empty;

        [Required]
        public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<ListaCompras> ListasCompras { get; set; } = new List<ListaCompras>();
        public virtual ICollection<ItemDesejado> ItensDesejados { get; set; } = new List<ItemDesejado>();
    }
}