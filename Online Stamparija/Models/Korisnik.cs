using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Stamparija.Models
{
    public class Korisnik
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string  Email { get; set; }

        public int Pozicija { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public int Aktivan { get; set; }
    }
}