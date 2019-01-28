using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Futbol.ModelViews
{
    public class JornadaPartidoViewModel
    {
        public Jornada Jornada { get; set; }
        public IEnumerable<Partido> Partidos { get; set; }
        public IEnumerable<Equipo> EquiposLocales { get; set; }
        public IEnumerable<Equipo> EquiposVisitantes { get; set; }
    }
}