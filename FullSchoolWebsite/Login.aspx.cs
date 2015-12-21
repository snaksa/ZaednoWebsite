using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace FullSchoolWebsite
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AdminLogin_ServerClick(object sender, EventArgs e)
        {
            string email = emailTextBox.Value;
            string password = passwordTextBox.Value;

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Admins a WHERE a.Email = @email AND a.Password = @password";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);

                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        Session["ID"] = reader["ID"];
                        Session["Name"] = reader["Name"];
                        Session["Email"] = reader["Email"];
                        Session["Role"] = reader["Role"];
                        Response.Redirect("AdminPanel/Index.aspx");
                    }
                    else
                    {
                        headerText.InnerText = "Грешен email или парола!";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    //show error
                }
            }
        }
    }
}