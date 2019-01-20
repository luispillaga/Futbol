using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
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
        private ConfiguracionSingleton conf = ConfiguracionSingleton.GetInstance();

        public ActionResult TorneoHome(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conf.configuracion.IdTorneo = id;
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
            ViewBag.torneo_estado = conf.configuracion.EstadosTorneo;
            var provincias = db.Provincia.ToList();
            var viewModel = new TorneoFormViewModel();
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterTorneo(TorneoFormViewModel torneo)
        {
            if (ModelState.IsValid)
            {
                var filename = Path.GetFileNameWithoutExtension(torneo.Imagen.ImageFile.FileName);
                var extension = Path.GetExtension(torneo.Imagen.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yy-MM-dd") + extension;
                torneo.Direccion.ciuadad_id = torneo.ciudad_id;
                torneo.Imagen.imagen_title = filename;
                torneo.Imagen.imagen_path = "~/Images/" + filename;
                var NewTorneo = new Torneo()
                {
                    Direccion = torneo.Direccion,
                    Imagen = torneo.Imagen,
                    torneo_nombre = torneo.torneo_nombre,
                    torneo_descripcion =  torneo.torneo_descripcion,
                    torneo_fecha_inicio = torneo.torneo_fecha_inicio,
                    torneo_hora_inicio = torneo.torneo_hora_inicio,
                    torneo_precio = torneo.torneo_precio,
                    torneo_estado = torneo.torneo_estado

                };
                filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                torneo.Imagen.ImageFile.SaveAs(filename);
                db.Torneo.Add(NewTorneo);
                db.SaveChanges();
                return RedirectToAction("ListaTorneos", "Torneos");
            }
            ViewBag.provincia_id = new SelectList(db.Provincia, "Id", "Nombre");
            return View(torneo);
        }

        // GET: Torneos/ListaTorneos
        public ActionResult ListaTorneos()
        {
            var torneo = db.Torneo.Include(t => t.Direccion).Include(t => t.Imagen);
            return View(torneo.ToList());
        }


        //GET: Ver Mi torneo
        public ActionResult MiTorneo()
        {
            if (conf.configuracion.IdTorneo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Torneo torneo = db.Torneo.Find(conf.configuracion.IdTorneo);
            if (torneo == null)
            {
                return HttpNotFound();
            }
            return View(torneo);
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
