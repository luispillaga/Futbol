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
    public class JugadoresController : Controller
    {
        private FutbolEntities db = new FutbolEntities();

        // GET: Jugadores
        public ActionResult Index()
        {
            var jugador = db.Jugador.Include(j => j.Equipo).Include(j => j.Imagen);
            return View(jugador.ToList());
        }

        // GET: Jugadores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jugador jugador = db.Jugador.Find(id);
            if (jugador == null)
            {
                return HttpNotFound();
            }
            return View(jugador);
        }

        // GET: Jugadores/Create
        public ActionResult Create()
        {
            ViewBag.equipo_id = new SelectList(db.Equipo, "equipo_id", "equipo_nombre");
            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title");
            return View();
        }

        // POST: Jugadores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "jugador_id,jugador_cedula,jugador_nombres,jugador_apellidos,jugador_dorsal,jugador_edad,jugador_estado,imagen_id,equipo_id")] Jugador jugador)
        {
            if (ModelState.IsValid)
            {
                db.Jugador.Add(jugador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.equipo_id = new SelectList(db.Equipo, "equipo_id", "equipo_nombre", jugador.equipo_id);
            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title", jugador.imagen_id);
            return View(jugador);
        }

        // GET: Jugadores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jugador jugador = db.Jugador.Find(id);
            if (jugador == null)
            {
                return HttpNotFound();
            }
            ViewBag.equipo_id = new SelectList(db.Equipo, "equipo_id", "equipo_nombre", jugador.equipo_id);
            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title", jugador.imagen_id);
            return View(jugador);
        }

        // POST: Jugadores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "jugador_id,jugador_cedula,jugador_nombres,jugador_apellidos,jugador_dorsal,jugador_edad,jugador_estado,imagen_id,equipo_id")] Jugador jugador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jugador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.equipo_id = new SelectList(db.Equipo, "equipo_id", "equipo_nombre", jugador.equipo_id);
            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title", jugador.imagen_id);
            return View(jugador);
        }

        // GET: Jugadores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jugador jugador = db.Jugador.Find(id);
            if (jugador == null)
            {
                return HttpNotFound();
            }
            return View(jugador);
        }

        // POST: Jugadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jugador jugador = db.Jugador.Find(id);
            db.Jugador.Remove(jugador);
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
