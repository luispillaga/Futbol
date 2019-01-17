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
    public class TorneosController : Controller
    {
        private FutbolEntities db = new FutbolEntities();


        public ActionResult TorneoHome(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var singleton = ConfiguracionSingleton.GetInstance();
            singleton.configuracion.IdTorneo = id;
            var torneo = db.Torneo.Find(id);
            if (torneo == null)
            {
                return HttpNotFound();
            }
            return View(torneo);
        }

        // GET: Torneos

        public ActionResult Index()
        {
            var torneo = db.Torneo.Include(t => t.Direccion).Include(t => t.Imagen);
            return View(torneo.ToList());
        }

        // GET: Torneos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Torneo torneo = db.Torneo.Find(id);
            if (torneo == null)
            {
                return HttpNotFound();
            }
            return View(torneo);
        }

        // GET: Torneos/Create
        public ActionResult Create()
        {
            ViewBag.direccion_id = new SelectList(db.Direccion, "direccion_id", "direccion_calle");
            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title");
            return View();
        }

        //GET: Torneos/RegisterTorneo
        public ActionResult RegisterTorneo()
        {
            ViewBag.provincia_id = new SelectList(db.Provincia, "Id", "Nombre");

            var provincias = db.Provincia.ToList();
            var viewModel = new TorneoFormViewModel();
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterTorneo(TorneoFormViewModel torneo)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Torneo.Add(torneo);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.direccion_id = new SelectList(db.Direccion, "direccion_id", "direccion_calle", torneo.direccion_id);
            //ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title", torneo.imagen_id);
            return View(torneo);
        }

        // GET: Torneos/ListaTorneos
        public ActionResult ListaTorneos()
        {
            var torneo = db.Torneo.Include(t => t.Direccion).Include(t => t.Imagen);
            return View(torneo.ToList());
        }

        // POST: Torneos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "torneo_id,torneo_nombre,torneo_fecha_inicio,torneo_hora_inicio,torneo_estado,torneo_precio,torneo_descripcion,direccion_id,imagen_id")] Torneo torneo)
        {
            if (ModelState.IsValid)
            {
                db.Torneo.Add(torneo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.direccion_id = new SelectList(db.Direccion, "direccion_id", "direccion_calle", torneo.direccion_id);
            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title", torneo.imagen_id);
            return View(torneo);
        }

        // GET: Torneos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Torneo torneo = db.Torneo.Find(id);
            if (torneo == null)
            {
                return HttpNotFound();
            }
            ViewBag.direccion_id = new SelectList(db.Direccion, "direccion_id", "direccion_calle", torneo.direccion_id);
            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title", torneo.imagen_id);
            return View(torneo);
        }

        // POST: Torneos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "torneo_id,torneo_nombre,torneo_fecha_inicio,torneo_hora_inicio,torneo_estado,torneo_precio,torneo_descripcion,direccion_id,imagen_id")] Torneo torneo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(torneo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.direccion_id = new SelectList(db.Direccion, "direccion_id", "direccion_calle", torneo.direccion_id);
            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title", torneo.imagen_id);
            return View(torneo);
        }

        // GET: Torneos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Torneo torneo = db.Torneo.Find(id);
            if (torneo == null)
            {
                return HttpNotFound();
            }
            return View(torneo);
        }

        // POST: Torneos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Torneo torneo = db.Torneo.Find(id);
            db.Torneo.Remove(torneo);
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
