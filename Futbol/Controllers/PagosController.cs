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
    public class PagosController : Controller
    {
        private FutbolEntities db = new FutbolEntities();
        private ConfiguracionSingleton conf = ConfiguracionSingleton.GetInstance();
        // GET: Pagos
        public ActionResult Index()
        {
            var pago = db.Pago.Include(p => p.TorneoEquipo);
            return View(pago.ToList());
        }

        // GET: Pagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pago.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // GET: Pagos/Create
        public ActionResult Create()
        {
            var TorneosEquipo = db.TorneoEquipo.Where(te => te.torneo_id == conf.configuracion.IdTorneo);
            ViewBag.tor_equ_id = new SelectList(TorneosEquipo, "tor_equ_id", "Equipo.equipo_nombre");
            return View();
        }

        // POST: Pagos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pago_id,pago_fecha,pago_valor,pago_descripcion,tor_equ_id")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                db.Pago.Add(pago);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tor_equ_id = new SelectList(db.TorneoEquipo, "tor_equ_id", "tor_equ_id", pago.tor_equ_id);
            return View(pago);
        }

        // GET: Pagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pago.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            ViewBag.tor_equ_id = new SelectList(db.TorneoEquipo, "tor_equ_id", "tor_equ_id", pago.tor_equ_id);
            return View(pago);
        }

        // POST: Pagos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pago_id,pago_fecha,pago_valor,pago_descripcion,tor_equ_id")] Pago pago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pago).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tor_equ_id = new SelectList(db.TorneoEquipo, "tor_equ_id", "tor_equ_id", pago.tor_equ_id);
            return View(pago);
        }

        // GET: Pagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pago pago = db.Pago.Find(id);
            if (pago == null)
            {
                return HttpNotFound();
            }
            return View(pago);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pago pago = db.Pago.Find(id);
            db.Pago.Remove(pago);
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
