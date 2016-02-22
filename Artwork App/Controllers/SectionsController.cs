using Artwork_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Artwork_App.Controllers
{
    public class SectionsController : Controller
    {
        // GET: Sections
        public ActionResult Index()
        {
            return View();
        }

          public JsonResult DoesSectionExist()
        { 
              
              Int32 sectionCount = 0;
            try
            {
              
                var resolveRequest = HttpContext.Request;
                resolveRequest.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                string jsonString = new System.IO.StreamReader(resolveRequest.InputStream).ReadToEnd();
                //deserialse
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string sectioName = serializer.Deserialize<string>(jsonString);
                if (!string.IsNullOrEmpty(sectioName))
                {
                    using (Artwork_App.ViewModels.SectionsViewModel SectionsViewModel = new ViewModels.SectionsViewModel())
                    {
                        sectionCount = SectionsViewModel.GetData().Where(x => x.SectionName == sectioName).Count();


                        return new JsonResult
                        {
                            Data = new { Data = sectionCount, Success = true, ErrorMessage = "" },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                }
                
               
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return new JsonResult
            {
                Data = new { Data = sectionCount, Success = true, ErrorMessage = "" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

          public JsonResult InsertSection()
          {

              Int32 retval = 0;
              try
              {

                  var resolveRequest = HttpContext.Request;
                  resolveRequest.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                  string jsonString = new System.IO.StreamReader(resolveRequest.InputStream).ReadToEnd();
                  //deserialse
                  JavaScriptSerializer serializer = new JavaScriptSerializer();
                  string sectioName = serializer.Deserialize<string>(jsonString);
                  if (!string.IsNullOrEmpty(sectioName))
                  {
                      using (Artwork_App.ViewModels.SectionsViewModel SectionsViewModel = new ViewModels.SectionsViewModel())
                      {

                          SectionsViewModel.SectionName = sectioName;
                          retval = SectionsViewModel.Insert(SectionsViewModel);
                          
                          

                          return new JsonResult
                          {
                              Data = new { Data = retval, Success = true, ErrorMessage = "" },
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
                      Data = new { Data = retval, Success = true, ErrorMessage =ex.Message },
                      ContentEncoding = System.Text.Encoding.UTF8,
                      JsonRequestBehavior = JsonRequestBehavior.AllowGet
                  };
              }
              return new JsonResult
              {
                  Data = new { Data = retval, Success = false, ErrorMessage = "" },
                  ContentEncoding = System.Text.Encoding.UTF8,
                  JsonRequestBehavior = JsonRequestBehavior.AllowGet
              };
          }

            public JsonResult GetAllSections(string term)
          {
              var col = new List<SectionsViewModel>();
              using (SectionsViewModel vm = new SectionsViewModel())
              {
                  col = (from p in vm.GetData()
                         where p.SectionName.ToUpper().StartsWith(term.ToUpper().Trim())
                         select p
                                  ).ToList();

                  var retVal = col.Select(c => new { label = c.SectionName, value = c.SectionID });
                  return Json(retVal, JsonRequestBehavior.AllowGet);
              }
          }
            public JsonResult RepopulateSections()
            {
                var col = new List<SectionsViewModel>();
                using (SectionsViewModel vm = new SectionsViewModel())
                {
                    col = (from p in vm.GetData()
                           
                           select p
                                    ).ToList();

                    var retVal = col.Select(c => new { label = c.SectionName, value = c.SectionID });
                    return Json(retVal, JsonRequestBehavior.AllowGet);
                }
            }
    }
}