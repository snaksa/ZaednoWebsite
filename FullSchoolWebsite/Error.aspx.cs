using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FullSchoolWebsite
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ErrorMessage"] == null) Response.Redirect("Default.aspx");

            string addition = "Моля върнете се на <a href='Default.aspx'>главната страница</a> или ни <a href='AboutUs.aspx'>изпратете съобщение.</a>";
            string message = Session["ErrorMessage"].ToString() + addition;
            MessageTextSpan.InnerHtml = message;
        }
    }
}