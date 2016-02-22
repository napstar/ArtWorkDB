using System;
using System.Collections.Generic;
using System.Linq;
using model = Artwork_App.ArtWorkDBEntities1;

namespace Artwork_App.ViewModels
{
    public class VendorViewModel : Repository.IRepository<VendorViewModel>, Repository.IMapper<VendorViewModel, Vendor>, IDisposable
    {
        private Artwork_App.ArtWorkDBEntities1 context;
        private bool disposed = false;

        public int VendorID { get; set; }
        public string VendorName { get; set; }

        public int Insert(VendorViewModel obj)
        {
            Int32 retVal = 0;
            using (this.context = new Artwork_App.ArtWorkDBEntities1())
            {
                Vendor DataModel = new Vendor();
                DataModel = this.MapPresentationObjectToDataObject(obj);
                this.context.Vendors.Add(DataModel);
                int rowsAffected = context.SaveChanges();
                retVal = DataModel.VendorID;
            }



            return retVal;
        }

        public int Update(VendorViewModel obj)
        {
            Int32 rowsAffected = 0;
            using (this.context = new Artwork_App.ArtWorkDBEntities1())
            {
                Vendor DataObject = new Vendor();
                //map and update
                DataObject = this.MapPresentationObjectToDataObject(obj);
                //set edit date

                this.context.Vendors.Attach(DataObject);
                this.context.Entry(DataObject).State = System.Data.Entity.EntityState.Modified;

                rowsAffected = this.context.SaveChanges();
            }


            return rowsAffected;
        }

        public int UpdateParams(VendorViewModel entity, params System.Linq.Expressions.Expression<Func<VendorViewModel, object>>[] properties)
        {
            throw new NotImplementedException();
        }

        public int Delete(VendorViewModel obj)
        {
            throw new NotImplementedException();
        }

        public VendorViewModel GetObject(int id)
        {
            using (this.context = new model())
            {
                Vendor dataObj = new Vendor();

                dataObj = this.context.Vendors.Find(id);
                using (VendorViewModel presentaion = new VendorViewModel())
                {
                    return this.MapDataObjectToPresentation(presentaion, dataObj);
                }
            }
        }

        public List<VendorViewModel> GetData()
        {
            List<VendorViewModel> vendorList = new List<VendorViewModel>();
            using (this.context = new model())
            {
                var collection = (from p in this.context.Vendors

                                  select p).ToList();
                foreach (var item in collection)
                {
                    using (VendorViewModel i = new VendorViewModel())
                    {
                        i.MapDataObjectToPresentation(i, item);
                        vendorList.Add(i);
                    }
                }
            }
            return vendorList; 
        }

        public VendorViewModel MapDataObjectToPresentation(VendorViewModel presentationViewModelObject, Vendor DataModel)
        {
            presentationViewModelObject.VendorID = DataModel.VendorID;
            presentationViewModelObject.VendorName = DataModel.Vendor1;

            return presentationViewModelObject;
        }

        public Vendor MapPresentationObjectToDataObject(VendorViewModel presentationViewModelObject)
        {
            var DataModel=new Vendor();
            DataModel.VendorID= presentationViewModelObject.VendorID;
           DataModel.Vendor1= presentationViewModelObject.VendorName ;
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