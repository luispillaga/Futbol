using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Futbol.Models
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Provincia Provincia { get; set; }
        public int ProvinciaId { get; set; }
    }
}