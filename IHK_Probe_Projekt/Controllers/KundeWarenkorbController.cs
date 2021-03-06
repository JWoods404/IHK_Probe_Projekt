using IHK_Probe_Projekt.Models;
using IHK_Probe_Projekt.Session;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IHK_Probe_Projekt.Controllers
{
    public class KundeWarenkorbController : Controller
    {
        //string sessionId = Request.Properties[SessionIdHandler.SessionIdToken] as string;
        private KaffeeContext _db = new KaffeeContext();

        // GET: KundeWarenkorb
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddItem()
        {
            Kunde kunde;
            var artikel = Session["artikel"] as Artikel;
            kunde = Session["user"] as Kunde;
            if (artikel != null)
            {
                ArtikelWarenkorb artikelWarenkorb = new ArtikelWarenkorb();
                artikelWarenkorb.ArtikelId = artikel.Id;
                artikelWarenkorb.WarenkorbId = kunde.Warenkorb.Id;
                _db.ArtikelWarenkorb.Add(artikelWarenkorb);
                _db.SaveChanges();
            }
            return RedirectToAction("warenkorb", kunde);
        }

        [HttpPost]
        public ActionResult AddItem(string name)
        {
            Kunde kunde;
            var artikel = _db.Artikeln.FirstOrDefault(a => a.Name == name);
            if (Session["user"] == null)
            {
                Session["artikel"] = artikel;
                return View("login");
            }
            else
            {
                kunde = Session["user"] as Kunde;
                ArtikelWarenkorb artikelWarenkorb = new ArtikelWarenkorb();
                artikelWarenkorb.ArtikelId = artikel.Id;
                artikelWarenkorb.WarenkorbId = kunde.Warenkorb.Id;
                _db.ArtikelWarenkorb.Add(artikelWarenkorb);
                _db.SaveChanges();
            }
            return RedirectToAction("warenkorb", kunde);
        }

        public ActionResult Warenkorb()
        {
            if (Session["user"] == null)
            {
                return View("login");
            }
            Kunde kunde = Session["user"] as Kunde;
            var list = _db.ArtikelWarenkorb.Where(x => x.WarenkorbId == kunde.Warenkorb.Id).ToList();
            kunde.Warenkorb.Artikeln.Clear();
            foreach (var item in list)
            {
                Artikel artikel = _db.Artikeln.FirstOrDefault(x => x.Id == item.ArtikelId);
                kunde.Warenkorb.Artikeln.Add(artikel);
            }
            return View(kunde);
        }

        [HttpPost]
        public ActionResult Warenkorb(FormCollection formCollection)
        {
            Kunde kunde = Session["user"] as Kunde;
            Bestellung bestellung = new Bestellung();
            bestellung.KundeId = kunde.Id;
            _db.Bestellungen.Add(bestellung);
            _db.SaveChanges();
            foreach (var item in kunde.Warenkorb.Artikeln)
            {
                ArtikelBestellung artikelBestellung = new ArtikelBestellung
                {
                    ArtikelId = item.Id,
                    BestellungId = bestellung.Id,
                    KundeId = kunde.Id
                };
                _db.ArtikelBestellung.Add(artikelBestellung);
                _db.SaveChanges();
                bestellung.Total += item.Preis;
                //bestellung.Artikeln.Add(item);
                RedirectToAction("PutBestellung", "Bestellung", (bestellung.Id, bestellung));
                _db.SaveChanges();
            }

            kunde.Bestellungen.Add(bestellung);
            Session["bestellung"] = bestellung;
            return RedirectToAction("checkout");
        }

        public ActionResult Checkout()
        {
            Kunde kunde = Session["user"] as Kunde;
            Bestellung bestellung = Session["bestellung"] as Bestellung;
            var bestellungId = kunde.Bestellungen.Max(x => x.Id);
            var list = _db.ArtikelBestellung.Where(ab => ab.BestellungId == bestellungId).ToList();
            List<Artikel> artikeln = new List<Artikel>();
            foreach (var item in list)
            {
                var artikel = _db.Artikeln.FirstOrDefault(a => a.Id == item.ArtikelId);
                artikeln.Add(artikel);
            }
            ViewBag.Kunde = kunde;
            ViewBag.list = artikeln;
            ViewBag.Total = bestellung.Total;
            ViewBag.KundenId = kunde.Id;
            ViewBag.BestellungId = bestellung.Id;

            kunde.Warenkorb.Artikeln.Clear();

            bool check = true;

            while (check)
            {
                var result = _db.ArtikelWarenkorb.FirstOrDefault(a => a.WarenkorbId == kunde.Warenkorb.Id);
                if (result != null)
                {
                    _db.ArtikelWarenkorb.Remove(result);
                    _db.SaveChanges();
                }
                else check = false;
            }
            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            string pw = HashHelper.CreateHash(Password);
            var kunde = _db.Kunden.FirstOrDefault(k => k.Email == Email && k.Password == pw);
            if (kunde != null)
            {
                Session["user"] = kunde;
                return RedirectToAction("AddItem");
            }
            ViewBag.Error = "Die Email/Passwort Combination ist falsch";
            return View();
        }

        public ActionResult NeuerKunde()
        {
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        public ActionResult NeuerKunde(Kunde kunde)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_db.Kunden.FirstOrDefault(k => k.Email == kunde.Email) != null)
                    {
                        ViewBag.Error = "Die Emailaddresse ist schon registriert";
                        return View();
                    }
                    kunde.Password = HashHelper.CreateHash(kunde.Password);
                    kunde.Warenkorb = new Warenkorb();
                    _db.Kunden.Add(kunde);
                    _db.SaveChanges();
                    Session["user"] = kunde;
                }
                catch (DbUpdateConcurrencyException)
                {

                }
            }
            return RedirectToAction("warenkorb");
        }
    }
}