using System;
using System.Collections.Generic;
using System.Linq;
using model = Artwork_App.ArtWorkDBEntities1;

namespace Artwork_App.ViewModels
{
    public class ArtTypeViewModel:  Repository.IRepository<ArtTypeViewModel>, Repository.IMapper<ArtTypeViewModel, ArtType>, IDisposable
    {
        private Artwork_App.ArtWorkDBEntities1 context;
        private bool disposed = false;

        public int ArtTypeID { get; set; }
        public string ArtTypeName { get; set; }


        public int Insert(ArtTypeViewModel presentaionObj)
        {
            Int32 retVal = 0;
            using (this.context = new Artwork_App.ArtWorkDBEntities1())
            {
                ArtType DataModel = new ArtType();
                DataModel = this.MapPresentationObjectToDataObject(presentaionObj);
                this.context.ArtTypes.Add(DataModel);
                int rowsAffected = context.SaveChanges();
                retVal = DataModel.ArtTypeID;
            }



            return retVal;
        }

        public int Update(ArtTypeViewModel obj)
        {
            Int32 rowsAffected = 0;
            using (this.context = new Artwork_App.ArtWorkDBEntities1())
            {
                ArtType DataObject = new ArtType();
                //map and update
                DataObject = this.MapPresentationObjectToDataObject(obj);
                //set edit date

                this.context.ArtTypes.Attach(DataObject);
                this.context.Entry(DataObject).State = System.Data.Entity.EntityState.Modified;

                rowsAffected = this.context.SaveChanges();
            }


            return rowsAffected;
        }

        public int UpdateParams(ArtTypeViewModel entity, params System.Linq.Expressions.Expression<Func<ArtTypeViewModel, object>>[] properties)
        {
            throw new NotImplementedException();
        }

        public int Delete(ArtTypeViewModel obj)
        {
            throw new NotImplementedException();
        }

        public ArtTypeViewModel GetObject(int id)
        {
            using (this.context = new model())
            {
                ArtType dataObj = new ArtType();

                dataObj = this.context.ArtTypes.Find(id);
                using (ArtTypeViewModel presentaion = new ArtTypeViewModel())
                {
                    return this.MapDataObjectToPresentation(presentaion, dataObj);
                }
            }
        }

        public List<ArtTypeViewModel> GetData()
        {
            List<ArtTypeViewModel> List = new List<ArtTypeViewModel>();
            using (this.context = new model())
            {
                var collection = (from p in this.context.ArtTypes

                                  select p).ToList();
                foreach (var item in collection)
                {
                    using (ArtTypeViewModel i = new ArtTypeViewModel())
                    {
                        i.MapDataObjectToPresentation(i, item);
                        List.Add(i);
                    }
                }
            }
            return List; 
        }

        public ArtTypeViewModel MapDataObjectToPresentation(ArtTypeViewModel presentationViewModelObject, ArtType DataModel)
        {

            presentationViewModelObject.ArtTypeName = DataModel.ArtType1;
            presentationViewModelObject.ArtTypeID = DataModel.ArtTypeID;

        return presentationViewModelObject;
        }

        public ArtType MapPresentationObjectToDataObject(ArtTypeViewModel presentationViewModelObject)
        {
            var DataObj = new ArtType();
            DataObj.ArtType1 = presentationViewModelObject.ArtTypeName;
            DataObj.ArtTypeID = presentationViewModelObject.ArtTypeID;

            return DataObj;
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