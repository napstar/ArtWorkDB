using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Artwork_App
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           Int32 empno;
            if (context.Request.QueryString["id"] != null)
               empno = Convert.ToInt32(context.Request.QueryString["id"]);
            else
               throw new ArgumentException("No parameter specified");
           context.Response.ContentType = "image/jpeg";
           Stream strm = ShowEmpImage(empno);
           byte[] buffer = new byte[4096];
           int byteSeq = strm.Read(buffer, 0, 4096);
           while (byteSeq> 0)
           {
             context.Response.OutputStream.Write(buffer, 0, byteSeq);
             byteSeq = strm.Read(buffer, 0, 4096);
           }     
        }

        private Stream ShowEmpImage(int id)
        {

            byte[] fileData = null;
            try
            {

                using (var c = new ArtWorkDBEntities1())
                {
                    fileData = (from p in c.Artworks
                                where p.ArtworkID == id
                                select p.Image).FirstOrDefault();
                    //var fileExtension = (from p in c.Artworks
                    //                     where p.ArtworkID == id
                    //                     select p.FileExtension).FirstOrDefault();

                    return new MemoryStream(fileData);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}