using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using FullSchoolWebsite.AdminPanel.Classes;

namespace FullSchoolWebsite
{
    public partial class AboutUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("contactPage");
            li.Attributes.Add("class", "current-menu-item");
        }

        protected void submit_ServerClick(object sender, EventArgs e)
        {
            string name1 = name.Value;
            string email1 = email.Value;
            string text1 = comments.Value;

            if (name1 == String.Empty || email1 == String.Empty || text1 == String.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Моля попълнете всички полета')", true);
                return;
            }

            name.Value = String.Empty;
            email.Value = String.Empty;
            comments.Value = String.Empty;

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "INSERT INTO MessagesFromUsers(SenderName, SenderEmail, Text, Date) VALUES(@senderName, @senderEmail, @text, @date)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add("@senderName", System.Data.SqlDbType.NVarChar);
                command.Parameters.Add("@senderEmail", System.Data.SqlDbType.NVarChar);
                command.Parameters.Add("@text", System.Data.SqlDbType.NVarChar);
                command.Parameters.Add("@date", System.Data.SqlDbType.DateTime);

                command.Parameters["@senderName"].Value = name1;
                command.Parameters["@senderEmail"].Value = email1;
                command.Parameters["@text"].Value = text1;
                command.Parameters["@date"].Value = DateTime.Now.ToUniversalTime();

                con.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}