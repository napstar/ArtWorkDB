using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Artwork_App.ViewModels;
using System.Web.Script.Serialization;

namespace Artwork_App.Controllers
{
    public class CurrentLocationController : Controller
    {
        // GET: CurrentLocation
        public ActionResult Index()
        {
            return View();
        }



        public JsonResult DoesLocationExist()
        {

            CurrentLocationViewModel ViewModel = new CurrentLocationViewModel();
            var locationCount = 0;
            try
            {
                //DESERIALIZE
                var resolveRequest = HttpContext.Request;
                resolveRequest.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                string jsonString = new System.IO.StreamReader(resolveRequest.InputStream).ReadToEnd();
                //deserialse
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string locationName = serializer.Deserialize<string>(jsonString);
                using (CurrentLocationViewModel presentation= new CurrentLocationViewModel ())
                {
                    locationCount = presentation.GetData()
                                              .Where(p => p.LocationName.ToUpper().Trim().StartsWith(locationName.ToUpper().Trim())).Count()
                                              ;
                    return new JsonResult
                    {
                        Data = new { Data = locationCount, Success = true, ErrorMessage = "" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            catch (Exception ex)
            {

                return new JsonResult
                {
                    Data = new { Data = locationCount, Success = false, ErrorMessage = ex.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

        }

        public JsonResult InsertNewLocation()
        {
            var location = new CurrentLocationViewModel();
            try
            {
                 var resolveRequest = HttpContext.Request;
                resolveRequest.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                string jsonString = new System.IO.StreamReader(resolveRequest.InputStream).ReadToEnd();
                //deserialse
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string locationName = serializer.Deserialize<string>(jsonString);

                using (CurrentLocationViewModel presentation= new CurrentLocationViewModel ())
                {
                    location.LocationName = locationName;
                    var retVal=presentation.Insert(location);
                    if (retVal>=1)
                    {
                        //get the newly inserted location
                        var newInsertedLocation = new CurrentLocationViewModel();
                        newInsertedLocation = presentation.GetObject(retVal);
                        return new JsonResult
                        {
                            Data = new { Data = newInsertedLocation, Success = true, ErrorMessage = "Artist was created" },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        //insert falied
                        return new JsonResult
                        {
                            Data = new { Data = location, Success = false, ErrorMessage = "Error in creating Location" },
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
                    Data = new { Data = location, Success = false, ErrorMessage = ex.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public JsonResult GetAllLocations(string term)
        {
            /*
             * This is the method used to get all locations for  rebinding
             * */
            var col = new List<CurrentLocationViewModel>();
            using (CurrentLocationViewModel vm = new CurrentLocationViewModel())
            {
                col = (from p in vm.GetData()
                       where p.LocationName.Trim().ToUpper().StartsWith(term.ToUpper().Trim())
                       select p
                                ).ToList();

                var retVal = col.Select(c => new { label = c.LocationName, value = c.LocationID  });
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }

            
        }
    }
}