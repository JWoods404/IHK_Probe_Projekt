using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using IHK_Probe_Projekt.Models;

namespace IHK_Probe_Projekt.Controllers
{
    [EnableCors("*", "*", "*")]
    public class KundeController : ApiController
    {
        private KaffeeContext db = new KaffeeContext();

        // GET: api/Kunde
        public IQueryable<Kunde> GetKunden()
        {
            return db.Kunden;
        }

        // GET: api/Kunde/5
        [ResponseType(typeof(Kunde))]
        public IHttpActionResult GetKunde(int id)
        {
            Kunde kunde = db.Kunden.Find(id);
            if (kunde == null)
            {
                return NotFound();
            }

            return Ok(kunde);
        }

        // PUT: api/Kunde/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKunde(int id, Kunde kunde)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kunde.Id)
            {
                return BadRequest();
            }

            db.Entry(kunde).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KundeExists(id))
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

        // POST: api/Kunde
        [ResponseType(typeof(Kunde))]
        public IHttpActionResult PostKunde(Kunde kunde)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kunden.Add(kunde);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kunde.Id }, kunde);
        }

        // DELETE: api/Kunde/5
        [ResponseType(typeof(Kunde))]
        public IHttpActionResult DeleteKunde(int id)
        {
            Kunde kunde = db.Kunden.Find(id);
            if (kunde == null)
            {
                return NotFound();
            }

            db.Kunden.Remove(kunde);
            db.SaveChanges();

            return Ok(kunde);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KundeExists(int id)
        {
            return db.Kunden.Count(e => e.Id == id) > 0;
        }
    }
}