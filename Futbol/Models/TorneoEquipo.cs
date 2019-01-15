using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Futbol.Models
{
    public class TorneoEquipo
    {
        public int Id { get; set; }
        public Torneo Torneo { get; set; }
        public int TorneoId { get; set; }
        public Equipo Equipo { get; set; }
        public int EquipoId { get; set; }
    }
}