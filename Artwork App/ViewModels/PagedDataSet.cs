using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artwork_App.ViewModels
{
    public class PagedDataSet<T> where T:class
    {
        public IEnumerable<T> Items { get; set; }
        public int Page { get; set; }
     
        public int PageTotal { get; set; }
    }
}