using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Torneo
    {
        [Key]
        public int TorneoId { get; set; }

        [Required(ErrorMessage ="El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength =4, ErrorMessage ="El campo {0} debe tener entre {2} y {1} caracteres")]
        [Index("IndexNombre", IsUnique =true) ]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]
        public DateTime FechaInicial { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }

        //Llave foranea
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        public int MunicipioId { get; set; }
        public virtual Municipio Municipio { get; set; }

        //relacion con la tabla intermedia TorneoEquipo
        public virtual ICollection<TorneoEquipo> TorneoEquipos { get; set; }


    }
}