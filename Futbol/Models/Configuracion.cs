using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Futbol.Models
{
    public class Configuracion
    {
        public int? IdTorneo { get; set; }
        public int? IdTorneoCliente { get; set; }
        public ICollection<Torneo> Torneos { get; set; }
        public List<SelectListItem> EstadosTorneo { get; set; }
        public List<SelectListItem> EstadosEquipo  { get; set; }
        public List<SelectListItem> EstadosJugador { get; set; }
        public bool IsTorneoActive { get; set; }
    }
}