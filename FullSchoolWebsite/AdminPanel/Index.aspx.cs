using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using FullSchoolWebsite.AdminPanel.Classes;

namespace AdminPanel
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadPosts();
                LoadMessages();
            }
        }

        protected void ButtonSubmitMessage_Click(object sender, EventArgs e)
        {
            if (TextBoxMessage.Text == String.Empty)
            { 
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля въведете своето съобщение към администраторите!";
                return;
            }

            string message = TextBoxMessage.Text;
            int adminID = (int)Session["ID"];

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "INSERT INTO PostsOnTheWall(AdminID, Text) VALUES(@adminID, @message)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);

                    command.Parameters.Add("@adminID", System.Data.SqlDbType.Int);
                    command.Parameters.Add("@message", System.Data.SqlDbType.NVarChar);

                    command.Parameters["@adminID"].Value = adminID;
                    command.Parameters["@message"].Value = message;

                    con.Open();
                    command.ExecuteNonQuery();
                    LoadPosts();

                    //success message
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Постът беше добавен успешно!";
                }
                catch (Exception ex)
                { 
                    //error message
                    AlertBox.Attributes["class"] = "alert_error";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Нещо се обърка. Моля опитайте пак!";
                }
            }

            
        }

        protected void DeletePostImageButton_Command(object sender, CommandEventArgs e)
        {
            if (Session["Role"].ToString() != "Главен администратор")
            {
                //error message
                AlertBox.Attributes["class"] = "alert_error";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Нямате правомощия за извършване на това действие!";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "DELETE FROM PostsOnTheWall WHERE ID = @deletePostID";
            string id = e.CommandName;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    command.Parameters.AddWithValue("@deletePostID", id);
                    command.ExecuteNonQuery();
                    LoadPosts();

                    //success message
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Постът беше изтрит успешно!";
                }
                catch (Exception ex)
                { 
                    //error message
                }
            }
        }

        protected void LoadPosts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT a.Name, a.Email, p.ID, p.Text FROM Admins a INNER JOIN PostsOnTheWall p ON p.AdminID = a.ID";
            List<PostOnTheWall> posts = new List<PostOnTheWall>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string name = (string)reader["Name"];
                        string email = (string)reader["Email"];
                        string message = (string)reader["Text"];

                        PostOnTheWall post = new PostOnTheWall(id, name, email, message);
                        posts.Add(post);
                    }

                    if (posts.Count == 0)
                    { 
                        PostOnTheWall p = new PostOnTheWall(0, "-", "-", "Няма налични съобщения от администраторите");
                        posts.Add(p);
                    }

                    AdminPosts.DataSource = posts;
                    AdminPosts.DataBind();
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

        protected void LoadMessages()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM MessagesFromUsers";
            List<MessageFromUser> messages = new List<MessageFromUser>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string name = (string)reader["SenderName"];
                        string email = (string)reader["SenderEmail"];
                        string messagee = (string)reader["Text"];
                        DateTime date = (DateTime)reader["Date"];

                        MessageFromUser message = new MessageFromUser(id, name, email, messagee, date);
                        messages.Add(message);
                    }

                    if (messages.Count == 0)
                    {
                        MessageFromUser p = new MessageFromUser(0, "-", "-", "Няма налични съобщения от потребители", DateTime.Now);
                        messages.Add(p);
                    }

                    UserMessages.DataSource = messages;
                    UserMessages.DataBind();
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

        protected void DeleteUserMessageButton_Command(object sender, CommandEventArgs e)
        {
            if (Session["Role"].ToString() != "Главен администратор")
            {
                AlertBox.Attributes["class"] = "alert_error";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Нямате правомощия за извършване на това действие!";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "DELETE FROM MessagesFromUsers WHERE ID = @deleteMessageID";
            string id = e.CommandName;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    command.Parameters.AddWithValue("@deleteMessageID", id);
                    command.ExecuteNonQuery();
                    LoadMessages();

                    //success message
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Съобщението е изтрито успешно!";
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