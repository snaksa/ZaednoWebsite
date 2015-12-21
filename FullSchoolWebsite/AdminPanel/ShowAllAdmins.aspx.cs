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
    public partial class ShowAllAdmins : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllAdmins();
            }
        }

        protected void LoadAllAdmins()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Admins";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Admin> admins = new List<Admin>();

                    while (reader.Read())
                    {
                        int id = Int32.Parse(reader["ID"].ToString());
                        string name = reader["Name"].ToString();
                        string password = reader["Password"].ToString();
                        string email = reader["Email"].ToString();
                        string role = reader["Role"].ToString();

                        Admin admin = new Admin(id, name, password, email, role);
                        admins.Add(admin);
                    }

                    AllAdminsRepeater.DataSource = admins;
                    AllAdminsRepeater.DataBind();
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

        protected void EditAdminWithID_Command(object sender, CommandEventArgs e)
        {
            string role = (string)Session["Role"];
            if (role != "Главен администратор")
            { 
                //error message
                AlertBox.Attributes["class"] = "alert_error";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Нямате правомощия да извършите това действие!";
                return;
            }

            string id = e.CommandName;
            Response.Redirect("EditAdmin.aspx?id=" + id);
        }

        protected void DeleteAdmin_Command(object sender, CommandEventArgs e)
        {
            string role = (string)Session["Role"];
            if (role != "Главен администратор")
            {
                //error message
                AlertBox.Attributes["class"] = "alert_error";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Нямате правомощия да извършите това действие!";
                return;
            }

            int id = Int32.Parse(e.CommandName);
            if (Int32.Parse(Session["ID"].ToString()) == id)
            {
                //error message
                AlertBox.Attributes["class"] = "alert_error";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Не можете да премахнете себе си!";
                return;            
            }
            
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "DELETE FROM Admins WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = id;

                    con.Open();
                    command.ExecuteNonQuery();
                    LoadAllAdmins();

                    //success message
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Администраторът е премахнат успешно!";
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