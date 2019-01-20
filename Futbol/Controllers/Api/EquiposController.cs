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
    public class EquiposController : ApiController
    {
        private FutbolEntities db = new FutbolEntities();
        private ConfiguracionSingleton conf = ConfiguracionSingleton.GetInstance();
        // GET: api/Equipos
        public IQueryable<Equipo> GetEquipo()
        {
            return db.Equipo;
        }

        // GET: api/Equipos/5
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult GetEquipo(int id)
        {
            Equipo equipo = db.Equipo.Find(id);
            if (equipo == null)
            {
                return NotFound();
            }

            return Ok(equipo);
        }

        // PUT: api/Equipos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEquipo(int id, Equipo equipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != equipo.equipo_id)
            {
                return BadRequest();
            }

            db.Entry(equipo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoExists(id))
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

        // POST: api/Equipos
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult PostEquipo(Equipo equipo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Equipo.Add(equipo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = equipo.equipo_id }, equipo);
        }

        // DELETE: api/Equipos/5
        [ResponseType(typeof(Equipo))]
        public IHttpActionResult DeleteEquipo(int id)
        {
            Equipo equipo = db.Equipo.Find(id);
            if (equipo == null)
            {
                return NotFound();
            }

            db.Equipo.Remove(equipo);
            //db.SaveChanges();

            TorneoEquipo torneo_equipo = db.TorneoEquipo
                .FirstOrDefault(te => te.torneo_id == conf.configuracion.IdTorneo && te.equipo_id == id);
            if (torneo_equipo == null)
            {
                return NotFound();
            }

            db.TorneoEquipo.Remove(torneo_equipo);
            db.SaveChanges();
            return Ok(equipo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquipoExists(int id)
        {
            return db.Equipo.Count(e => e.equipo_id == id) > 0;
        }
    }
}