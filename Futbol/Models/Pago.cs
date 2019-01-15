using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Futbol.Models
{
    public class Pago
    {
        public int Id { get; set; }

        [Display(Name = "Fecha Pago")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        public TorneoEquipo TorneoEquipo { get; set; }

        public int TorneoEquipoId { get; set; }
    }
}