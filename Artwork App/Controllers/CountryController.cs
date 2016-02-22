using Artwork_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Artwork_App.Controllers
{
    public class CountryController : Controller
    {
        // GET: Country
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllCountries(string term)
        {
            /*
             * This is the method used to get all artists for  rebinding
             * */
            var col = new List<CountryViewModel>();
            using (CountryViewModel vm = new CountryViewModel())
            {
                col = (from p in vm.GetData()
                       where p.CountryName.Trim().ToUpper().StartsWith(term.ToUpper())
                       select p
                                ).ToList();

                var retVal = col.Select(c => new { label = c.CountryName, value = c.CountryID });
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }

            return Json(col, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertNewCountry()
        {
            var country = new CountryViewModel();
            try
            {
                var resolveRequest = HttpContext.Request;
                resolveRequest.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                string jsonString = new System.IO.StreamReader(resolveRequest.InputStream).ReadToEnd();
                //deserialse
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string countryname = serializer.Deserialize<string>(jsonString);
                using (CountryViewModel presentaion = new CountryViewModel())
                {

                    country.CountryName = countryname;
                    var retVal = presentaion.Insert(country); ;
                    if (retVal >= 1)
                    {
                        //get the newly artist and show 

                        country = presentaion.GetObject(retVal);

                        return new JsonResult
                        {
                            Data = new { Data = country, Success = true, ErrorMessage = "Country was created" },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        return new JsonResult
                        {
                            Data = new { Data = country, Success = false, ErrorMessage = "Error in creating Country" },
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
                    Data = new { Data = country, Success = false, ErrorMessage = ex.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public JsonResult DoesThisCountryExist()
        {
            CountryViewModel ViewModle = new CountryViewModel();
            var countryCount = 0;
            try
            {
                var resolveRequest = HttpContext.Request;
                resolveRequest.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                string jsonString = new System.IO.StreamReader(resolveRequest.InputStream).ReadToEnd();
                //deserialse
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string countryname = serializer.Deserialize<string>(jsonString);
                using (CountryViewModel presentaion = new CountryViewModel())
                {
                    countryCount = presentaion.GetData()
                                        .Where(x => x.CountryName.ToUpper().Equals(countryname.ToUpper())).Count();
                    return new JsonResult
                    {
                        Data = new { Data = countryCount, Success = true, ErrorMessage = "" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }

            }
            catch (Exception ex)
            {

                return new JsonResult
                {
                    Data = new { Data = countryCount, Success = false, ErrorMessage = ex.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

        }
    }
}