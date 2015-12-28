using OnliStam.Pomocnici;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace Online_Stamparija.Controllers.api
{
    public class FileController : ApiController
    {
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateErrorResponse(System.Net.HttpStatusCode.OK, "This was triggered by GET request");
        }

        [System.Web.Http.HttpPost]
        public async System.Threading.Tasks.Task<HttpResponseMessage> UploadFile([FromBody]string id)
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
                        dataStream.Seek(0, System.IO.SeekOrigin.Begin);
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
}
