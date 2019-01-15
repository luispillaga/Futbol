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
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime Hora { get; set; }

        public string Estado { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public Imagen Imagen { get; set; }
        public int ImagenId { get; set; }
        public Direccion Direccion { get; set; }
        public int DireccionId { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}