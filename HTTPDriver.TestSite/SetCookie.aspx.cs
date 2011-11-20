using System;
using System.Web;
using System.Web.UI;

namespace HTTPDriver.TestSite
{
    public partial class SetCookie : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cookies.Add(new HttpCookie("Tea") {Value = "LoveOne"});
            Response.Cookies.Add(new HttpCookie("Coffee") {Value = "BlackStrongNoSugar"});
        }
    }
}