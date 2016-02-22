using System;
using System.Collections.Generic;
using System.Linq;
using model = Artwork_App.ArtWorkDBEntities1;

namespace Artwork_App.ViewModels
{
    public class SectionsViewModel : Repository.IRepository<SectionsViewModel>, Repository.IMapper<SectionsViewModel, Section>, IDisposable
    {
        private Artwork_App.ArtWorkDBEntities1 context;
        private bool disposed = false;
        public string SectionName { get; set; }
        public Nullable<int> SectionID { get; set; }


        public int Insert(SectionsViewModel obj)
        {
            Int32 retVal = 0;
            using (this.context = new Artwork_App.ArtWorkDBEntities1())
            {
                Section DataModel = new Section();
                DataModel = this.MapPresentationObjectToDataObject(obj);
                this.context.Sections.Add(DataModel);
                int rowsAffected = context.SaveChanges();
                retVal = DataModel.SectionID;
            }



            return retVal;
        }

        public int Update(SectionsViewModel obj)
        {
            Int32 rowsAffected = 0;
            using (this.context = new Artwork_App.ArtWorkDBEntities1())
            {
                Section DataObject = new Section();
                //map and update
                DataObject = this.MapPresentationObjectToDataObject(obj);
                //set edit date

                this.context.Sections.Attach(DataObject);
                this.context.Entry(DataObject).State = System.Data.Entity.EntityState.Modified;

                rowsAffected = this.context.SaveChanges();
            }


            return rowsAffected;
        }

        public int UpdateParams(SectionsViewModel entity, params System.Linq.Expressions.Expression<Func<SectionsViewModel, object>>[] properties)
        {
            throw new NotImplementedException();
        }

        public int Delete(SectionsViewModel obj)
        {
            throw new NotImplementedException();
        }

        public SectionsViewModel GetObject(int id)
        {
            using (this.context = new model())
            {
                Section dataObj = new Section();

                dataObj = this.context.Sections.Find(id);
                using (SectionsViewModel presentaion = new SectionsViewModel())
                {
                    return this.MapDataObjectToPresentation(presentaion, dataObj);
                }
            }
        }

        public List<SectionsViewModel> GetData()
        {

            List<SectionsViewModel> List = new List<SectionsViewModel>();
            using (this.context = new model())
            {
                var collection = (from p in this.context.Sections 

                                  select p).ToList();
                foreach (Section item in collection)
                {
                    using (SectionsViewModel i = new SectionsViewModel())
                    {
                        i.MapDataObjectToPresentation(i, item);
                        List.Add(i);
                    }
                }
            }

            return List;
        }

        public SectionsViewModel MapDataObjectToPresentation(SectionsViewModel presentationViewModelObject, Section DataModel)
        {

            if (DataModel.SectionID>=1)
            {
                presentationViewModelObject.SectionID = DataModel.SectionID;
            }
            else
            {
                presentationViewModelObject.SectionID = 0;
            }
            presentationViewModelObject.SectionName = DataModel.Section1;

            return presentationViewModelObject;
        }

        public Section MapPresentationObjectToDataObject(SectionsViewModel presentationViewModelObject)
        {
            var dataObj = new Section();

            if (presentationViewModelObject.SectionID.HasValue)
            {
                dataObj.SectionID = presentationViewModelObject.SectionID.Value;
                    
            }
            else
	        {
                dataObj.SectionID = 0;
	        }
           
            dataObj.Section1 = presentationViewModelObject.SectionName;


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