using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IHK_Probe_Projekt.Models;

namespace IHK_Probe_Projekt.Controllers
{
    public class ArtikelBestellungController : Controller
    {
        private KaffeeContext db = new KaffeeContext();

        // GET: ArtikelBestellung
        public ActionResult Index()
        {
            return View(db.ArtikelBestellung.ToList());
        }

        // GET: ArtikelBestellung/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtikelBestellung artikelBestellung = db.ArtikelBestellung.Find(id);
            if (artikelBestellung == null)
            {
                return HttpNotFound();
            }
            return View(artikelBestellung);
        }

        // GET: ArtikelBestellung/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtikelBestellung/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ArtikelId,BestellungId,KundeId")] ArtikelBestellung artikelBestellung)
        {
            if (ModelState.IsValid)
            {
                db.ArtikelBestellung.Add(artikelBestellung);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artikelBestellung);
        }

        // GET: ArtikelBestellung/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtikelBestellung artikelBestellung = db.ArtikelBestellung.Find(id);
            if (artikelBestellung == null)
            {
                return HttpNotFound();
            }
            return View(artikelBestellung);
        }

        // POST: ArtikelBestellung/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ArtikelId,BestellungId,KundeId")] ArtikelBestellung artikelBestellung)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artikelBestellung).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artikelBestellung);
        }

        // GET: ArtikelBestellung/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtikelBestellung artikelBestellung = db.ArtikelBestellung.Find(id);
            if (artikelBestellung == null)
            {
                return HttpNotFound();
            }
            return View(artikelBestellung);
        }

        // POST: ArtikelBestellung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtikelBestellung artikelBestellung = db.ArtikelBestellung.Find(id);
            db.ArtikelBestellung.Remove(artikelBestellung);
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
