using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Artwork_App.Extensions
{
    public class HttpFile : HttpPostedFileBase, IDisposable
    {

        public override int ContentLength
        {
            get
            {
                return (int)
                    this.InputStream.Length;
            }
        }

        public override string ContentType
        {
            get
            {
                return
                    this._ContentType;
            }
            //set { _ContentType = value; }
        }
        private string _ContentType;

        public override string FileName
        {
            get { return base.FileName; }
           // set { _FileName = value; }
        }
        private string _FileName;

        public override Stream InputStream
        {
            get
            {
                if (_Stream == null)
                {
                    _Stream = new FileStream(_FileName, FileMode.Open,
                   FileAccess.Read, FileShare.Read);
                }
                return _Stream;
            }
        }
        private FileStream _Stream;

        public HttpFile(string fileName, string contentType)
        {
            this._ContentType = contentType;
            this._FileName = fileName;
        } // HttpFile

        public void Dispose()
        {
            if (_Stream != null)
            {
                try { _Stream.Dispose(); }
                finally { _Stream = null; }
            }
        } // Dispose

        public override void SaveAs(String filename)
        {
            File.WriteAllBytes(filename, File.ReadAllBytes(_FileName));
        } // SaveAs

        public HttpFile(String fileName, String contentType, byte[]
 contentData)
        {
            this._ContentType = contentType;
            this._FileName = fileName;
            this._Stream = new FileStream(fileName, FileMode.Open,
           FileAccess.Read, FileShare.Read);
            _Stream.Write(contentData, 0, contentData.Length);
            _Stream.Close();
        } // 

    }

}