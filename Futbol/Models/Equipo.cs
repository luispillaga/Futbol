using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Futbol.Models
{
    public class Equipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Representante { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public Imagen Escudo { get; set; }
        public int ImagenId { get; set; }
        public string Estado { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}