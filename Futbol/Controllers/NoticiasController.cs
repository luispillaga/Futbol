using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Futbol;
using Futbol.Models;

namespace Futbol.Controllers
{
    public class NoticiasController : Controller
    {
        private FutbolEntities db = new FutbolEntities();
        private ConfiguracionSingleton conf = ConfiguracionSingleton.GetInstance(); 
        // GET: Noticias
        public ActionResult Index()
        {
           // var noticia = db.Noticia.Include(n => n.Imagen).Include(n => n.Torneo);
            var noticia = db.Noticia.Where(n => n.torneo_id == conf.configuracion.IdTorneo);
            return View(noticia.ToList());
        }

        // GET: Noticias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticia noticia = db.Noticia.Find(id);
            if (noticia == null)
            {
                return HttpNotFound();
            }
            return View(noticia);
        }

        // GET: Noticias/Create
        public ActionResult Create()
        {
            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title");
            ViewBag.torneo_id = new SelectList(db.Torneo, "torneo_id", "torneo_nombre");
            return View();
        }

        // POST: Noticias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Noticia noticia)
        {
            if (ModelState.IsValid)
            {
                var filename = Path.GetFileNameWithoutExtension(noticia.Imagen.ImageFile.FileName);
                var extension = Path.GetExtension(noticia.Imagen.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yy-MM-dd") + extension;
                noticia.Imagen.imagen_title = filename;
                noticia.Imagen.imagen_path = "~/Images/" + filename;
                noticia.torneo_id = conf.configuracion.IdTorneo;
                filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                noticia.Imagen.ImageFile.SaveAs(filename);
                db.Noticia.Add(noticia);
                db.SaveChanges();
                return RedirectToAction("Index", "Noticias");
            }

            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title", noticia.imagen_id);
            ViewBag.torneo_id = new SelectList(db.Torneo, "torneo_id", "torneo_nombre", noticia.torneo_id);
            return View(noticia);
        }

        // GET: Noticias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticia noticia = db.Noticia.Find(id);
            if (noticia == null)
            {
                return HttpNotFound();
            }
            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title", noticia.imagen_id);
            ViewBag.torneo_id = new SelectList(db.Torneo, "torneo_id", "torneo_nombre", noticia.torneo_id);
            return View(noticia);
        }

        // POST: Noticias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "noticia_id,noticia_titulo,noticia_descripcion,imagen_id,torneo_id")] Noticia noticia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(noticia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title", noticia.imagen_id);
            ViewBag.torneo_id = new SelectList(db.Torneo, "torneo_id", "torneo_nombre", noticia.torneo_id);
            return View(noticia);
        }

        // GET: Noticias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Noticia noticia = db.Noticia.Find(id);
            if (noticia == null)
            {
                return HttpNotFound();
            }
            return View(noticia);
        }

        // POST: Noticias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Noticia noticia = db.Noticia.Find(id);
            db.Noticia.Remove(noticia);
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
