using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artwork_App.ViewModels
{
    public class PhotoViewImage
    {
        string name{get;set;}
        string alternatetext{get;set;}
        byte[] actualimage{get;set;}
        string contenttype { get; set; } 

     public  PhotoViewImage  GetByID(int id)
        {
         
            var q =new  PhotoViewImage();
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
                    q.actualimage = fileData;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return q;
        }
    }
}