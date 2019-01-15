using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Futbol.Models
{
    public class Direccion
    {
        public int Id { get; set; }

        [Display(Name = "Calle")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100)]
        public string Calle { get; set; }

        [Display(Name = "Referencia")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string Referencia { get; set; }
        public Ciudad Ciudad { get; set; }

        [Display(Name = "Ciudad")]
        public int CiudadId { get; set; }
    }
}