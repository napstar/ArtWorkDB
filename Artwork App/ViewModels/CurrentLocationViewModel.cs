using System;
using System.Collections.Generic;
using System.Linq;
using model = Artwork_App.ArtWorkDBEntities1;

namespace Artwork_App.ViewModels
{
    public class CurrentLocationViewModel : Repository.IRepository<CurrentLocationViewModel>, Repository.IMapper<CurrentLocationViewModel, CurrentLocation>, IDisposable
    {
        private Artwork_App.ArtWorkDBEntities1 context;
        private bool disposed = false;
        public string LocationName { get; set; }
        public Nullable<int> LocationID { get; set; }

        

  
 
      

        public int Insert(CurrentLocationViewModel obj)
        {
            Int32 retVal = 0;
            using (this.context = new Artwork_App.ArtWorkDBEntities1())
            {
                CurrentLocation DataModel = new CurrentLocation();
                DataModel = this.MapPresentationObjectToDataObject(obj);
                this.context.CurrentLocations.Add(DataModel);
                int rowsAffected = context.SaveChanges();
                retVal = DataModel.CurrentLocationID;
            }



            return retVal;
        }
        public int Update(CurrentLocationViewModel obj)
        {
            Int32 rowsAffected = 0;
            using (this.context = new Artwork_App.ArtWorkDBEntities1())
            {
                CurrentLocation DataObject = new CurrentLocation();
                //map and update
                DataObject = this.MapPresentationObjectToDataObject(obj);
                //set edit date

                this.context.CurrentLocations.Attach(DataObject);
                this.context.Entry(DataObject).State = System.Data.Entity.EntityState.Modified;

                rowsAffected = this.context.SaveChanges();
            }


            return rowsAffected;
        }

        public int UpdateParams(CurrentLocationViewModel entity, params System.Linq.Expressions.Expression<Func<CurrentLocationViewModel, object>>[] properties)
        {
            throw new NotImplementedException();
        }

        public int Delete(CurrentLocationViewModel obj)
        {
            throw new NotImplementedException();
        }

        public CurrentLocationViewModel GetObject(int id)
        {
            using (this.context = new model())
            {
                CurrentLocation dataObj = new CurrentLocation();

                dataObj = this.context.CurrentLocations.Find(id);
                using (CurrentLocationViewModel presentaion = new CurrentLocationViewModel())
                {
                    return this.MapDataObjectToPresentation(presentaion, dataObj);
                }
            }
        }

        public List<CurrentLocationViewModel> GetData()
        {
            List<CurrentLocationViewModel> List = new List<CurrentLocationViewModel>();
            using (this.context = new model())
            {
                var collection = (from p in this.context.CurrentLocations

                                  select p).ToList();
                foreach (CurrentLocation item in collection)
                {
                    using (CurrentLocationViewModel i = new CurrentLocationViewModel())
                    {
                        i.MapDataObjectToPresentation(i, item);
                        List.Add(i);
                    }
                }
            }

            return List;
        }

        public CurrentLocationViewModel MapDataObjectToPresentation(CurrentLocationViewModel presentationViewModelObject, CurrentLocation DataModel)
        {
            presentationViewModelObject.LocationID = DataModel.CurrentLocationID;
            presentationViewModelObject.LocationName = DataModel.CurrentLocation1;

            return presentationViewModelObject;
        }

        public CurrentLocation MapPresentationObjectToDataObject(CurrentLocationViewModel presentationViewModelObject)
        {
            var dataObj = new CurrentLocation();
            dataObj.CurrentLocation1 = presentationViewModelObject.LocationName;
            if (presentationViewModelObject.LocationID.HasValue)
            {
                dataObj.CurrentLocationID = presentationViewModelObject.LocationID.Value;

            }
            
            return dataObj;
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