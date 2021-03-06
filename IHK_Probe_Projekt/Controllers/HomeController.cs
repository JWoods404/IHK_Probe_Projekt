using IHK_Probe_Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IHK_Probe_Projekt.Controllers
{
    public class HomeController : Controller
    {
        private KaffeeContext _db = new KaffeeContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Shop()
        {
            List<Artikel> list = _db.Artikeln.ToList();
            ViewBag.count = list.Count();
            ViewBag.list = list;
            return View();
        }

        [HttpPost]
        public ActionResult Shop(string name)
        {
            var artikel = _db.Artikeln.FirstOrDefault(a => a.Name == name);
            return RedirectToAction("ArtikelInfo",artikel);
        }

        public ActionResult ArtikelInfo(Artikel artikel)
        {
            return View(artikel);
        }

        public ActionResult Kalender()
        {
            ViewBag.Message = "Veranstaltungen";

            return View();
        }
    }
}