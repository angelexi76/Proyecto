using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Patrocinador
    {
        [Key]
        public int PatrocinadorId { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 11, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [Index("IndexIdentificacion", IsUnique = true)]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(40, MinimumLength = 10, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [Range(0, 9999999999)]

        //[DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string TipoPersona { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        //Relacion
        public virtual ICollection<Equipo> Equipos { get; set; }
    }
}