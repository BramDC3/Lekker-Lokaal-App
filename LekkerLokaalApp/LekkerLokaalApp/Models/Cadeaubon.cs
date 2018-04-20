using System;
using System.Collections.Generic;
using System.Text;

namespace LekkerLokaalApp.Models
{
    public class Cadeaubon
    {
        public int BestelLijnId { get; set; }
        public string Naam { get; set; }
        public decimal Prijs { get; set; }
        public DateTime AanmaakDatum { get; set; }
        public int HandelaarId { get; set; }
        public string Gebruikersnaam { get; set; }
        public int Geldigheid { get; set; }
    }
}
