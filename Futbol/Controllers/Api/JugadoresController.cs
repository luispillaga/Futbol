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

namespace Futbol.Controllers.Api
{
    public class JugadoresController : ApiController
    {
        private FutbolEntities db = new FutbolEntities();

        // GET: api/Jugadores
        public IQueryable<Jugador> GetJugador()
        {
            return db.Jugador;
        }

        // GET: api/Jugadores/5
        [ResponseType(typeof(Jugador))]
        public IHttpActionResult GetJugador(int id)
        {
            Jugador jugador = db.Jugador.Find(id);
            if (jugador == null)
            {
                return NotFound();
            }

            return Ok(jugador);
        }

        // PUT: api/Jugadores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJugador(int id, Jugador jugador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jugador.jugador_id)
            {
                return BadRequest();
            }

            db.Entry(jugador).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JugadorExists(id))
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

        // POST: api/Jugadores
        [ResponseType(typeof(Jugador))]
        public IHttpActionResult PostJugador(Jugador jugador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Jugador.Add(jugador);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jugador.jugador_id }, jugador);
        }

        // DELETE: api/Jugadores/5
        [ResponseType(typeof(Jugador))]
        public IHttpActionResult DeleteJugador(int id)
        {
            Jugador jugador = db.Jugador.Find(id);
            if (jugador == null)
            {
                return NotFound();
            }

            db.Jugador.Remove(jugador);
            db.SaveChanges();

            return Ok(jugador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JugadorExists(int id)
        {
            return db.Jugador.Count(e => e.jugador_id == id) > 0;
        }
    }
}