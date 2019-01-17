using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Futbol.Models
{
    public class Configuracion
    {
        public int? IdTorneo { get; set; }
        public int? IdTorneoCliente { get; set; }
        public ICollection<Torneo> Torneos { get; set; }
    }
}