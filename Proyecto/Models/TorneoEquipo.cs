using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class TorneoEquipo
    {
        [Key]
        public int TorneoEquipoId { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        public int TorneoId { get; set; }

        [Required(ErrorMessage = "El campo {0}, es obligatorio")]
        public int EquipoId { get; set; }

        public virtual Equipo Equipo { get; set; }
        public virtual Torneo Torneo {get;set;}

    }
}