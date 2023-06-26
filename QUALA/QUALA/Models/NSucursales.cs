using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QUALA.Models
{
    public class NSucursales
    {
        [Key]
        public int RegistroId { get; set; }

        [Required]
        public int Codigo { get; set; }

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }

        [Required]
        [MaxLength(250)]
        public string Direccion { get; set; }

        [Required]
        [MaxLength(50)]
        public string Identificacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public int MonedaId { get; set; }
        public NMonedas Moneda { get; set; }
    }
}