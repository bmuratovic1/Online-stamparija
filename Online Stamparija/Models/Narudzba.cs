using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Stamparija.Models
{
    public class Narudzba
    {
        public int ID { get; set; } //mora imati ID :)

        public string NazivKorisnika { get; set; } //ime prezime ili firma

        public string Adresa { get; set; }

        public string BrojTelefona { get; set; }

        public string Opis { get; set; }

        public string Napomena { get; set; }

        public int brojKomada { get; set; }

        public DateTime vrijemeIsporuke { get; set; }

        public string Status { get; set; }

        public string NazivFajla { get; set; }

        public string usernameKorisnika { get; set; }
        //Komentar
    }
}