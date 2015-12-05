using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Stamparija.Models
{
    public class Materijal
    {
        public int ID { get; set; }

        public string Naziv { get; set; }

        public string Opis { get; set; }

        public double Kolicina { get; set; }
    }
}