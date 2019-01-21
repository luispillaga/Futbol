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
    public class PagosController : ApiController
    {
        private FutbolEntities db = new FutbolEntities();

        // GET: api/Pagos
        public IQueryable<Pago> GetPago()
        {
            return db.Pago;
        }

        // GET: api/Pagos/5
        [ResponseType(typeof(Pago))]
        public IHttpActionResult GetPago(int id)
        {
            Pago pago = db.Pago.Find(id);
            if (pago == null)
            {
                return NotFound();
            }

            return Ok(pago);
        }

        // PUT: api/Pagos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPago(int id, Pago pago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pago.pago_id)
            {
                return BadRequest();
            }

            db.Entry(pago).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PagoExists(id))
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

        // POST: api/Pagos
        [ResponseType(typeof(Pago))]
        public IHttpActionResult PostPago(Pago pago)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pago.Add(pago);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = pago.pago_id }, pago);
        }

        // DELETE: api/Pagos/5
        [ResponseType(typeof(Pago))]
        public IHttpActionResult DeletePago(int id)
        {
            Pago pago = db.Pago.Find(id);
            if (pago == null)
            {
                return NotFound();
            }

            db.Pago.Remove(pago);
            db.SaveChanges();

            return Ok(pago);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PagoExists(int id)
        {
            return db.Pago.Count(e => e.pago_id == id) > 0;
        }
    }
}