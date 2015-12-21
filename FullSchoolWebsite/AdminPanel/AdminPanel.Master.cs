using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FullSchoolWebsite.AdminPanel
{
    public partial class AdminPanel : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Name"] == null) Response.Redirect("../Login.aspx");

            AdminName.InnerText = (string)Session["Name"];
        }

        protected void SignOut_ServerClick(object sender, EventArgs e)
        {
            Session["ID"] = null;
            Session["Name"] = null;
            Session["Email"] = null;
            Session["Role"] = null;
            Response.Redirect("Index.aspx");
        }

        protected void EditMySettings_ServerClick(object sender, EventArgs e)
        {
            int id = Int32.Parse(Session["ID"].ToString());
            Response.Redirect("EditAdmin.aspx?id=" + id);
        }
    }
}