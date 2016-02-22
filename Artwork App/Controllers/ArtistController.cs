using Artwork_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Artwork_App.Controllers
{
    public class ArtistController : Controller
    {
        // GET: Artist
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult DoesThisArtistExist()
        {
            ArtistViewModel ViewModle = new ArtistViewModel();
            var artistcount = 0;
            try
            {
                var resolveRequest = HttpContext.Request;
                resolveRequest.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                string jsonString = new System.IO.StreamReader(resolveRequest.InputStream).ReadToEnd();
                //deserialse
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string artistname = serializer.Deserialize<string>(jsonString);
                using (ArtistViewModel presentaion = new ArtistViewModel())
                {
                   
                    artistcount = presentaion.GetData()
                                        .Where(x => x.ArtistName.ToUpper().Equals(artistname.ToUpper())).Count();
                    return new JsonResult
                    {
                        Data = new { Data = artistcount, Success = true, ErrorMessage = "" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                
            }
            catch (Exception ex)
            {

                return new JsonResult
                {
                    Data = new { Data = artistcount, Success = false,ErrorMessage=ex.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

        }
        public JsonResult GetAllArtists()
        {
            /*
             * This is the method used to get all artists for  rebinding
             * */
            var col = new List<ArtistViewModel>();
            using (ArtistViewModel vm = new ArtistViewModel())
            {
                col = (from p in vm.GetData().OrderByDescending(x=>x.ArtistID)
                      
                       select p
                                ).ToList();

                var retVal = col.Select(c => new { label = c.ArtistName, value = c.ArtistID });
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }

            return Json(col, JsonRequestBehavior.AllowGet);
        }

        public   JsonResult InsertNewRecordNO_EF()
        {
            var artist = new ArtistViewModel();
            try
            {
                var resolveRequest = HttpContext.Request;
                resolveRequest.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                string jsonString = new System.IO.StreamReader(resolveRequest.InputStream).ReadToEnd();
                //deserialse
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string artistname = serializer.Deserialize<string>(jsonString);
                using (ArtistViewModel presentaion = new ArtistViewModel())
                {

                    artist.ArtistName = artistname;
                    var retVal = 0; ;
                    string connectionString = ConfigurationManager.ConnectionStrings["ArtWorkDBConnectionString"].ToString();
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        using (SqlCommand command = new SqlCommand("INSERT INTO [dbo].[Artist] ([Artist]) output INSERTED.ArtistID  VALUES(@param)", con))
                        {
                            command.Parameters.Add("@param", System.Data.SqlDbType.VarChar, 50).Value = artist.ArtistName;
                        // retVal=   command.ExecuteNonQuery();

                            retVal = (int)command.ExecuteScalar();
                        }

                    }
                    if (retVal >= 1)
                    {
                        //get the newly artist and show 

                        artist = presentaion.GetObject(retVal);

                        return new JsonResult
                        {
                            Data = new { Data = artist, Success = true, ErrorMessage = "Artist was created" },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        return new JsonResult
                        {
                            Data = new { Data = artist, Success = false, ErrorMessage = "Error in creating Artist" },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                }
            }
            catch (Exception ex)
            {

                return new JsonResult
                {
                    Data = new { Data = artist, Success = false, ErrorMessage = ex.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        public JsonResult GetAllArtistsRefresh()
        {
            /*
             * This is the method used to get all artists for  rebinding
             * */
               var col = new List<ArtistViewModel>();
               string connectionString = ConfigurationManager.ConnectionStrings["ArtWorkDBConnectionString"].ToString();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("SELECT [ArtistID],[Artist] FROM [dbo].[Artist]  order by ArtistID desc", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                  
                                       // var dataTable = new System.Data.DataTable();
                                       // dataTable.Load(reader);
                                        while (reader.Read())
                                        {
                                          var m= new ArtistViewModel();
                                            m.ArtistID=reader.GetInt32(0);
                                            m.ArtistName = reader.GetString(1);
                                           col.Add(m);
                                           // reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                                        }
                                    }
                }
                
            }
         
            //using (ArtistViewModel vm = new ArtistViewModel())
            //{
            //    col = (from p in vm.GetData()

            //           select p
            //                    ).ToList();

            //    var retVal = col.Select(c => new { label = c.ArtistName, value = c.ArtistID });
            //    return Json(retVal, JsonRequestBehavior.AllowGet);
            //}
            var retVal = col.Select(c => new { label = c.ArtistName, value = c.ArtistID });
            return Json(retVal, JsonRequestBehavior.AllowGet);
           // return Json(col, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertNewArtist()
        {
            var artist = new ArtistViewModel();
            try
            {
                var resolveRequest = HttpContext.Request;
                resolveRequest.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                string jsonString = new System.IO.StreamReader(resolveRequest.InputStream).ReadToEnd();
                //deserialse
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string artistname = serializer.Deserialize<string>(jsonString);
                using (ArtistViewModel presentaion = new ArtistViewModel())
                {

                    artist.ArtistName = artistname;
                    var retVal = presentaion.Insert(artist); ;
                    if (retVal >= 1)
                    {
                        //get the newly artist and show 

                        artist = presentaion.GetObject(retVal);

                        return new JsonResult
                        {
                            Data = new { Data = artist, Success = true, ErrorMessage = "Artist was created" },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        return new JsonResult
                        {
                            Data = new { Data = artist, Success = false, ErrorMessage = "Error in creating Artist" },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                }
            }
            catch (Exception ex)
            {

                return new JsonResult
                {
                    Data = new { Data = artist, Success = false, ErrorMessage = ex.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}