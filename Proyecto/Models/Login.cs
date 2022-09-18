using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Login
    {
        [Key]
        public int LoginId { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
       // [Index("IndexUsuario", IsUnique = true)]
        public string Usuario { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

    }
}