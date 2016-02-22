using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Artwork_App.RptDataSets
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<ViewModels.ArtworkViewModel> ArtworkVM = new List<ViewModels.ArtworkViewModel>();
                
                using (var context = new ArtWorkDBEntities1())
                {
                    var collection = (from p in context.Artworks
                                      join q in context.ArtTypes
                                      on p.ArtTypeID equals q.ArtTypeID
                                      select new { 
                                       PurchasePrce=p.PurchasePrice
                                       ,ArtType=q.ArtType1
                                      }
                                         ).Sum(o=>o.PurchasePrce);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RPTReports/Report1.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("SQlDataSource1", collection);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
            }
        }
    }
}