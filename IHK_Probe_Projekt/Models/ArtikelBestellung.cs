using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace IHK_Probe_Projekt.Models
{
    public class ArtikelBestellung
    {
        public int Id { get; set; }

        public int ArtikelId { get; set; }
        //public int menge { get; set; }
        public int BestellungId { get; set; }
        public int KundeId { get; set; }
    }
}