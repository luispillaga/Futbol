using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Futbol.ModelViews
{
    public class LlavesViewModel
    {
        public IEnumerable<FaseEliminatoria> FasesEliminatorias { get; set; }
        public IEnumerable<FaseGrupo> FasesGrupos { get; set; }
    }
}