using System;

namespace HTTPDriver.TestSite
{
    public partial class GetCookie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CookieIsSet())
                cookieIsSet.Text = "This message will be display if cookie was set by HTTPDriver Browser";
        }

        private bool CookieIsSet()
        {
            return Request.Cookies.Count > 0 &&
                Request.Cookies[0].Name == "CookieSetByHTTPDriverBrowserTest";
        }
    }
}