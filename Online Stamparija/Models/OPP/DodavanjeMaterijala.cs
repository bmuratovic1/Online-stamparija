using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// OOP - Objekat za Prijenos Podataka
namespace Online_Stamparija.Models.OPP
{
    /// <summary>
    /// predstavlja redu u tabeli DodavanjaMaterijala
    /// </summary>
    public class DodavanjeMaterijala
    {
        public int ID { get; set; }

        public int MaterijalId { get; set; }

        public int KorisnikId { get; set; }

        public double Kolicina { get; set; }

        public DateTime DatumDodavanja { get; set; }
    }
}