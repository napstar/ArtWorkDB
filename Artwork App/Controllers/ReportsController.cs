using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Artwork_App.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Reports()
        {
            var  i=0;
            return Redirect("/RptDataSets/ReportsWebPage.aspx");
        }
    }
}