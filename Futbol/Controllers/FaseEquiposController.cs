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
    public class FaseEquiposController : Controller
    {
        private FutbolEntities db = new FutbolEntities();

        // GET: FaseEquipos
        public ActionResult Index()
        {
            var faseEquipo = db.FaseEquipo.Include(f => f.TorneoEquipo).Include(f => f.Grupo).Include(f => f.TorneoEquipo1);
            return View(faseEquipo.ToList());
        }

        // GET: FaseEquipos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaseEquipo faseEquipo = db.FaseEquipo.Find(id);
            if (faseEquipo == null)
            {
                return HttpNotFound();
            }
            return View(faseEquipo);
        }

        // GET: FaseEquipos/Create
        public ActionResult Create()
        {
            ViewBag.fasee_id = new SelectList(db.TorneoEquipo, "tor_equ_id", "tor_equ_id");
            ViewBag.grupo_id = new SelectList(db.Grupo, "grupo_id", "grupo_nombre");
            ViewBag.tor_equ_id = new SelectList(db.TorneoEquipo, "tor_equ_id", "tor_equ_id");
            return View();
        }

        // POST: FaseEquipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "fase_equ_id,tor_equ_id,fasee_id,grupo_id")] FaseEquipo faseEquipo)
        {
            if (ModelState.IsValid)
            {
                db.FaseEquipo.Add(faseEquipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fasee_id = new SelectList(db.TorneoEquipo, "tor_equ_id", "tor_equ_id", faseEquipo.fasee_id);
            ViewBag.grupo_id = new SelectList(db.Grupo, "grupo_id", "grupo_nombre", faseEquipo.grupo_id);
            ViewBag.tor_equ_id = new SelectList(db.TorneoEquipo, "tor_equ_id", "tor_equ_id", faseEquipo.tor_equ_id);
            return View(faseEquipo);
        }

        // POST: FaseEquipos/Añadir equipo
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEquipo([Bind(Include = "fase_equ_id,tor_equ_id,fasee_id,grupo_id")] FaseEquipo faseEquipo)
        {
            if (ModelState.IsValid)
            {
                var NuevaEstadistica = new Estadistica()
                {
                    estadistica_puntos = 0,
                    estadistica_derrotas = 0,
                    estadistica_gol_favor = 0,
                    estadistica_victorias = 0,
                    estadistica_empates = 0,
                    estadistica_gol_diferencia = 0,
                    estadistica_gol_contra = 0
                };
                NuevaEstadistica.FaseEquipo = faseEquipo;
                db.Estadistica.Add(NuevaEstadistica);
                db.SaveChanges();
                return RedirectToAction("Details", "Grupos", new {id = faseEquipo.grupo_id});
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: FaseEquipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaseEquipo faseEquipo = db.FaseEquipo.Find(id);
            if (faseEquipo == null)
            {
                return HttpNotFound();
            }
            ViewBag.fasee_id = new SelectList(db.TorneoEquipo, "tor_equ_id", "tor_equ_id", faseEquipo.fasee_id);
            ViewBag.grupo_id = new SelectList(db.Grupo, "grupo_id", "grupo_nombre", faseEquipo.grupo_id);
            ViewBag.tor_equ_id = new SelectList(db.TorneoEquipo, "tor_equ_id", "tor_equ_id", faseEquipo.tor_equ_id);
            return View(faseEquipo);
        }

        // POST: FaseEquipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "fase_equ_id,tor_equ_id,fasee_id,grupo_id")] FaseEquipo faseEquipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faseEquipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fasee_id = new SelectList(db.TorneoEquipo, "tor_equ_id", "tor_equ_id", faseEquipo.fasee_id);
            ViewBag.grupo_id = new SelectList(db.Grupo, "grupo_id", "grupo_nombre", faseEquipo.grupo_id);
            ViewBag.tor_equ_id = new SelectList(db.TorneoEquipo, "tor_equ_id", "tor_equ_id", faseEquipo.tor_equ_id);
            return View(faseEquipo);
        }

        // GET: FaseEquipos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaseEquipo faseEquipo = db.FaseEquipo.Find(id);
            if (faseEquipo == null)
            {
                return HttpNotFound();
            }
            return View(faseEquipo);
        }

        // POST: FaseEquipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FaseEquipo faseEquipo = db.FaseEquipo.Find(id);
            db.FaseEquipo.Remove(faseEquipo);
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
