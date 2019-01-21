using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Futbol;
using Futbol.Models;

namespace Futbol.Controllers.Api
{
    public class TorneosController : ApiController
    {
        private FutbolEntities db = new FutbolEntities();
        private ConfiguracionSingleton conf = ConfiguracionSingleton.GetInstance();
        // GET: api/Torneos
        public IQueryable<Torneo> GetTorneo()
        {
            return db.Torneo;
        }

        // GET: api/Torneos/5
        [ResponseType(typeof(Torneo))]
        public IHttpActionResult GetTorneo(int id)
        {
            Torneo torneo = db.Torneo.Find(id);
            if (torneo == null)
            {
                return NotFound();
            }

            return Ok(torneo);
        }

        // PUT: api/Torneos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTorneo(int id, Torneo torneo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != torneo.torneo_id)
            {
                return BadRequest();
            }

            db.Entry(torneo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TorneoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Torneos
        [ResponseType(typeof(Torneo))]
        public IHttpActionResult PostTorneo(Torneo torneo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Torneo.Add(torneo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = torneo.torneo_id }, torneo);
        }

        // DELETE: api/Torneos/5
        [ResponseType(typeof(Torneo))]
        public IHttpActionResult DeleteTorneo(int id)
        {
            Torneo torneo = db.Torneo.Find(id);
            if (torneo == null)
            {
                return NotFound();
            }
            db.Torneo.Remove(torneo);
            var torneos_equipos = db.TorneoEquipo.Where(te => te.torneo_id == id);
            if (torneos_equipos != null)
            {
                db.TorneoEquipo.RemoveRange(torneos_equipos);
            }

            db.SaveChanges();

            return Ok(torneo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TorneoExists(int id)
        {
            return db.Torneo.Count(e => e.torneo_id == id) > 0;
        }
    }
}