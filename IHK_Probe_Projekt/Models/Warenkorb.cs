using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IHK_Probe_Projekt.Models
{
    public class Warenkorb
    {
        public int Id { get; set; }
        //public int KundenId {get;set;}
        public decimal Total { get; set; }

        public virtual ICollection<Artikel> Artikeln { get; set; }

        public Warenkorb()
        {
            Artikeln = new HashSet<Artikel>();
        }

    }
}