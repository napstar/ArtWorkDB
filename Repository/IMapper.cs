using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IMapper<T, K>
    {
        T MapDataObjectToPresentation(T presentationViewModelObject, K DataModel);
        K MapPresentationObjectToDataObject(T presentationViewModelObject);
    }
}
