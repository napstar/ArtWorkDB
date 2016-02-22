using Artwork_App.Extensions;
using Artwork_App.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using Artwork_App.CustomObjects;
using System.Net;

namespace Artwork_App.Controllers
{

    [Authorize(Roles = "information systems")]
    public class ArtWorksController : Controller
    {
        // GET: ArtWorks
        //public ActionResult Index()
        //{
        //    List<ArtworkViewModel> collection = new List<ArtworkViewModel>();
        //    try
        //    {
              

        //        using (ViewModels.ArtworkViewModel ViewModel = new ArtworkViewModel())
        //        {
        //            collection = ViewModel.GetData();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }

        //    return View(collection);
        //}
        
        public ActionResult GetDataPagination(int? Page_No)
        {

            List<ArtworkViewModel> collection = new List<ArtworkViewModel>();
            var pagedData = new PagedDataSet<ArtworkViewModel>();
            int pageSize = 10;
            try
            {


                using (ViewModels.ArtworkViewModel ViewModel = new ArtworkViewModel())
                {
                    var pageNumber = (Page_No.HasValue) ? ((Page_No.Value >= 1) ? Page_No.Value : 0) : 0;

                    pagedData = ViewModel.GetDataPagination(pageNumber);
                    //pick coilumns you need
                    var selectedData=(from p in pagedData.Items
                                      select new 
                                      {
                                          ArtWorkID=p.ArtWorkID
                                          ,ArtTitle=p.ArtTitle
                                          ,Location=p.CurrenTLocation.Value

                                      }
                                          ).ToList();
                    //    model = new PagedList<ArtworkViewModel>(collection, Page_No.HasValue?Page_No.Value:1, pageSize);
                    //    //collection.ToPagedList(pageValue, 10);
                    //    model.PageCount = ViewModel.GetData().Count();
                    //  obj = ViewModel.GetDataPagination(Page_No);
                  
                }

            }
            catch (Exception ex)
            {
               
                throw ex;
            }

            return View(pagedData.Items.ToPagedList(pageNumber: Page_No ?? 1, pageSize: 10));
        }
        public JsonResult GetAllVendors(string term)
        {
            /*
             * This is the method used to get all vendors for  rebinding
             * */


            var col = new List<VendorViewModel>();
            using (VendorViewModel vm = new VendorViewModel())
            {
                col = (from p in vm.GetData()
                       where p.VendorName.ToUpper().StartsWith(term.ToUpper().Trim())
                       select p
                                ).ToList();

                var retVal = col.Select(c => new { label = c.VendorName, value = c.VendorID });
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Index(int? Page_No)
        {
            List<ArtworkViewModel> collection = new List<ArtworkViewModel>();
            var pagedData = new PagedDataSet<ArtworkViewModel>();
            int pageSize = 10;
            try
            {


                using (ViewModels.ArtworkViewModel ViewModel = new ArtworkViewModel())
                {
                    var pageNumber = (Page_No.HasValue) ? ((Page_No.Value >= 1) ? Page_No.Value : 0) : 0;
                    pagedData = ViewModel.GetDataPagination(pageNumber);

                    //    model = new PagedList<ArtworkViewModel>(collection, Page_No.HasValue?Page_No.Value:1, pageSize);
                    //    //collection.ToPagedList(pageValue, 10);
                    //    model.PageCount = ViewModel.GetData().Count();
                  //  obj = ViewModel.GetDataPagination(Page_No);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return View(pagedData);
        } 
        [HttpGet]
        public ActionResult Create(ArtworkViewModel model)
        {
            
            ArtworkViewModel ViewModel = new ArtworkViewModel();
            
            ModelState.Clear();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(ArtworkViewModel model, HttpPostedFileBase file)
        {
            var newID = 0;
            HttpPostedFileBase uploadfile = Request.Files["upload"];
            var viewModel = new ArtworkViewModel();
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    //image cannot be length cannot be more than
                    byte[] pic = this.ConvertToBytes(model.Image);
                    model.Photo= pic ;
                    string extension = System.IO.Path.GetExtension(model.Image.FileName);
                    if (extension == ".png" || extension.Equals(".jpeg") || extension.Equals(".jpg"))
                    {
                        model.fileExtension = extension;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid File formats:Only png,jpeg,jpg are allowed");
                        throw new Exception("Invalid File formats:Only png,jpeg,jpg are allowed");
                    }
                }


                newID = model.Insert(model);

                if (newID > 0)
                {
                    using (ArtworkViewModel ArtWorkViewMod = new ArtworkViewModel())
                    {
                        viewModel = ArtWorkViewMod.GetObject(newID);
                        return RedirectToAction("Edit", new { id = newID });
                    }
                }
            }
            else
            {
                //model errors
                  ModelState.AddModelError("","There are form errors; please correct them to continue!");
              
            }
            return View(model);
           
        }
        public static List<string> GetErrorListFromModelState(ModelStateDictionary modelState)
        {
            var query = from state in modelState.Values
                        from error in state.Errors
                        select error.ErrorMessage;

            var errorList = query.ToList();
            return errorList;
        }
        [HttpGet]
        public ActionResult Edit(Int32? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                ArtworkViewModel ViewModel = new ArtworkViewModel();
                if (id.HasValue)
                {

                    ViewModel = ViewModel.GetObject(id.Value);
                }
                //does modle state have any errors


                return View(ViewModel);
            }
            else
            {
                return RedirectToAction("Index", "ArtWorks");
            }
          
        }
        [HttpPost]
        public ActionResult Edit(ArtworkViewModel model)
        {
            var id = 0;
            var viewModel = new ArtworkViewModel();
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    //image cannot be length cannot be more than
                    byte[] pic = this.ConvertToBytes(model.Image);

                    string extension = System.IO.Path.GetExtension(model.Image.FileName);
                    if (extension == ".png" || extension.Equals(".jpeg") || extension.Equals(".jpg"))
                    {
                        model.fileExtension = extension;

                        model.Photo = pic;
                        id = model.Update(model);

                        if (id > 0)
                        {
                            using (ArtworkViewModel ArtWorkViewMod = new ArtworkViewModel())
                            {
                                viewModel = ArtWorkViewMod.GetObject(model.ArtWorkID);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Image", "Invalid File formats:Only png,jpeg,jpg are allowed");
                        //throw new Exception("Invalid File formats:Only png,jpeg,jpg are allowed");
                    }
                }
                else
                {
                    id = model.Update(model);

                    if (id > 0)
                    {
                        using (ArtworkViewModel ArtWorkViewMod = new ArtworkViewModel())
                        {
                            viewModel = ArtWorkViewMod.GetObject(model.ArtWorkID);
                        }
                    }
                }

                //map

              
            }
            else
            {
                //model errors

                ModelState.AddModelError("", "There are form errors; please correct them to continue!");
            }

          //  return RedirectToAction("Edit", new { id = model.ArtWorkID });
            return View(model);
        }
        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {

            byte[] imageBytes = null;

            System.IO.BinaryReader reader = new System.IO.BinaryReader(image.InputStream);

            imageBytes = reader.ReadBytes((int)image.ContentLength);

            return imageBytes;

        }

        public JsonResult GetAllArtists(string term)
        {
            /*
             * This is the method used to get all artists for the auto complete
             * */
            var col = new List<ArtistViewModel>();
            using (ArtistViewModel vm = new ArtistViewModel())
            {
                
                  col = (from p in vm.GetData()
                         where p.ArtistName.ToUpper().Trim().StartsWith(term.ToUpper().Trim())
                                  select p
                                  ).ToList();

                  var retVal = col.Select(c => new { label = c.ArtistName, value = c.ArtistID });
                 return Json(retVal, JsonRequestBehavior.AllowGet);
            }

            return Json(col, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Save(ArtworkViewModel model, HttpPostedFileBase file)
        {
            var id = 0;
            var viewModel = new ArtworkViewModel();
            if (ModelState.IsValid)
            {
                if (model.Image!=null)
                {
                     byte[] pic=this.ConvertToBytes(model.Image);
                    
                    string extension = System.IO.Path.GetExtension(model.Image.FileName);
                    if (extension == ".png" || extension.Equals(".jpeg") || extension.Equals(".jpg"))
                    {
                        model.fileExtension = extension;
                        viewModel = model;
                        viewModel.Photo = pic;
                    }
                    else
                    {
                        throw new Exception("Invalid File formats:Only png,jpeg,jpg are allowed");
                    }
                }



                id = model.Update(viewModel);
                
                if (id > 0)
                {
                    using (ArtworkViewModel ArtWorkViewMod = new ArtworkViewModel())
                    {
                        viewModel = ArtWorkViewMod.GetObject(model.ArtWorkID);
                    }
                }
            }
            else
            {
               //model errors
                
                  ModelState.AddModelError("","There are form errors; please correct them to continue!");
            }

            return RedirectToAction("Edit", new { id = model.ArtWorkID });
        }
        public FileResult  GetImageFromDataBase(int id)
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
                }
            }
            catch (Exception)
            {

                throw;
            }


            return File(fileData, "image/jpg"); ;
        }

        [HttpGet]
        public FileContentResult GetImg(int id)
        {
            try
            {
                byte[] fileData = null;
                using (ArtworkViewModel vm = new ArtworkViewModel())
                {
                    using (var c = new ArtWorkDBEntities1())
                    {
                        fileData = (from p in c.Artworks
                                    where p.ArtworkID == id
                                    select p.Image).FirstOrDefault();
                        var fileExtension = (from p in c.Artworks
                                             where p.ArtworkID == id
                                             select p.FileExtension).FirstOrDefault();
                    }
                    FileContentResult varFile = new FileContentResult(fileData, "image/octet-stream");
                    return varFile;
                    //   return new FileContentResult(fileData, "application/octet-stream", 1);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
           
           
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ShowPhoto(Int32 id)
        {
            //This is my method for getting the image information
            // including the image byte array from the image column in
            // a database.
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
                }
            }
            catch (Exception)
            {

                throw;
            }
            //As you can see the use is stupid simple.  Just get the image bytes and the
            //  saved content type.  See this is where the contentType comes in real handy.
            ImageResult result = new ImageResult(fileData, "jpg");

            return result;
        }

        public JsonResult GetAllCountries(string term)
        {
            /*
             * This is the method used to get all artists for  rebinding
             * */
            var col = new List<CountryViewModel>();
            using (CountryViewModel vm = new CountryViewModel())
            {
                col = (from p in vm.GetData().Take(5)
                         where p.CountryName.ToUpper().Trim().StartsWith(term.ToUpper().Trim())
                       select p
                                ).ToList();

                var retVal = col.Select(c => new { label = c.CountryName, value = c.CountryID });
                return Json(retVal, JsonRequestBehavior.AllowGet);
            }

            return Json(col, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllLocations(string term)
        {

            var col = new List<CurrentLocationViewModel>();
            using (CurrentLocationViewModel location = new CurrentLocationViewModel ())
            {
                col=(from p in location.GetData()
                         where p.LocationName.ToUpper().StartsWith(term.ToUpper().Trim())
                         select p
                         ).ToList();

                //converet to item and label
                var retVal = col.Select(c => new {label=c.LocationName,value=c.LocationID });

                return Json(retVal, JsonRequestBehavior.AllowGet);
            }
            return Json(col, JsonRequestBehavior.AllowGet);
        }

 
       public ActionResult LogOut()
        {
            

             return RedirectToAction("Index", "Artworks");
        }
    }
}