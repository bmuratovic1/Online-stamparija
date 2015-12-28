using Online_Stamparija.Models;
using OnliStam.Pomocnici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Online_Stamparija.Controllers.api
{
    public class PosaoController : ApiController
    {

        // POST: api/Posao
        [HttpPost]
        public HttpResponseMessage CreateJson([System.Web.Http.FromBody] Posao model)
        {
            //if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            //{
            try
            {
                var dbPomocnik = new MySqlPomocnik();
                double potrebnaKolicinaMaterijala = Convert.ToDouble(model.KolicinaMaterijala);

                var materijal = dbPomocnik.IzvrsiProceduru<Materijal>(Konstante.StoredProcedures.DAJ_MATERIJAL_ID, new Dictionary<string, object> { { "ID", model.MaterijalId } });

                if(materijal.Kolicina < potrebnaKolicinaMaterijala)
                {
                    return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, "Nedovoljno raspoloživog materijala!");
                }
                materijal.Kolicina -= potrebnaKolicinaMaterijala;

                model.Naziv = HttpUtility.UrlDecode(model.Naziv);
                model.Opis = HttpUtility.UrlDecode(model.Opis);
                model.VrstaMaterijala = HttpUtility.HtmlDecode(model.VrstaMaterijala);
                dbPomocnik.IzvrsiProceduru(Konstante.StoredProcedures.IZMJENI_MATERIJAL, materijal);

                model.VrstaMaterijala = materijal.Naziv;
                var response = dbPomocnik.IzvrsiProceduru<Posao>(Konstante.StoredProcedures.DODAJ_POSAO, model);

                return Request.CreateErrorResponse(System.Net.HttpStatusCode.OK, response.Rows[0]["ID"].ToString());
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
