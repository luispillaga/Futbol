using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Futbol.Models
{
    public class Direccion
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Referencia { get; set; }
        public Ciudad Ciudad { get; set; }
        public int CiudadId { get; set; }
    }
}