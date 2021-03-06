using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IHK_Probe_Projekt.Models
{
    public class KaffeeInitializer : CreateDatabaseIfNotExists<KaffeeContext>
    {
        protected override void Seed(KaffeeContext context)
        {
            // Daten
            Kunde k1 = new Kunde() { Vorname = "Heinz", Nachname = "Meier", Strasse = "Hauptstr.", Hausnr = "3", PLZ = "12345", Ort = "Beispiel", TelefonNr = "0172 123 4567", Email = "heinz@meier.de", Password = HashHelper.CreateHash("P@ssword") };
            Kunde k2 = new Kunde() { Vorname = "Claudia", Nachname = "Merz", Strasse = "Fischstr", Hausnr = "5", PLZ = "23456", Ort = "Hamburg", TelefonNr = "0172 456 4567", Email = "claudia@merz.de", Password = HashHelper.CreateHash("1234") };
            Artikel a1 = new Artikel { Name = "Filter Blend", Categorie = "Saisonaler Blend", Geschmacksnoten = "Schwarzer Trauben / Nougat / Schokolade", Beschreibung = "50 % COCHALAN (Land: PERU, Region: CAJAMARCA, 50 % MAGDALENA (Land: COLOMBIA, Region: HUILA)", Menge = 15, Preis = 14.00M };
            Artikel a2 = new Artikel { Name = "Mahembe", Categorie = "Rwanda", Geschmacksnoten = "Orange / Apfel / Oolong", Beschreibung = "Land: RWANDA, Region: NYAMASHEKE", Menge = 16, Preis = 16.00M };
            Artikel a3 = new Artikel { Name = "La Granada", Categorie = "Colombia", Geschmacksnoten = "Heidelbeer / Ananas / Rose", Beschreibung = "Land: Colombia, Region: HUILA", Menge = 20, Preis = 18.00M };
            Artikel a4 = new Artikel { Name = "Recreio", Categorie = "Brazil Espresso", Geschmacksnoten = "SCHOKOLADE TORTE/ HAZELNUSS / DATELN", Beschreibung = "Land: Brazil, Region: SÃO SEBASTIÃO DA GRAMA", Menge = 20, Preis = 13.00M };

            Bestellung b1 = new Bestellung { Id = 1 };
            b1.Artikeln.Add(a1);
            b1.Artikeln.Add(a3);
            b1.Artikeln.Add(a4);

            Warenkorb w1 = new Warenkorb { Id = 1 };
            Warenkorb w2 = new Warenkorb { Id = 2 };

            w1.Artikeln.Add(a2);
            k1.Warenkorb = w1;
            k1.Bestellungen.Add(b1);
            k2.Warenkorb = w2;

            context.Kunden.Add(k1);
            context.Kunden.Add(k2);
            base.Seed(context);
        }
    }

    public class KaffeeContext : DbContext
    {

        public KaffeeContext() : base("kaffeedb")
        {
            Database.SetInitializer(new KaffeeInitializer());
        }

        public DbSet<Artikel> Artikeln { get; set; }
        public DbSet<Bestellung> Bestellungen { get; set; }
        public DbSet<Kunde> Kunden { get; set; }
        public DbSet<Warenkorb> Warenkorb { get; set; }
        public DbSet<ArtikelBestellung> ArtikelBestellung { get; set; }
        public DbSet<ArtikelWarenkorb> ArtikelWarenkorb { get; set; }
    }

    // install-package entityframework
    // install-package Microsoft.AspNet.WebApi.Cors
    // in WebApiConfig, config.Formatters.Remove(config.Formatters.XmlFormatter);
    // in WebApiConfig, config.EnableCors();
    // add ConnectionString in Web.config
}