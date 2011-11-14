using System;
using System.Web;

namespace HTTPDriver.TestSite
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cookies.Add(TestCookie("Chocolate"));
            Response.Cookies.Add(TestCookie("Rasin"));
            Response.Cookies.Add(TestCookie("Oatmeal"));
        }

        private static HttpCookie TestCookie(string name)
        {
            var httpCookie = new HttpCookie(name);
            httpCookie.Values.Add("TestValue1", "Value1");
            httpCookie.Values.Add("TestValue2", "Value2");
            httpCookie.Values.Add("TestValue3", "Value3");
            httpCookie.Expires = DateTime.Now.AddHours(1);
            return httpCookie;
        }
    }
}