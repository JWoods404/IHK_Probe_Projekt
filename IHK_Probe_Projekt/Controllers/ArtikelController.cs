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
    public class ArtikelController : ApiController
    {
        private KaffeeContext db = new KaffeeContext();

        // GET: api/Artikel
        public IQueryable<Artikel> GetArtikeln()
        {
            return db.Artikeln;
        }

        // GET: api/Artikel/5
        [ResponseType(typeof(Artikel))]
        public IHttpActionResult GetArtikel(int id)
        {
            Artikel artikel = db.Artikeln.Find(id);
            if (artikel == null)
            {
                return NotFound();
            }

            return Ok(artikel);
        }

        // PUT: api/Artikel/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArtikel(int id, Artikel artikel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artikel.Id)
            {
                return BadRequest();
            }

            db.Entry(artikel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtikelExists(id))
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

        // POST: api/Artikel
        [ResponseType(typeof(Artikel))]
        public IHttpActionResult PostArtikel(Artikel artikel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Artikeln.Add(artikel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = artikel.Id }, artikel);
        }

        // DELETE: api/Artikel/5
        [ResponseType(typeof(Artikel))]
        public IHttpActionResult DeleteArtikel(int id)
        {
            Artikel artikel = db.Artikeln.Find(id);
            if (artikel == null)
            {
                return NotFound();
            }

            db.Artikeln.Remove(artikel);
            db.SaveChanges();

            return Ok(artikel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArtikelExists(int id)
        {
            return db.Artikeln.Count(e => e.Id == id) > 0;
        }
    }
}