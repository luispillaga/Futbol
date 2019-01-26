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

namespace Futbol.Controllers
{
    public class FaseEliminatoriasController : Controller
    {
        private FutbolEntities db = new FutbolEntities();
        private ConfiguracionSingleton conf = ConfiguracionSingleton.GetInstance();
        // GET: FaseEliminatorias
        public ActionResult Index()
        {
            return View(db.FaseEliminatoria.ToList());
        }

        // GET: FaseEliminatorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaseEliminatoria faseEliminatoria = db.FaseEliminatoria.Find(id);
            if (faseEliminatoria == null)
            {
                return HttpNotFound();
            }
            return View(faseEliminatoria);
        }

        // GET: FaseEliminatorias/Create
        public ActionResult Create()
        {
            ViewBag.fasee_estado = conf.configuracion.EstadosLlave;
            return View();
        }

        // POST: FaseEliminatorias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fasee_id,fasee_nombre,fasee_fecha,fasee_num_equipos,fasee_al_mejor_de,fasee_estado,fasee_tercer_lugar")] FaseEliminatoria faseEliminatoria)
        {
            if (ModelState.IsValid)
            {
                faseEliminatoria.torneo_id = conf.configuracion.IdTorneo;
                faseEliminatoria.fasee_estado = "Configurando";
                db.FaseEliminatoria.Add(faseEliminatoria);
                db.SaveChanges();
                return RedirectToAction("ListaLlaves", "Llaves");
            }

            return View(faseEliminatoria);
        }

        // GET: FaseEliminatorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaseEliminatoria faseEliminatoria = db.FaseEliminatoria.Find(id);
            if (faseEliminatoria == null)
            {
                return HttpNotFound();
            }
            return View(faseEliminatoria);
        }

        // POST: FaseEliminatorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fasee_id,fasee_nombre,fasee_fecha,fasee_num_equipos,fasee_al_mejor_de,fasee_estado,fasee_tercer_lugar")] FaseEliminatoria faseEliminatoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faseEliminatoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faseEliminatoria);
        }

        // GET: FaseEliminatorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaseEliminatoria faseEliminatoria = db.FaseEliminatoria.Find(id);
            if (faseEliminatoria == null)
            {
                return HttpNotFound();
            }
            return View(faseEliminatoria);
        }

        // POST: FaseEliminatorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FaseEliminatoria faseEliminatoria = db.FaseEliminatoria.Find(id);
            db.FaseEliminatoria.Remove(faseEliminatoria);
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
