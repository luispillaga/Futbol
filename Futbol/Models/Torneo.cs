using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Futbol.Models
{
    public class Torneo
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Display(Name = "Fecha Inicio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime Hora { get; set; }


        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20)]
        [DataType(DataType.Text)]
        public string Estado { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        public Imagen Imagen { get; set; }

        [Display(Name = "Imagen")]
        public int ImagenId { get; set; }

        public Direccion Direccion { get; set; }

        [Display(Name = "Dirección")]
        public int DireccionId { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}