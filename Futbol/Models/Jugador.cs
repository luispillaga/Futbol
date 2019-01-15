using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Futbol.Models
{
    public class Jugador
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Estado { get; set; }
        public Imagen Foto { get; set; }
        public int ImagenId { get; set; }
        public string Dorsal { get; set; }
        public Equipo Equipo { get; set; }
        public int EquipoId { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
    }
}