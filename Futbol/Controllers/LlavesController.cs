using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Futbol.Models;
using Futbol.ModelViews;

namespace Futbol.Controllers
{
    public class LlavesController : Controller
    {
        private FutbolEntities db = new FutbolEntities();
        private ConfiguracionSingleton conf = ConfiguracionSingleton.GetInstance();

        // GET: Llaves
        public ActionResult ListaLlaves()
        {
            List<SelectListItem> TipoLlave = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Fase de Grupos",
                    Value = "Fase de Grupos"
                },
                new SelectListItem()
                {
                    Text = "Eliminatoria",
                    Value = "Eliminatoria"
                },
            };
            ViewBag.TipoLlave = TipoLlave;
            var ModelView = new LlavesViewModel()
            {
                FasesEliminatorias = db.FaseEliminatoria.Where(fe => fe.torneo_id == conf.configuracion.IdTorneo),
                FasesGrupos = db.FaseGrupo.Where(fg => fg.torneo_id == conf.configuracion.IdTorneo),
            };
            return View(ModelView);
        }

        //GET: CREATE LLAVE
        public ActionResult Create(string TipoLlave)
        {
            if (TipoLlave != null)
            {
                if (TipoLlave == "Fase de Grupos")
                {
                    return RedirectToAction("Create", "FaseGrupos");
                }
                else
                {
                    return RedirectToAction("Create", "FaseEliminatorias");
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}