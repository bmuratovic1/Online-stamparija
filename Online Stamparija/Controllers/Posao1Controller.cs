using Online_Stamparija.Models;
using Online_Stamparija.Models.OPP;
using OnliStam.Pomocnici;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Online_Stamparija.Controllers
{
    public class Posao1Controller: ApiController
    {
        public HttpResponseMessage Get()
        {
            return Request.CreateErrorResponse(System.Net.HttpStatusCode.OK, "");
        }

        // POST: Posao/CreateJson
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


        [HttpPost]
        public async System.Threading.Tasks.Task<HttpResponseMessage> UploadFile(string id)
        {
            var supportedTypes = new List<string> { "png", "jpg", "jpeg", "gif" };
            if(!Request.Content.IsMimeMultipartContent())
            {
                return Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "The request doesn't contain valid content!");
            }

            try
            {
                var provider = new MultipartMemoryStreamProvider();
                await Request.Content.ReadAsMultipartAsync(provider);

                string fileName = "";
                string fullpath = "";

                var file = provider.Contents.FirstOrDefault();
                if(file != null)
                {
                    if(string.IsNullOrEmpty(file.Headers.ContentDisposition.FileName))
                        throw new ArgumentNullException("file", "Invalid file provided!");

                    var serverPath = System.Web.HttpContext.Current.Server.MapPath("~/Images/Profile");
                    fileName = GetFileName(serverPath, file.Headers);
                    var ext = Path.GetExtension(fileName);

                    if(!supportedTypes.Contains(ext.Trim().TrimStart('.')))
                        throw new ArgumentException("Provided file type is not supported!");

                    fullpath = Path.Combine(serverPath, Path.GetFileName(fileName));

                    var dataStream = await file.ReadAsStreamAsync();

                    using(var fileStream = File.Create(fullpath))
                    {
                        dataStream.Seek(0, SeekOrigin.Begin);
                        dataStream.CopyTo(fileStream);
                    }
                }

                var dbHelper = new MySqlPomocnik();

                //dbHelper.IzvrsiProceduru(new SqlUpit(), new Dictionary<string,object>());

                var response = Request.CreateResponse(HttpStatusCode.OK);

                response.Content = new StringContent(JSONHelper.ToJSON(new { fileName = fileName }), Encoding.UTF8, "text/plain");
                response.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue(@"text/html");
                return response;
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        private string GetFileName(string path, HttpContentHeaders headers)
        {
            var v1 = new MultipartFormDataStreamProvider(path);
            string fileName = v1.GetLocalFileName(headers);
            if(headers.ContentDisposition != null && !string.IsNullOrEmpty(headers.ContentDisposition.FileName))
            {
                string newName = Guid.NewGuid().ToString("N");
                headers.ContentDisposition.FileName = newName + Path.GetExtension(headers.ContentDisposition.FileName.Replace('"', ' '));
                fileName = headers.ContentDisposition.FileName;
            }
            if(fileName.StartsWith("\"") && fileName.EndsWith("\""))
            {
                fileName = fileName.Trim('"');
            }
            if(fileName.Contains(@"/") || fileName.Contains(@"\"))
            {
                fileName = Path.GetFileName(fileName);
            }
            return fileName;

        }
    }
    public static class JSONHelper
    {
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static string ToJSON(this object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return serializer.Serialize(obj);
        }
    }
}
