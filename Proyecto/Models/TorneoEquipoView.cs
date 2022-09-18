using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class TorneoEquipoView
    {
        public int TorneoId { get; set; }

        [Required(ErrorMessage = "Est es un campo obligatorio")]
        public int EquipoId { get; set; }
        
        
    }
}