using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTPDriver.TestSite
{
    public partial class TemporaryRedirectToAnotherPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           Response.Redirect("/TestSite/AnotherPage.aspx"); //302 Temporary Redirect
        }
    }
}