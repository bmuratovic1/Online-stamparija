using OnliStam.Pomocnici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Online_Stamparija.Controllers.api
{
    public class LozinkaController : ApiController
    {

        [HttpPost]
        public bool PromjeniLozinku(string username, string oldPassword, string newPassword)
        {
            var dbPomocnik = new MySqlPomocnik();
            var odg = dbPomocnik.IzvrsiProceduru(Konstante.StoredProcedures.PROMJENI_LOZINKU, new Dictionary<string, object>());
            return true;
        }
    }
}
