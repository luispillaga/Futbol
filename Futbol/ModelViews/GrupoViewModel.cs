using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Futbol.ModelViews
{
    public class GrupoViewModel
    {
        public Grupo Grupo { get; set; }
        public IEnumerable<Jornada> Jornadas { get; set; }
        public IEnumerable<Estadistica> EquiposEstadisticas { get; set; }
        public int? grupo_id { get; set; }
        public IEnumerable<TorneoEquipo> TorneosEquipos { get; set; }
    }
}