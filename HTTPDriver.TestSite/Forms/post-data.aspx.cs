using System;
using System.Text;

namespace HTTPDriver.TestSite.Forms
{
    public partial class post_data : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var postVariables = new StringBuilder();
            foreach (string key in Request.Form.Keys)
                postVariables.AppendFormat("{0}: {1}<br/>", key, Request.Form[key]);
            postData.Text = postVariables.ToString();
        }
    }
}