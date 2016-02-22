using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Artwork_App.ViewModels;
using System.Web.Script.Serialization;

namespace Artwork_App.Controllers
{
    public class VendorsController : Controller
    {
        // GET: Vendors
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult DoesThisVendorExist()
        {
            VendorViewModel ViewModle = new VendorViewModel();
            var vendorCount = 0;
            try
            {
                var resolveRequest = HttpContext.Request;
                resolveRequest.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                string jsonString = new System.IO.StreamReader(resolveRequest.InputStream).ReadToEnd();
                //deserialse
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string vendorName = serializer.Deserialize<string>(jsonString);
                using (VendorViewModel presentaion = new VendorViewModel())
                {
                    vendorCount = presentaion.GetData()
                                        .Where(x => x.VendorName.ToUpper().Equals(vendorName.ToUpper())).Count();
                    return new JsonResult
                    {
                        Data = new { Data = vendorCount, Success = true, ErrorMessage = "" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }

            }
            catch (Exception ex)
            {

                return new JsonResult
                {
                    Data = new { Data = vendorCount, Success = false, ErrorMessage = ex.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

        }
        public JsonResult GetAllVendors()
        {
            /*
             * This is the method used to get all vendors for  rebinding
             * */


            var col = new List<VendorViewModel>();
            using (VendorViewModel vm = new VendorViewModel())
            {
                col = (from p in vm.GetData()
                       
                       select p
                                ).ToList();

                var retVal = col.Select(c => new { label = c.VendorName, value = c.VendorID });
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult InsertNewVendor()
        {
            var vendor = new VendorViewModel();
            try
            {
                var resolveRequest = HttpContext.Request;
                resolveRequest.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                string jsonString = new System.IO.StreamReader(resolveRequest.InputStream).ReadToEnd();
                //deserialse
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string vendorName = serializer.Deserialize<string>(jsonString);
                using (VendorViewModel presentaion = new VendorViewModel())
                {

                    vendor.VendorName = vendorName;
                    var retVal = presentaion.Insert(vendor); ;
                    if (retVal >= 1)
                    {
                        //get the newly artist and show 

                        vendor = presentaion.GetObject(retVal);

                        return new JsonResult
                        {
                            Data = new { Data = vendor, Success = true, ErrorMessage = "Vendor was created" },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        return new JsonResult
                        {
                            Data = new { Data = vendor, Success = false, ErrorMessage = "Error in creating Vendor" },
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
                    Data = new { Data = vendor, Success = false, ErrorMessage = ex.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}