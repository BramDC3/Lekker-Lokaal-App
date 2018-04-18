using System;
using System.Collections.Generic;
using System.Text;

namespace LekkerLokaalApp.Models
{
    public class Handelaar
    {
        public int HandelaarId { get; set; }
        public string BTW_Nummer { get; set; }
        public string Beschrijving { get; set; }
        public string EersteAanmelding { get; set; }
        public string Emailadres { get; set; }
        public string Naam { get; set; }
        public string Gebruikersnaam { get; set; }
    }
}
