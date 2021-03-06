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
    public class WarenkorbController : ApiController
    {
        private KaffeeContext db = new KaffeeContext();

        // GET: api/Warenkorb
        public IQueryable<Warenkorb> GetWarenkorb()
        {
            return db.Warenkorb;
        }

        // GET: api/Warenkorb/5
        [ResponseType(typeof(Warenkorb))]
        public IHttpActionResult GetWarenkorb(int id)
        {
            Warenkorb warenkorb = db.Warenkorb.Find(id);
            if (warenkorb == null)
            {
                return NotFound();
            }

            return Ok(warenkorb);
        }

        // PUT: api/Warenkorb/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWarenkorb(int id, Warenkorb warenkorb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != warenkorb.Id)
            {
                return BadRequest();
            }

            db.Entry(warenkorb).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarenkorbExists(id))
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

        // POST: api/Warenkorb
        [ResponseType(typeof(Warenkorb))]
        public IHttpActionResult PostWarenkorb(Warenkorb warenkorb)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Warenkorb.Add(warenkorb);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = warenkorb.Id }, warenkorb);
        }

        // DELETE: api/Warenkorb/5
        [ResponseType(typeof(Warenkorb))]
        public IHttpActionResult DeleteWarenkorb(int id)
        {
            Warenkorb warenkorb = db.Warenkorb.Find(id);
            if (warenkorb == null)
            {
                return NotFound();
            }

            db.Warenkorb.Remove(warenkorb);
            db.SaveChanges();

            return Ok(warenkorb);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WarenkorbExists(int id)
        {
            return db.Warenkorb.Count(e => e.Id == id) > 0;
        }
    }
}