using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Futbol;
using Futbol.ModelViews;

namespace Futbol.Controllers
{
    public class JornadasController : Controller
    {
        private FutbolEntities db = new FutbolEntities();

        // GET: Jornadas
        public ActionResult Index()
        {
            var jornada = db.Jornada.Include(j => j.FaseEliminatoria).Include(j => j.Grupo);
            return View(jornada.ToList());
        }

        // GET: Jornadas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jornada jornada = db.Jornada.Find(id);
            if (jornada == null)
            {
                return HttpNotFound();
            }
            return View(jornada);
        }

        // GET: Jornadas/DetalleGrupo/5
        public ActionResult DetalleGrupo(int? id, string jornada_nombre)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jornada jornada = db.Jornada.Find(id);
            if (jornada == null)
            {
                return HttpNotFound();
            }
            
            List<Equipo> locales = new List<Equipo>();
            List<Equipo> visitantes = new List<Equipo>();
            var partidos = db.Partido.Where(p => p.jornada_id == id);
            var equipos = db.Equipo;
            if (partidos !=null)
            {
                foreach (var item in partidos)
                {
                    foreach (var item2 in equipos)
                    {
                        if (item.partido_equipo_local == item2.equipo_id)
                        {
                            locales.Add(item2);
                        }
                        if (item.partido_equipo_visitante == item2.equipo_id)
                        {
                            visitantes.Add(item2);
                        }
                    }
                }
            }

            ViewBag.jornada_nombre = jornada_nombre;
            var ViewModel = new JornadaPartidoViewModel()
            {
                Jornada = jornada,
                Partidos = db.Partido.Where(p => p.jornada_id == id),
                EquiposLocales = locales,
                EquiposVisitantes = visitantes
            };
            return View(ViewModel);
        }

        // GET: Jornadas/Create
        public ActionResult Create()
        {
            ViewBag.fasee_id = new SelectList(db.FaseEliminatoria, "fasee_id", "fasee_nombre");
            ViewBag.grupo_id = new SelectList(db.Grupo, "grupo_id", "grupo_nombre");
            return View();
        }

        // POST: Jornadas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "jornada_id,jornada_fecha_inicio,grupo_id,fasee_id")] Jornada jornada)
        {
            if (ModelState.IsValid)
            {
                db.Jornada.Add(jornada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fasee_id = new SelectList(db.FaseEliminatoria, "fasee_id", "fasee_nombre", jornada.fasee_id);
            ViewBag.grupo_id = new SelectList(db.Grupo, "grupo_id", "grupo_nombre", jornada.grupo_id);
            return View(jornada);
        }

        // GET: Jornadas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jornada jornada = db.Jornada.Find(id);
            if (jornada == null)
            {
                return HttpNotFound();
            }
            ViewBag.fasee_id = new SelectList(db.FaseEliminatoria, "fasee_id", "fasee_nombre", jornada.fasee_id);
            ViewBag.grupo_id = new SelectList(db.Grupo, "grupo_id", "grupo_nombre", jornada.grupo_id);
            return View(jornada);
        }

        // POST: Jornadas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "jornada_id,jornada_fecha_inicio,grupo_id,fasee_id")] Jornada jornada)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jornada).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fasee_id = new SelectList(db.FaseEliminatoria, "fasee_id", "fasee_nombre", jornada.fasee_id);
            ViewBag.grupo_id = new SelectList(db.Grupo, "grupo_id", "grupo_nombre", jornada.grupo_id);
            return View(jornada);
        }

        // GET: Jornadas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jornada jornada = db.Jornada.Find(id);
            if (jornada == null)
            {
                return HttpNotFound();
            }
            return View(jornada);
        }

        // POST: Jornadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jornada jornada = db.Jornada.Find(id);
            db.Jornada.Remove(jornada);
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
