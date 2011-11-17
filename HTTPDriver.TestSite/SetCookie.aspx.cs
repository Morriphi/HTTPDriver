using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HTTPDriver.TestSite
{
    public partial class SetCookie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var teaCookie = new HttpCookie("Tea");
            teaCookie.Value = "LoveOne";
            Response.Cookies.Add(teaCookie);
        }
    }
}