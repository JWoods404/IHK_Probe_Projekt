using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHK_Probe_Projekt.Models
{
    public class Bestellung
    {
        public int Id { get; set; }
        public int KundeId { get; set; }
        public decimal Total { get; set; }

        public virtual ICollection<Artikel> Artikeln { get; set; }

        public Bestellung()
        {
            Artikeln = new HashSet<Artikel>();
        }
    }
}