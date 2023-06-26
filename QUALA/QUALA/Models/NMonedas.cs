using System.ComponentModel.DataAnnotations;

namespace QUALA.Models
{
    public class NMonedas
    {
        [Key]
        public int MonedaId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(10)]
        public string Codigo { get; set; }
    }
}