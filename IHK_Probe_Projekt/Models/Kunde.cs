using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHK_Probe_Projekt.Models
{
    public class Kunde
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Strasse { get; set; }
        public string Hausnr { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Email { get; set; }
        public string TelefonNr { get; set; }
        public string Password { get; set; } // Remember to save as hash value
        
        public virtual Warenkorb Warenkorb { get; set; }
        public virtual ICollection<Bestellung> Bestellungen { get; set; }

        public Kunde()
        {
            Bestellungen = new HashSet<Bestellung>();
        }
    }
}