using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using FullSchoolWebsite.AdminPanel.Classes;

namespace AdminPanel
{
    public partial class EditAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null) Response.Redirect("Index.aspx");

            if (!IsPostBack)
            {
                Admin admin = GetAdmin(Request.QueryString["id"]);
                if (admin != null) SetTextFields(admin);
                else Response.Redirect("Index.aspx");
            }
        }

        protected Admin GetAdmin(string id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Admins WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = Int32.Parse(Request.QueryString["id"]);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows) return null;

                    reader.Read();
                    int adminID = Int32.Parse(reader["ID"].ToString());
                    string name = reader["Name"].ToString();
                    string password = reader["Password"].ToString();
                    string email = reader["Email"].ToString();
                    string role = reader["Role"].ToString();

                    Admin admin = new Admin(adminID, name, password, email, role);
                    return admin;
                }
                catch (Exception ex)
                {
                    //error message
                    return null;
                }
            }
        }

        protected void SetTextFields(Admin admin)
        {
            AdminNameTextBox.Text = admin.Name;
            AdminEmailTextBox.Text = admin.Email;
            AdminPasswordTextBox.Text = admin.Password;
            AdminRoleDropDownList.SelectedValue = admin.Role;
        }

        protected void ButtonEditAdmin_Click(object sender, EventArgs e)
        {
            if (AdminNameTextBox.Text == String.Empty || AdminEmailTextBox.Text == String.Empty || AdminPasswordTextBox.Text == String.Empty)
            { 
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля попълнете всички полета!";
                return;
            }

            if (Session["Role"].ToString() != "Главен администратор" && AdminRoleDropDownList.SelectedValue == "Главен администратор")
            {
                AlertBox.Attributes["class"] = "alert_error";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Не можете да промените ролята си!";
                return;
            }

            int id = Int32.Parse(Request.QueryString["id"]);
            string name = AdminNameTextBox.Text;
            string email = AdminEmailTextBox.Text;
            string password = AdminPasswordTextBox.Text;
            string role = AdminRoleDropDownList.SelectedValue;

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "UPDATE Admins SET Name = @name, Password = @password, Email = @email, Role = @role WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@password", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@role", System.Data.SqlDbType.NVarChar);

                    command.Parameters["@id"].Value = id;
                    command.Parameters["@name"].Value = name;
                    command.Parameters["@email"].Value = email;
                    command.Parameters["@password"].Value = password;
                    command.Parameters["@role"].Value = role;

                    con.Open();
                    command.ExecuteNonQuery();
                    Response.Redirect("ShowAllAdmins.aspx");
                    //success message
                }
                catch (Exception ex)
                { 
                    //error message                
                    AlertBox.Attributes["class"] = "alert_error";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Нещо се обърка. Моля опитайте отново!";
                }
            }
        }
    }
}