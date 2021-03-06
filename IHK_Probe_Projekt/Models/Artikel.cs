using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHK_Probe_Projekt.Models
{
    public class Artikel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Preis { get; set; }
        public int Menge { get; set; }
        public string Categorie { get; set; }
        public string Beschreibung { get; set; }
        public string Geschmacksnoten { get; set; }
        public byte[] Foto { get; set; } // https://stackoverflow.com/questions/4653095/how-to-store-images-using-entity-framework-code-first-ctp-5
    }
}