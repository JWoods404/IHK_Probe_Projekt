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
    public class ArtikelWarenkorbController : Controller
    {
        private KaffeeContext db = new KaffeeContext();

        // GET: ArtikelWarenkorb
        public ActionResult Index()
        {
            return View(db.ArtikelWarenkorb.ToList());
        }

        // GET: ArtikelWarenkorb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtikelWarenkorb artikelWarenkorb = db.ArtikelWarenkorb.Find(id);
            if (artikelWarenkorb == null)
            {
                return HttpNotFound();
            }
            return View(artikelWarenkorb);
        }

        // GET: ArtikelWarenkorb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtikelWarenkorb/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ArtikelId,WarenkorbId")] ArtikelWarenkorb artikelWarenkorb)
        {
            if (ModelState.IsValid)
            {
                db.ArtikelWarenkorb.Add(artikelWarenkorb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artikelWarenkorb);
        }

        // GET: ArtikelWarenkorb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtikelWarenkorb artikelWarenkorb = db.ArtikelWarenkorb.Find(id);
            if (artikelWarenkorb == null)
            {
                return HttpNotFound();
            }
            return View(artikelWarenkorb);
        }

        // POST: ArtikelWarenkorb/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ArtikelId,WarenkorbId")] ArtikelWarenkorb artikelWarenkorb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artikelWarenkorb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artikelWarenkorb);
        }

        // GET: ArtikelWarenkorb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtikelWarenkorb artikelWarenkorb = db.ArtikelWarenkorb.Find(id);
            if (artikelWarenkorb == null)
            {
                return HttpNotFound();
            }
            return View(artikelWarenkorb);
        }

        // POST: ArtikelWarenkorb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArtikelWarenkorb artikelWarenkorb = db.ArtikelWarenkorb.Find(id);
            db.ArtikelWarenkorb.Remove(artikelWarenkorb);
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
