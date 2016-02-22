using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Artwork_App.RptDataSets
{
    public partial class ReportsWebPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           // var context = new ArtWorkDBEntities1();
            if (!IsPostBack)
            {
                List<ViewModels.ArtworkViewModel> ArtworkVM = new List<ViewModels.ArtworkViewModel>();
                //using (ViewModels.ArtworkViewModel ViewModle= new ViewModels.ArtworkViewModel ())
                //{
                //    ArtworkVM = ViewModle.GetData();
                //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RPTReports/Report1.rdlc");
                //    ReportViewer1.LocalReport.DataSources.Clear();
                //    ReportDataSource rdc = new ReportDataSource("MyDataset", ArtworkVM);
                //    ReportViewer1.LocalReport.DataSources.Add(rdc);
                //    ReportViewer1.LocalReport.Refresh();
                //}
                using (var context= new ArtWorkDBEntities1 ())
                {
                    
                }
            }

        }
    }
}