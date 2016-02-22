using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Artwork_App.Extensions
{
    public static class HtmlExtensions
    {
        public static System.Web.Mvc.MvcHtmlString Image(this System.Web.Mvc.HtmlHelper html, byte[] image)
        {
            var img = String.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image));
            return new MvcHtmlString("<img src='" + img + "' />");
        }
    }
}