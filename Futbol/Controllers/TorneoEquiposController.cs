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
    public class TorneoEquiposController : Controller
    {
        private FutbolEntities db = new FutbolEntities();

        // GET: TorneoEquipos
        public ActionResult Index()
        {
            var torneoEquipo = db.TorneoEquipo.Include(t => t.Equipo).Include(t => t.Torneo);
            return View(torneoEquipo.ToList());
        }

       


        // GET: TorneoEquipos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TorneoEquipo torneoEquipo = db.TorneoEquipo.Find(id);
            if (torneoEquipo == null)
            {
                return HttpNotFound();
            }
            return View(torneoEquipo);
        }


 
        public ActionResult Create([Bind(Include = "tor_equ_id,tor_equ_fecha_inscripcion,torneo_id,equipo_id")] TorneoEquipo torneoEquipo)
        {
            if (ModelState.IsValid)
            {
                db.TorneoEquipo.Add(torneoEquipo);
                db.SaveChanges();
                return RedirectToAction("Index","Equipos");
            }

            ViewBag.equipo_id = new SelectList(db.Equipo, "equipo_id", "equipo_nombre", torneoEquipo.equipo_id);
            ViewBag.torneo_id = new SelectList(db.Torneo, "torneo_id", "torneo_nombre", torneoEquipo.torneo_id);
            return View(torneoEquipo);
        }

        // GET: TorneoEquipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TorneoEquipo torneoEquipo = db.TorneoEquipo.Find(id);
            if (torneoEquipo == null)
            {
                return HttpNotFound();
            }
            ViewBag.equipo_id = new SelectList(db.Equipo, "equipo_id", "equipo_nombre", torneoEquipo.equipo_id);
            ViewBag.torneo_id = new SelectList(db.Torneo, "torneo_id", "torneo_nombre", torneoEquipo.torneo_id);
            return View(torneoEquipo);
        }

        // POST: TorneoEquipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tor_equ_id,tor_equ_fecha_inscripcion,torneo_id,equipo_id")] TorneoEquipo torneoEquipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(torneoEquipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.equipo_id = new SelectList(db.Equipo, "equipo_id", "equipo_nombre", torneoEquipo.equipo_id);
            ViewBag.torneo_id = new SelectList(db.Torneo, "torneo_id", "torneo_nombre", torneoEquipo.torneo_id);
            return View(torneoEquipo);
        }

        // GET: TorneoEquipos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TorneoEquipo torneoEquipo = db.TorneoEquipo.Find(id);
            if (torneoEquipo == null)
            {
                return HttpNotFound();
            }
            return View(torneoEquipo);
        }

        // POST: TorneoEquipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TorneoEquipo torneoEquipo = db.TorneoEquipo.Find(id);
            db.TorneoEquipo.Remove(torneoEquipo);
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
