using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using FullSchoolWebsite.AdminPanel.Classes;

namespace FullSchoolWebsite.AdminPanel
{
    public partial class ShowSuggestedNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSuggestedNews();
            }
        }

        protected void LoadSuggestedNews()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT ID, Title, Author, Date, Email FROM SuggestedNews";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<MyNews> allNews = new List<MyNews>();
                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string title = (string)reader["Title"];
                        string author = (string)reader["Author"];
                        string email = (string)reader["Email"];
                        DateTime date = (DateTime)reader["Date"];

                        MyNews news = new MyNews(id, title, author, email, date.AddHours(2));
                        allNews.Add(news);
                    }
                    ShowSuggestedNewsRepeater.DataSource = allNews;
                    ShowSuggestedNewsRepeater.DataBind();
                }
                catch (Exception ex)
                {
                    //error message
                    AlertBox.Attributes["class"] = "alert_error";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Нещо се обърка. Моля опитайте отново!";
                    return;
                }
            }
        }

        protected void DeleteMainImage(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM SuggestedNews WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        string imagePath = reader["ImagePath"].ToString();
                        System.IO.File.Delete(Server.MapPath(imagePath));
                    }
                    catch (Exception ex)
                    { 
                        //nothing happens
                    }
                }
            }
        }

        protected void Show_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandName;
            Response.Redirect("../ViewSuggestedNews.aspx?id=" + id);
        }

        protected void Accept_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandName;
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM SuggestedNews WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = Int32.Parse(id);

                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows) return;

                    while (reader.Read())
                    {
                        int newsID = Int32.Parse(reader["ID"].ToString());
                        string title = reader["Title"].ToString();
                        string text = reader["Text"].ToString();
                        string imagePath = reader["ImagePath"].ToString();
                        string author = reader["Author"].ToString();
                        bool showEmail = Boolean.Parse(reader["ShowEmail"].ToString());
                        string email = reader["Email"].ToString();
                        DateTime date = DateTime.Parse(reader["Date"].ToString());

                        MyNews news = new MyNews(newsID, title, text, imagePath, false, author, showEmail, email, date.AddHours(2));
                        TransferNews(news);
                        DeleteFromSuggestedNewsTable(news.ID);

                        //success message
                        AlertBox.Attributes["class"] = "alert_success";
                        AlertBox.Visible = true;
                        AlertBox.InnerText = "Новината беше одобрена успешно!";
                    }
                }
                catch (Exception ex)
                {
                    AlertBox.Attributes["class"] = "alert_error";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Нещо се обърка. Моля опитайте отново!";
                    return;
                }
            }
        }

        protected void TransferNews(MyNews news)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "INSERT INTO AcceptedNews(Title, Text, ImagePath, Author, ShowEmail, Email, Date, Status, PositiveVotes, NegativeVotes, Views) "
                + "VALUES(@title, @text, @imagePath, @author, @showEmail, @email, @date, @status, 0, 0, 0)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@text", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@imagePath", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@author", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@showEmail", System.Data.SqlDbType.Bit);
                    command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@date", System.Data.SqlDbType.DateTime);
                    command.Parameters.Add("@status", System.Data.SqlDbType.Bit);

                    command.Parameters["@title"].Value = news.Title;
                    command.Parameters["@text"].Value = news.Text;
                    command.Parameters["@imagePath"].Value = news.ImagePath;
                    command.Parameters["@author"].Value = news.Author;
                    command.Parameters["@showEmail"].Value = news.ShowEmail;
                    command.Parameters["@email"].Value = news.Email;
                    command.Parameters["@date"].Value = news.Date.AddHours(-2);
                    command.Parameters["@status"].Value = news.Status;

                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //nothing happens
                }
            }
        }

        protected void DeleteFromSuggestedNewsTable(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "DELETE FROM SuggestedNews WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = id;
                    con.Open();
                    command.ExecuteNonQuery();
                    LoadSuggestedNews();
                }
                catch (Exception ex)
                {
                    //nothing happens
                }
            }
        }

        protected void Refuse_Command(object sender, CommandEventArgs e)
        {
            int id = Int32.Parse(e.CommandName);
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "DELETE FROM SuggestedNews WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    DeleteMainImage(id);
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = id;
                    con.Open();
                    command.ExecuteNonQuery();
                    LoadSuggestedNews();

                    //success message
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Новината беше отхвърлена успешно!";
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