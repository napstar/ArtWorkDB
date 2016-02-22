using System;
using System.Collections.Generic;
using System.Linq;
using model = Artwork_App.ArtWorkDBEntities1;

namespace Artwork_App.ViewModels
{
    public class CountryViewModel : Repository.IRepository<CountryViewModel>, Repository.IMapper<CountryViewModel, Country>, IDisposable
    {
        private Artwork_App.ArtWorkDBEntities1 context;
        private bool disposed = false;
        public string CountryName { get; set; }
        public Nullable<int> CountryID { get; set; }


        public int Insert(CountryViewModel obj)
        {
            Int32 retVal = 0;
            using (this.context = new Artwork_App.ArtWorkDBEntities1())
            {
                Country DataModel = new Country();
                DataModel = this.MapPresentationObjectToDataObject(obj);
                this.context.Countries.Add(DataModel);
                int rowsAffected = context.SaveChanges();
                retVal = DataModel.CountryID;
            }



            return retVal;
        }

        public int Update(CountryViewModel obj)
        {
            Int32 rowsAffected = 0;
            using (this.context = new Artwork_App.ArtWorkDBEntities1())
            {
                Country DataObject = new Country();
                //map and update
                DataObject = this.MapPresentationObjectToDataObject(obj);
                //set edit date

                this.context.Countries.Attach(DataObject);
                this.context.Entry(DataObject).State = System.Data.Entity.EntityState.Modified;

                rowsAffected = this.context.SaveChanges();
            }


            return rowsAffected;
        }

        public int UpdateParams(CountryViewModel entity, params System.Linq.Expressions.Expression<Func<CountryViewModel, object>>[] properties)
        {
            throw new NotImplementedException();
        }

        public int Delete(CountryViewModel obj)
        {
            throw new NotImplementedException();
        }

        public CountryViewModel GetObject(int id)
        {
            using (this.context = new model())
            {
                Country dataObj = new Country();

                dataObj = this.context.Countries.Find(id);
                using (CountryViewModel presentaion = new CountryViewModel())
                {
                    return this.MapDataObjectToPresentation(presentaion, dataObj);
                }
            }
        }

        public List<CountryViewModel> GetData()
        {
            List<CountryViewModel> List = new List<CountryViewModel>();
            using (this.context = new model())
            {
                var collection = (from p in this.context.Countries

                                  select p).ToList();
                foreach (Country item in collection)
                {
                    using (CountryViewModel i = new CountryViewModel())
                    {
                        i.MapDataObjectToPresentation(i, item);
                        List.Add(i);
                    }
                }
            }

            return List;
        }

        public CountryViewModel MapDataObjectToPresentation(CountryViewModel presentationViewModelObject, Country DataModel)
        {
            presentationViewModelObject.CountryID = DataModel.CountryID;
            presentationViewModelObject.CountryName = DataModel.Country1;

            return presentationViewModelObject;
        }

        public Country MapPresentationObjectToDataObject(CountryViewModel presentationViewModelObject)
        {
            var DataModel = new Country();
            if (presentationViewModelObject.CountryID.HasValue)
            {
                DataModel.CountryID = presentationViewModelObject.CountryID.Value;
            }
           
         DataModel.Country1=   presentationViewModelObject.CountryName ;

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