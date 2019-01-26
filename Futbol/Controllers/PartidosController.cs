using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Futbol;

namespace Futbol.Controllers
{
    public class PartidosController : Controller
    {
        private FutbolEntities db = new FutbolEntities();

        // GET: Partidos
        public ActionResult Index()
        {
            var partido = db.Partido.Include(p => p.Jornada);
            return View(partido.ToList());
        }

        // GET: Partidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido partido = db.Partido.Find(id);
            if (partido == null)
            {
                return HttpNotFound();
            }
            return View(partido);
        }

        // GET: Partidos/Create
        public ActionResult Create()
        {
            ViewBag.jornada_id = new SelectList(db.Jornada, "jornada_id", "jornada_id");
            return View();
        }

        // POST: Partidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "partido_id,partido_hora,partido_estado,partido_observaciones,partido_equipo_local,partido_equipo_visitante,jornada_id")] Partido partido)
        {
            if (ModelState.IsValid)
            {
                db.Partido.Add(partido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.jornada_id = new SelectList(db.Jornada, "jornada_id", "jornada_id", partido.jornada_id);
            return View(partido);
        }

        // GET: Partidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido partido = db.Partido.Find(id);
            if (partido == null)
            {
                return HttpNotFound();
            }
            ViewBag.jornada_id = new SelectList(db.Jornada, "jornada_id", "jornada_id", partido.jornada_id);
            return View(partido);
        }

        // POST: Partidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "partido_id,partido_hora,partido_estado,partido_observaciones,partido_equipo_local,partido_equipo_visitante,jornada_id")] Partido partido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.jornada_id = new SelectList(db.Jornada, "jornada_id", "jornada_id", partido.jornada_id);
            return View(partido);
        }

        // GET: Partidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido partido = db.Partido.Find(id);
            if (partido == null)
            {
                return HttpNotFound();
            }
            return View(partido);
        }

        // POST: Partidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partido partido = db.Partido.Find(id);
            db.Partido.Remove(partido);
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
