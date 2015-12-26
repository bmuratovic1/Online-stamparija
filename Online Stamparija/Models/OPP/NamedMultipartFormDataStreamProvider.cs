using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Online_Stamparija.Models.OPP
{
    /// <summary>
    /// 
    /// </summary>
    public class NamedMultipartFormDataStreamProvider: MultipartFormDataStreamProvider
    {
        public NamedMultipartFormDataStreamProvider(string fileName)
            : base(fileName)
        {
        }
        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders
          headers)
        {
            string fileName = base.GetLocalFileName(headers);
            if(!string.IsNullOrEmpty(headers.ContentDisposition.FileName))
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