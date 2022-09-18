using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class TorneoDetailsView
    {       
        public int TorneoId { get; set; }      
        public string Nombre { get; set; }
        public string Categoria { get; set; }
       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }
       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }
       
        public string Municipio { get; set; }
        //public virtual Municipio Municipio { get; set; }

        //relacion con la tabla intermedia TorneoEquipo
        public List<TorneoEquipo> TorneoEquipos { get; set; }
    }
}