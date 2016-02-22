using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using model = Artwork_App.ArtWorkDBEntities1;
namespace Artwork_App.ViewModels
{
    public class ArtistViewModel : Repository.IRepository<ArtistViewModel>, Repository.IMapper<ArtistViewModel, Artist>, IDisposable
    {
        private Artwork_App.ArtWorkDBEntities1 context;
        private bool disposed = false;

        public int ArtistID { get; set; }
        public string ArtistName { get; set; }


        public int Insert(ArtistViewModel presentaionObj)
        {
            Int32 retVal = 0;
            using (this.context = new Artwork_App.ArtWorkDBEntities1())
            {
                Artist DataModel = new Artist();
                DataModel = this.MapPresentationObjectToDataObject(presentaionObj);
                this.context.Artists.Add(DataModel);
                int rowsAffected = context.SaveChanges();
                retVal = DataModel.ArtistID;
                refreshContext();
            }



            return retVal;
        }

        public int Update(ArtistViewModel obj)
        {
            Int32 rowsAffected = 0;
            using (this.context = new Artwork_App.ArtWorkDBEntities1())
            {
                Artist DataObject = new Artist();
                //map and update
                DataObject = this.MapPresentationObjectToDataObject(obj);
                //set edit date

                this.context.Artists.Attach(DataObject);
                this.context.Entry(DataObject).State = System.Data.Entity.EntityState.Modified;

                rowsAffected = this.context.SaveChanges();
                refreshContext();
            }


            return rowsAffected;
        }

        public int UpdateParams(ArtistViewModel entity, params System.Linq.Expressions.Expression<Func<ArtistViewModel, object>>[] properties)
        {
            throw new NotImplementedException();
        }

        public int Delete(ArtistViewModel obj)
        {
            throw new NotImplementedException();
        }

        public ArtistViewModel GetObject(int id)
        {
            using (this.context = new model())
            {
              
                Artist dataObj = new Artist();

                dataObj = this.context.Artists.Find(id);
                using (ArtistViewModel presentaion = new ArtistViewModel())
                {
                    return this.MapDataObjectToPresentation(presentaion, dataObj);
                }
            }
        }
     
        public List<ArtistViewModel> GetData()
        {
            List<ArtistViewModel> List = new List<ArtistViewModel>();
            try
            {
                using (this.context = new model())
                {
                    try
                    {
                       // refreshContext();
                     

                        var collection = (from p in this.context.Artists
                                          orderby p.ArtistID descending
                                          select p).ToList();
                        var context = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this.context).ObjectContext;
                        context.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, collection);
                        foreach (var item in collection)
                        {
                            using (ArtistViewModel i = new ArtistViewModel())
                            {
                                i.MapDataObjectToPresentation(i, item);
                                List.Add(i);
                            }
                        }
                    }
                    
                    catch (Exception ex)
                    {
                        
                        throw ex;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
           
            return List; 
        }

      public void refreshContext()
        {
            var newCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)this.context).ObjectContext;
            var refreshableObjects = (from entry in newCtx.ObjectStateManager.GetObjectStateEntries(
                                              System.Data.Entity.EntityState.Added
                                             | System.Data.Entity.EntityState.Deleted
                                             | System.Data.Entity.EntityState.Modified
                                             | System.Data.Entity.EntityState.Unchanged)
                                      where entry.EntityKey != null
                                      select entry.Entity).ToList();

            newCtx.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, refreshableObjects);
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

        public ArtistViewModel MapDataObjectToPresentation(ArtistViewModel presentationViewModelObject, Artist DataModel)
        {
            presentationViewModelObject.ArtistID = DataModel.ArtistID;
            presentationViewModelObject.ArtistName = DataModel.Artist1;

            return presentationViewModelObject;
        }

        public Artist MapPresentationObjectToDataObject(ArtistViewModel presentationViewModelObject)
        {
            var DataModel = new Artist();
          DataModel.ArtistID=  presentationViewModelObject.ArtistID ;
         DataModel.Artist1=    presentationViewModelObject.ArtistName ;
         return DataModel;
        }
    }
}