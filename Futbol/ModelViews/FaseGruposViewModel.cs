using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Futbol.ModelViews
{
    public class FaseGruposViewModel
    {
        public FaseGrupo FaseGrupo { get; set; }
        public IEnumerable<Grupo> ListaGrupos { get; set; }
    }
}