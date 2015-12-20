using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Stamparija.Models
{
    public class Posao
    {
        public int ID { get; set; }

        public string Naziv { get; set; }

        public string Opis { get; set; }

        public string VrijemeTrajanja { get; set; }

        public string VrstaMaterijala { get; set; }

        public int MaterijalId { get; set; }

        public string KolicinaMaterijala { get; set; }

        public int Status { get; set; }
    }
}