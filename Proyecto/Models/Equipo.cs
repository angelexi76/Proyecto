using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Equipo
    {
        [Key]
        public int EquipoId { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        [Index("IndexNombre", IsUnique = true)]
        public string Nombre { get; set; }

        
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres")]
        public string Deporte { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }

        //Relacion con patrocinador
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        public int PatrocinadorId { get; set; }
        public virtual Patrocinador Patrocinador { get; set; }

        //Relacion uno a uno con Entrenador
        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        public int EntrenadorId { get; set; }
        public virtual Entrenador Entrenador { get; set; }

        //Relacion con la tabla intermedia TorneoEquipo
        public virtual ICollection<TorneoEquipo> TorneoEquipos { get; set; }



    }
}