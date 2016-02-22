using System;
using System.Collections.Generic;
using System.Linq;
using model = Artwork_App.ArtWorkDBEntities1;

namespace Artwork_App.ViewModels
{
    public class LevelsViewModel : Repository.IRepository<LevelsViewModel>, Repository.IMapper<LevelsViewModel, Level>, IDisposable
    {
        private Artwork_App.ArtWorkDBEntities1 context;
        private bool disposed = false;
        public string LevelName { get; set; }
        public Nullable<int> LevelID { get; set; }


        public int Insert(LevelsViewModel obj)
        {
            Int32 retVal = 0;
            using (this.context = new Artwork_App.ArtWorkDBEntities1())
            {
                Level DataModel = new Level();
                DataModel = this.MapPresentationObjectToDataObject(obj);
                this.context.Levels.Add(DataModel);
                int rowsAffected = context.SaveChanges();
                retVal = DataModel.LevelID;
            }



            return retVal;
        }

        public int Update(LevelsViewModel obj)
        {
            Int32 rowsAffected = 0;
            using (this.context = new Artwork_App.ArtWorkDBEntities1())
            {
                Level DataObject = new Level();
                //map and update
                DataObject = this.MapPresentationObjectToDataObject(obj);
                //set edit date

                this.context.Levels.Attach(DataObject);
                this.context.Entry(DataObject).State = System.Data.Entity.EntityState.Modified;

                rowsAffected = this.context.SaveChanges();
            }


            return rowsAffected;
        }

        public int UpdateParams(LevelsViewModel entity, params System.Linq.Expressions.Expression<Func<LevelsViewModel, object>>[] properties)
        {
            throw new NotImplementedException();
        }

        public int Delete(LevelsViewModel obj)
        {
            throw new NotImplementedException();
        }

        public LevelsViewModel GetObject(int id)
        {
            using (this.context = new model())
            {
                Level dataObj = new Level();

                dataObj = this.context.Levels.Find(id);
                using (LevelsViewModel presentaion = new LevelsViewModel())
                {
                    return this.MapDataObjectToPresentation(presentaion, dataObj);
                }
            }
        }

        public List<LevelsViewModel> GetData()
        {
            List<LevelsViewModel> List = new List<LevelsViewModel>();
            using (this.context = new model())
            {
                var collection = (from p in this.context.Levels

                                  select p).ToList();
                foreach (Level item in collection)
                {
                    using (LevelsViewModel i = new LevelsViewModel())
                    {
                        i.MapDataObjectToPresentation(i, item);
                        List.Add(i);
                    }
                }
            }

            return List;
        }

        public LevelsViewModel MapDataObjectToPresentation(LevelsViewModel presentationViewModelObject, Level DataModel)
        {

            presentationViewModelObject.LevelID = DataModel.LevelID;
            presentationViewModelObject.LevelName = DataModel.Levels;
            return presentationViewModelObject;
        }

        public Level MapPresentationObjectToDataObject(LevelsViewModel presentationViewModelObject)
        {
            var DataObj = new Level();
            DataObj.LevelID = presentationViewModelObject.LevelID.Value;
            DataObj.Levels = presentationViewModelObject.LevelName;
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