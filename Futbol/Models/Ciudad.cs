using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Futbol.Models
{
    public class Ciudad
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100)]
        public string Nombre { get; set; }

        public Provincia Provincia { get; set; }

        [Display(Name = "Provincia")]
        public int ProvinciaId { get; set; }
    }
}