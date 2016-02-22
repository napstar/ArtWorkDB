using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using model = Artwork_App.ArtWorkDBEntities1;
using PagedList.Mvc;


namespace Artwork_App.ViewModels
{
    public class ArtworkViewModel : Repository.IRepository<ArtworkViewModel>, Repository.IMapper<ArtworkViewModel, Artwork>, IDisposable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArtWorkID { get; set; }
        
        [Required(ErrorMessage = "You must provide an Asset Number")]
        [StringLength(50)]
        public string  AssetNumber { get; set; }

         [DataType(DataType.MultilineText)]
         [Display(Name = "Title Of Art")]
        [Required(ErrorMessage = "You must provide an Art Title")]
        [StringLength(50)]
        public string ArtTitle { get; set; }

        [Required(ErrorMessage = "You must select an Art Type")]
        [Display(Name = "Type Of Art")]
        public Nullable<int> ArtTypeID { get; set; }


         [Required(ErrorMessage = "You must select an Artist")]
         [Display(Name = "Artist")]
        public Nullable<int> ArtistID { get; set; }

        [Required(ErrorMessage = "You must select an Country")]
        [Display(Name = "Country of orgin")]
        public Nullable<int> CountryID { get; set; }

         [Required(ErrorMessage = "You must select an Vendor")]
        public Nullable<int> VendorID { get; set; }

        [Required(ErrorMessage = "You must provide an Art Creation Date")]
        [Display(Name = "Art Creation Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateCreated { get; set; }


        [Required(ErrorMessage = "You must provide a Purchase Date")]
        [Display(Name = "Date of Purchase")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DatePurchased { get; set; }

        [Required(ErrorMessage = "You must select a Location")]
     
        [Display(Name = "Location")]
        public Nullable<int> CurrenTLocation { get; set; }

        [Required(ErrorMessage = "You must select a Level")]
        [Display(Name = "Level")]
        public Nullable<int> LevelID { get; set; }

        public string fileExtension{ get; set; }
       // public byte[] Image { get; set; }
        public System.Web.HttpPostedFileBase Image { get; set; }

        [RegularExpression(@"^.*\.(jpg|jpeg|png)$",
ErrorMessage = "Please use an image with an extension of .jpg, .jpeg")]
        public byte[] Photo { get; set; }

        [Required(ErrorMessage = "You must provide a Physical Dimension")]
        [StringLength(50)]
        public string PhysicalDimension { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }
            [Required(ErrorMessage = "You must select a Location")]
            
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [Display(Name = "Purchased Price")]
        public decimal PurchasePrice { get; set; }

            [Required(ErrorMessage = "You must select an Section")]
            [Display(Name = "Section")]
            public Nullable<int> SectionID { get; set; }


        //audit
            public Nullable<DateTime> RecordCreationDate { get; set; }
            public Nullable<DateTime> RecordLastUpdateDate { get; set; }



        private Artwork_App.ArtWorkDBEntities1 context;
        private bool disposed = false;
       
        //colections

        public List<ArtistViewModel> Artists { get; set; }
        public List<ArtTypeViewModel> ArtTypes { get; set; }
          public List<CountryViewModel> Countries { get; set; }
          public List<CurrentLocationViewModel> CurrentLocations { get; set; }
          public List<LevelsViewModel> Levels { get; set; }
          public List<SectionsViewModel> Sections { get; set; }
          public List<VendorViewModel> Vendors { get; set; }

        public  ArtworkViewModel()
          {
              using (ArtistViewModel  aristVM= new ArtistViewModel ())
              {
                  this.Artists = aristVM.GetData();
              }
              using (ArtTypeViewModel ArtTyVM= new ArtTypeViewModel ())
              {
                  this.ArtTypes = ArtTyVM.GetData();
              }

              using (CountryViewModel CountriesVM= new CountryViewModel ())
              {
                  this.Countries = CountriesVM.GetData();
              }

              using (CurrentLocationViewModel CLVM= new CurrentLocationViewModel ())
              {
                  this.CurrentLocations = CLVM.GetData();
              }
              using (LevelsViewModel L= new LevelsViewModel ())
              {
                  this.Levels=L.GetData();
              }
              using (VendorViewModel v= new VendorViewModel ())
              {
                  this.Vendors = v.GetData();
              }
              using (SectionsViewModel SVM= new SectionsViewModel ())
              {
                  this.Sections = SVM.GetData();
              }
          }

        public int Insert(ArtworkViewModel obj)
        {  Int32 retVal = 0;
            using (this.context= new Artwork_App.ArtWorkDBEntities1() )
            {
                Artwork DataModel = new Artwork();
                
                DataModel = this.MapPresentationObjectToDataObject(obj);
                //auidt 
                //set insert date
                DataModel.RecordCreationDate = DateTime.Now;

                if (DataModel.Image==null)
                {
                    this.context.Artworks.Add(DataModel);
                   // this.context.Entry(DataModel).State = System.Data.Entity.EntityState.Modified;
                    this.context.Entry(DataModel).Property(x => x.Image).IsModified = false;
                    int rowsAffected = context.SaveChanges();
                    retVal = DataModel.ArtworkID;
                }
                else
                {
                    this.context.Artworks.Add(DataModel);
                    int rowsAffected = context.SaveChanges();
                    retVal = DataModel.ArtworkID;
                }
             
            }
          
           

            return retVal;
        }

        public int Update(ArtworkViewModel obj)
        {
            
            Int32 rowsAffected = 0;
            try
            {
                if (obj.Photo!=null)
                {
                    using (this.context = new Artwork_App.ArtWorkDBEntities1())
                    {
                        Artwork DataObject = new Artwork();
                        //map and update
                        DataObject = this.MapPresentationObjectToDataObject(obj);
                        //set edit date
                        DataObject.RecordLastUpdateDate = DateTime.Now;
                        this.context.Artworks.Attach(DataObject);
                        this.context.Entry(DataObject).State = System.Data.Entity.EntityState.Modified;
                       
                        rowsAffected = this.context.SaveChanges();
                    }
                }
                else
                {
                    using (this.context = new Artwork_App.ArtWorkDBEntities1())
                    {
                        Artwork DataObject = new Artwork();
                        //map and update
                        DataObject = this.MapPresentationObjectToDataObject(obj);
                        //set edit date
                        //set edit date
                        DataObject.RecordLastUpdateDate = DateTime.Now;
                        context.Artworks.Attach(DataObject);
                       
                        this.context.Entry(DataObject).State = System.Data.Entity.EntityState.Modified;
                        this.context.Entry(DataObject).Property(x => x.Image).IsModified = false;
                        rowsAffected = this.context.SaveChanges();
                    }
                }
               
            }
             catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                foreach (var failure in ex.EntityValidationErrors) {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors) {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new System.Data.Entity.Validation.DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" + 
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
            //catch (Exception ex)
            //{
                
            //    throw new Exception( ex.Message);
            //}
            
          
          
            return rowsAffected;
        }

        public int UpdateParams(ArtworkViewModel entity, params System.Linq.Expressions.Expression<Func<ArtworkViewModel, object>>[] properties)
        {
            throw new NotImplementedException();
        }

        public int Delete(ArtworkViewModel obj)
        {
            throw new NotImplementedException();

        }

        public ArtworkViewModel GetObject(int id)
        {
             using (this.context = new model())
            {
                Artwork dataObj =new  Artwork();

                dataObj = this.context.Artworks.Find(id);
                using (ArtworkViewModel presentaion= new ArtworkViewModel ())
                {
                       return this.MapDataObjectToPresentation(presentaion,dataObj);
                }
            }
        }
        public PagedDataSet<ArtworkViewModel> GetDataPagination(int? pageNumber)
        {
            int numberOfObjectsPerPage = 10;
            PagedDataSet<ArtworkViewModel> obj = new PagedDataSet<ArtworkViewModel>();
            var page_NO = (pageNumber.HasValue)?((pageNumber.Value>=1)?pageNumber.Value:0):0;
            using (this.context= new model ())
            {
                if (pageNumber>=1)
                {
                    var dataObjList = (this.context.Artworks.OrderBy(p => p.ArtworkID).Skip(numberOfObjectsPerPage * (page_NO)).Take(numberOfObjectsPerPage).ToList());


                    obj.PageTotal = (from p in this.context.Artworks
                                     select p
                                       ).Count();


                    var collection = new List<ArtworkViewModel>();
                    foreach (var item in dataObjList)
                    {
                        var presentationObj = new ArtworkViewModel();
                        presentationObj = this.MapDataObjectToPresentation(presentationObj, item);
                        collection.Add(presentationObj);
                    }
                    if (collection.Count >= 1)
                    {
                        obj.Items = (IEnumerable<ArtworkViewModel>)collection;
                    }
                }
                else
                {
                    var dataObjList = (this.context.Artworks.OrderBy(p => p.ArtworkID).ToList());
                    obj.PageTotal = (from p in this.context.Artworks
                                     select p
                                    ).Count();


                    var collection = new List<ArtworkViewModel>();
                    foreach (var item in dataObjList)
                    {
                        var presentationObj = new ArtworkViewModel();
                        presentationObj = this.MapDataObjectToPresentation(presentationObj, item);
                        collection.Add(presentationObj);
                    }
                    if (collection.Count >= 1)
                    {
                        obj.Items = (IEnumerable<ArtworkViewModel>)collection;
                    }
                }
              
                return obj;
            }
           

          
        }

        //public List<ArtworkViewModel> GetDataPagination(int? pageNumber)
        //{
        //    int numberOfObjectsPerPage = 5;
        //    List<ArtworkViewModel> col = new List<ArtworkViewModel>();
        //    using (this.context = new model())
        //    {
        //        if (pageNumber.HasValue)
        //        {
        //            if (pageNumber.Value>=1)
        //            {
        //                var dataObjList = (from p in this.context.Artworks
        //                             .OrderByDescending(p => p.ArtworkID)
        //                           .Skip(pageNumber.Value * numberOfObjectsPerPage)
        //                           .Take(numberOfObjectsPerPage)
        //                                   select p
        //                         ).ToList();

        //                foreach (var item in dataObjList)
        //                {
        //                    var presentationObj = new ArtworkViewModel();
        //                    presentationObj = this.MapDataObjectToPresentation(presentationObj, item);
        //                    col.Add(presentationObj);
        //                }
        //            }
        //            else
        //            {
        //                var dataObjList = (from p in this.context.Artworks
        //                               .OrderByDescending(p => p.ArtworkID)
        //                         .Skip(0)
        //                         .Take(numberOfObjectsPerPage)
        //                                   select p
        //                       ).ToList();
        //                foreach (var item in dataObjList)
        //                {
        //                    var presentationObj = new ArtworkViewModel();
        //                    presentationObj = this.MapDataObjectToPresentation(presentationObj, item);
        //                    col.Add(presentationObj);
        //                }
        //            }
                  
        //        }
        //        else
        //        {
        //            var dataObjList = (from p in this.context.Artworks
        //                              .OrderByDescending(p => p.ArtworkID)
        //                        .Skip(0)
        //                        .Take(numberOfObjectsPerPage)
        //                               select p
        //                      ).ToList();
        //            foreach (var item in dataObjList)
        //            {
        //                var presentationObj = new ArtworkViewModel();
        //                presentationObj = this.MapDataObjectToPresentation(presentationObj, item);
        //                col.Add(presentationObj);
        //            }
        //        }
               
              
        //    }

        //    return col;

        //}
        public List<ArtworkViewModel> GetData()
        {
            List<ArtworkViewModel> artWorkList = new List<ArtworkViewModel>();
            using (this.context= new model ())
            {
                var collection = (from p in this.context.Artworks
                                
                                  select p).ToList();
                foreach (Artwork item in collection)
                {
                    using (ArtworkViewModel i = new ArtworkViewModel())
                    {
                          i.MapDataObjectToPresentation(i, item);
                          artWorkList.Add(i);
                    }
                }
            }

            return artWorkList;
        }
        public byte[] ConvertToBytes(System.Web.HttpPostedFileBase image)
        {

            byte[] imageBytes = null;

            System.IO.BinaryReader reader = new System.IO.BinaryReader(image.InputStream);

            imageBytes = reader.ReadBytes((int)image.ContentLength);

            return imageBytes;

        }
        
        public ArtworkViewModel MapDataObjectToPresentation(ArtworkViewModel presentationViewModelObject, Artwork DataModel)
        {
            presentationViewModelObject.ArtWorkID = DataModel.ArtworkID;
            presentationViewModelObject.AssetNumber = DataModel.AssetNumber;
            presentationViewModelObject.ArtTitle = DataModel.ArtTitle;
            presentationViewModelObject.ArtTypeID = DataModel.ArtTypeID;
            presentationViewModelObject.ArtistID = DataModel.ArtistID;
             presentationViewModelObject.CountryID = DataModel.CountryID;
             presentationViewModelObject.VendorID = DataModel.VendorID;
             presentationViewModelObject.DateCreated = DataModel.DateCreate;
             presentationViewModelObject.DatePurchased = DataModel.DatePurchased;
              presentationViewModelObject.CurrenTLocation=DataModel.CurrentLocationID;
           // presentationViewModelObject.Photo =DataModel.Image;
            //if ( presentationViewModelObject.Photo!=null)
            //{
            //    Extensions.HttpFile f = new Extensions.HttpFile("", "", presentationViewModelObject.Photo);
            //    presentationViewModelObject.Image = f;
            //}
            if ( DataModel.PurchasePrice.HasValue)
            {
                presentationViewModelObject.PurchasePrice = DataModel.PurchasePrice.Value; 
            }
           
            //presentationViewModelObject.Image = DataModel.Image;
              //if (DataModel.Image != null)
              //{
              //    //System.IO.Stream strm;
              //    //strm = presentationViewModelObject.Image.InputStream;
              //    //strm.Read(DataModel.Image, 0, DataModel.Image);
              //    //System.IO.BinaryReader b = new System.IO.BinaryReader(presentationViewModelObject.Image.InputStream);
              //    //byte[] binData = b.ReadBytes(presentationViewModelObject.Image.ContentLength);
              //    //presentationViewModelObject.Image = binData;

              //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
              //    {

              //        var imageData = DataModel.i;
              //        var length = Convert.ToInt32(imageData.Length);
              //        byte[] tempImage = new byte[length];
              //        imageData.Read(tempImage, 0, length);

              //        presentationViewModelObject.Image = tempImage;


              //    }
                 
              //    string extension = System.IO.Path.GetExtension(presentationViewModelObject.Image.FileName);
              //    DataModel.FileExtension = extension;
              //}
            if (DataModel.Image!=null)
            {
                presentationViewModelObject.Photo = DataModel.Image;

            }
            if (DataModel.Levels.HasValue)
            {
                presentationViewModelObject.LevelID = DataModel.Levels.Value;
            }

            if (DataModel.Section.HasValue)
            {
                presentationViewModelObject.SectionID = DataModel.Section.Value;
            }
         
            presentationViewModelObject.PhysicalDimension=DataModel.PhysicalDimension;
            presentationViewModelObject.Comments=DataModel.Comments;
            if (!string.IsNullOrEmpty(DataModel.FileExtension))
            {
                presentationViewModelObject.fileExtension = DataModel.FileExtension;
            }

            //audit
            if ( DataModel.RecordCreationDate.HasValue)
            {
                presentationViewModelObject.RecordCreationDate = DataModel.RecordCreationDate.Value;

                
            }

            if (DataModel.RecordLastUpdateDate.HasValue)
            {
                presentationViewModelObject.RecordLastUpdateDate = DataModel.RecordLastUpdateDate.Value;
            }
            return presentationViewModelObject;



        }
        

        public Artwork MapPresentationObjectToDataObject(ArtworkViewModel presentationViewModelObject)
        {
            Artwork DataModel = new Artwork();

            DataModel.ArtworkID = presentationViewModelObject.ArtWorkID;


            DataModel.AssetNumber = presentationViewModelObject.AssetNumber;


            DataModel.ArtTitle = presentationViewModelObject.ArtTitle;
            DataModel.ArtTypeID = presentationViewModelObject.ArtTypeID;


            DataModel.ArtistID = presentationViewModelObject.ArtistID;
            DataModel.CountryID = presentationViewModelObject.CountryID;
            DataModel.VendorID = presentationViewModelObject.VendorID;
            DataModel.DateCreate = presentationViewModelObject.DateCreated;
            DataModel.DatePurchased = presentationViewModelObject.DatePurchased;
            DataModel.CurrentLocationID = presentationViewModelObject.CurrenTLocation;
            DataModel.Image = presentationViewModelObject.Photo;
            DataModel.PurchasePrice = presentationViewModelObject.PurchasePrice;
            //if (presentationViewModelObject.Image != null)
            //{
            //    //System.IO.BinaryReader b = new System.IO.BinaryReader(presentationViewModelObject.Image.InputStream);
            //    //byte[] binData = b.ReadBytes(presentationViewModelObject.Image.ContentLength);
            //    //presentationViewModelObject.Image = binData;

            //    //using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            //    //{

            //    //    var imageData = presentationViewModelObject.Image.InputStream;
            //    //    var length = Convert.ToInt32(imageData.Length);
            //    //    byte[] tempImage = new byte[length];
            //    //    imageData.Read(tempImage, 0, length);

            //    //    DataModel.Image = tempImage;


            //    //}
            //    DataModel.Image = presentationViewModelObject.Photo;
            //    string extension = System.IO.Path.GetExtension(presentationViewModelObject.Image.FileName);
            //    DataModel.FileExtension = extension;
            //}
           DataModel.Levels= presentationViewModelObject.LevelID  ;
           DataModel.Section= presentationViewModelObject.SectionID  ;
            if (  presentationViewModelObject.Photo!=null)
            {
                DataModel.Image = presentationViewModelObject.Photo;
            }
            if (!string.IsNullOrEmpty(presentationViewModelObject.fileExtension))
            {
                DataModel.FileExtension = presentationViewModelObject.fileExtension;
            }
            DataModel.PhysicalDimension = presentationViewModelObject.PhysicalDimension;
            DataModel.Comments = presentationViewModelObject.Comments;

            return DataModel;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Dispose any managed objects
                    // ...
                }

                // Now disposed of any unmanaged objects
                // ...

                disposed = true;
            }
        }
    }
}