using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public interface IRepository<T>
    {
        int Insert(T obj);
        int Update(T obj);
        //call this        ///Update(Model, d=>d.Name, d=>d.SecondProperty, d=>d.AndSoOn); 
        int UpdateParams(T entity, params Expression<Func<T, object>>[] properties);
        int Delete(T obj);
        T GetObject(int id);
        List<T> GetData();
    }
}
