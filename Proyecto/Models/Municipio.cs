using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Municipio
    {
        [Key]
        public int MunicipioId { get; set; } //llave primaria autoincrementable (identity)

        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [StringLength(30, MinimumLength =4, ErrorMessage =" El campo {0} debe tener entre {2} y {1} caractesres")]
        [Index("IndexNombre", IsUnique=true )]
        public string Nombre { get; set; }

        //Configurar la relación con Torneo
        public virtual ICollection<Torneo> Torneos { get; set; }
    }
}