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
    public class BestellungController : ApiController
    {
        private KaffeeContext db = new KaffeeContext();

        // GET: api/Bestellung
        public IQueryable<Bestellung> GetBestellungen()
        {
            return db.Bestellungen;
        }

        // GET: api/Bestellung/5
        [ResponseType(typeof(Bestellung))]
        public IHttpActionResult GetBestellung(int id)
        {
            Bestellung bestellung = db.Bestellungen.Find(id);
            if (bestellung == null)
            {
                return NotFound();
            }

            return Ok(bestellung);
        }

        // PUT: api/Bestellung/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBestellung(int id, Bestellung bestellung)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bestellung.Id)
            {
                return BadRequest();
            }

            db.Entry(bestellung).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BestellungExists(id))
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

        // POST: api/Bestellung
        [ResponseType(typeof(Bestellung))]
        public IHttpActionResult PostBestellung(Bestellung bestellung)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bestellungen.Add(bestellung);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bestellung.Id }, bestellung);
        }

        // DELETE: api/Bestellung/5
        [ResponseType(typeof(Bestellung))]
        public IHttpActionResult DeleteBestellung(int id)
        {
            Bestellung bestellung = db.Bestellungen.Find(id);
            if (bestellung == null)
            {
                return NotFound();
            }

            db.Bestellungen.Remove(bestellung);
            db.SaveChanges();

            return Ok(bestellung);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BestellungExists(int id)
        {
            return db.Bestellungen.Count(e => e.Id == id) > 0;
        }
    }
}