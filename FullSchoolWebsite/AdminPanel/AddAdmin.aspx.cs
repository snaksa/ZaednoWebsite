using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace AdminPanel
{
    public partial class AddAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void ButtonAddAdmin_Click(object sender, EventArgs e)
        {
            string currentAdminRole = (string)Session["Role"];
            if (currentAdminRole != "Главен администратор")
            { 
                //error message
                AlertBox.Attributes["class"] = "alert_error";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Нямате право да добавяте нови администратори!";
                return;
            }

            if (AdminNameTextBox.Text == String.Empty || AdminEmailTextBox.Text == String.Empty || AdminPasswordTextBox.Text == String.Empty)
            { 
                //error message
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля попълнете всички полета!";
                return;
            }

            string name = AdminNameTextBox.Text;
            string password = AdminPasswordTextBox.Text;
            string email = AdminEmailTextBox.Text;
            string role = AdminRoleDropDownList.SelectedValue;

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "INSERT INTO Admins(Name, Password, Email, Role) VALUES(@name, @password, @email, @role)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@role", System.Data.SqlDbType.NVarChar);

                    command.Parameters["@name"].Value = name;
                    command.Parameters["@password"].Value = password;
                    command.Parameters["@email"].Value = email;
                    command.Parameters["@role"].Value = role;

                    con.Open();
                    command.ExecuteNonQuery();
                    
                    //success message
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Администраторът е добавен успешно!";
                }
                catch (Exception ex)
                {
                    AlertBox.Attributes["class"] = "alert_error";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Нещо се обърка. Моля опитайте отново!";
                }
            }

           
        }
    }
}