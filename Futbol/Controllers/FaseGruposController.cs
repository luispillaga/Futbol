using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Futbol;
using Futbol.Models;
using Futbol.ModelViews;

namespace Futbol.Controllers
{
    public class FaseGruposController : Controller
    {
        private FutbolEntities db = new FutbolEntities();
        private ConfiguracionSingleton conf = ConfiguracionSingleton.GetInstance();
        // GET: FaseGrupos
        public ActionResult Index()
        {
            return View(db.FaseGrupo.ToList());
        }

        // GET: FaseGrupos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaseGrupo faseGrupo = db.FaseGrupo.Find(id);
            if (faseGrupo == null)
            {
                return HttpNotFound();
            }
            var ListaGrupos = db.Grupo.Where(g => g.faseg_id == id);
            var ViewModel = new FaseGruposViewModel()
            {
                FaseGrupo = faseGrupo,
                ListaGrupos = ListaGrupos
            };
            return View(ViewModel);
        }

        // GET: FaseGrupos/Create
        public ActionResult Create()
        {
            ViewBag.faseg_estado = conf.configuracion.EstadosLlave;
            return View();
        }

        // POST: FaseGrupos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "faseg_id,faseg_nombre,faseg_fecha,faseg_numero_grupos,faseg_al_mejor_de,faseg_estado")] FaseGrupo faseGrupo)
        {
            if (ModelState.IsValid)
            {
                faseGrupo.torneo_id = conf.configuracion.IdTorneo;
                faseGrupo.faseg_estado = "Configurando";
                db.FaseGrupo.Add(faseGrupo);
                for (int i = 0; i < faseGrupo.faseg_numero_grupos; i++)
                {
                    Grupo Grupo = new Grupo()
                    {
                        FaseGrupo = faseGrupo,
                        grupo_nombre = conf.configuracion.NombreGrupoDefault[i]
                    };
                    db.Grupo.Add(Grupo);
                }
                db.SaveChanges();
                return RedirectToAction("ListaLlaves", "Llaves");
            }

            return View(faseGrupo);
        }

        // GET: FaseGrupos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaseGrupo faseGrupo = db.FaseGrupo.Find(id);
            if (faseGrupo == null)
            {
                return HttpNotFound();
            }
            return View(faseGrupo);

        }

        // POST: FaseGrupos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "faseg_id,faseg_nombre,faseg_fecha,faseg_numero_grupos,faseg_al_mejor_de,faseg_estado")] FaseGrupo faseGrupo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faseGrupo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faseGrupo);
        }

        // GET: FaseGrupos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaseGrupo faseGrupo = db.FaseGrupo.Find(id);
            if (faseGrupo == null)
            {
                return HttpNotFound();
            }
            return View(faseGrupo);
        }

        // POST: FaseGrupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FaseGrupo faseGrupo = db.FaseGrupo.Find(id);
            db.FaseGrupo.Remove(faseGrupo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
