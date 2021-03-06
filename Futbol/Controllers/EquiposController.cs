﻿using System;
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
    public class EquiposController : Controller
    {
        private FutbolEntities db = new FutbolEntities();
        private ConfiguracionSingleton conf = ConfiguracionSingleton.GetInstance();

        public ActionResult Index()
        {
            var equipo = db.Equipo.Include(e => e.Imagen);
            var torneoEquipo = db.TorneoEquipo;
            List<Equipo> todos_equipos = equipo.ToList();
            List<TorneoEquipo> todos_relacion = torneoEquipo.ToList();
            List<TorneoEquipo> nuevo_relacion = new List<TorneoEquipo>();

            var conf = ConfiguracionSingleton.GetInstance();
            ViewBag.IdTorneo = conf.configuracion.IdTorneo;

            foreach (var item in todos_relacion)
            {
                if (item.torneo_id == conf.configuracion.IdTorneo)
                    nuevo_relacion.Add(item);
            }

            List<Equipo> lista_equipos = new List<Equipo>();

            foreach (var item in nuevo_relacion) { 
                foreach (var item2 in todos_equipos)
                {
                    if (item.equipo_id==item2.equipo_id) {
                        lista_equipos.Add(item2);
                    }
                }
            }

            return View(lista_equipos);
        }

        // GET: Equipos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = db.Equipo.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // GET: Equipos/Create
        public ActionResult Create()
        {
            ViewBag.equipo_estado = conf.configuracion.EstadosEquipo;
            return View();
        }

        // POST: Equipos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Equipo equipo)
        {
            conf = ConfiguracionSingleton.GetInstance();
            
            if (ModelState.IsValid)
            {
                var filename = Path.GetFileNameWithoutExtension(equipo.Imagen.ImageFile.FileName);
                var extension = Path.GetExtension(equipo.Imagen.ImageFile.FileName);
                filename = filename + DateTime.Now.ToString("yy-MM-dd") + extension;
                equipo.Imagen.imagen_title = filename;
                equipo.Imagen.imagen_path = "~/Images/" + filename;
                filename = Path.Combine(Server.MapPath("~/Images/"), filename);
                equipo.Imagen.ImageFile.SaveAs(filename);

                db.Equipo.Add(equipo);
                db.SaveChanges();
                return RedirectToAction("Create", "TorneoEquipos", new TorneoEquipo { tor_equ_fecha_inscripcion = DateTime.Now , equipo_id = equipo.equipo_id,torneo_id = conf.configuracion.IdTorneo});
            }

            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title",  equipo.imagen_id);
            return View(equipo);
        }

        // GET: Equipos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = db.Equipo.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title", equipo.imagen_id);
            return View(equipo);
        }

        // POST: Equipos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "equipo_id,equipo_nombre,equipo_representante,equipo_celular,equipo_telefono,equipo_estado,imagen_id")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.imagen_id = new SelectList(db.Imagen, "imagen_id", "imagen_title", equipo.imagen_id);
            return View(equipo);
        }

        // GET: Equipos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipo equipo = db.Equipo.Find(id);
            if (equipo == null)
            {
                return HttpNotFound();
            }
            return View(equipo);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipo equipo = db.Equipo.Find(id);
            db.Equipo.Remove(equipo);
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
