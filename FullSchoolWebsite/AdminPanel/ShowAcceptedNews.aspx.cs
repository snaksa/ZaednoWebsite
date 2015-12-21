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
    public partial class ShowAcceptedNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadNews();
            }
        }

        protected void LoadNews()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT ID, Title, Author, Date, Email, Status FROM AcceptedNews";

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
                        bool status = Boolean.Parse(reader["Status"].ToString());

                        MyNews news = new MyNews(id, title, String.Empty, null, status, author, true, email, date.AddHours(2));
                        allNews.Add(news);
                    }
                    AcceptedNewsRepeater.DataSource = allNews;
                    AcceptedNewsRepeater.DataBind();
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


        protected void ButtonDeleteSelectedNews_Click(object sender, EventArgs e)
        {
            string news = newsToDelete.Value;
            if (news == String.Empty)
            {
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля изберете новини, които да бъдат изтрити!";
                return;
            }

            string[] IDs = news.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;

            foreach (string id in IDs)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        string query = "DELETE FROM AcceptedNews WHERE ID = @id ";
                        SqlCommand command = new SqlCommand(query, con);
                        command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                        command.Parameters["@id"].Value = Int32.Parse(id);

                        DeleteMainImage(Int32.Parse(id));
                        DeleteNewsComments(Int32.Parse(id));
                        con.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //error message
                        return;
                    }
                }
            }
            LoadNews();
            //success message
            AlertBox.Attributes["class"] = "alert_success";
            AlertBox.Visible = true;
            AlertBox.InnerText = "Избраните новини бяха изтрити успешно!";
        }

        protected void DeleteMainImage(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM AcceptedNews WHERE ID = @id";

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

        protected void LoadCommentsWithID_Command(object sender, CommandEventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(sender.GetType(), "ShowArticle", "ShowArticle('#testDiv')", true);

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM ReporterComments WHERE NewsID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = Int32.Parse(e.CommandName);

                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    List<NewsComment> comments = new List<NewsComment>();
                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string name = (string)reader["AuthorName"];
                        string email = (string)reader["AuthorEmail"];
                        string text = (string)reader["Text"];
                        DateTime date = (DateTime)reader["Date"];

                        NewsComment comment = new NewsComment(id, name, email, text, date.AddHours(2));
                        comments.Add(comment);
                    }
                    reader.Dispose();

                    if (comments.Count == 0) comments.Add(new NewsComment(0, "-", "-", "Няма налични коментари", DateTime.Now.ToUniversalTime().AddHours(2)));
                    CommentsRepeater.DataSource = comments;
                    CommentsRepeater.DataBind();

                    query = "SELECT * FROM AcceptedNews WHERE ID = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    cmd.Parameters["@id"].Value = e.CommandName;
                    SqlDataReader read = cmd.ExecuteReader();
                    read.Read();
                    string newsName2 = (string)read["Title"];
                    if (newsName2.Length < 40) newsName.Text = newsName2;
                    else
                    {
                        newsName.Text = newsName2.Substring(0, 40) + "...";
                        newsName.ToolTip = newsName2;
                    }
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

        protected void DeleteNewsComments(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "DELETE FROM ReporterComments WHERE NewsID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = id;
                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //nothing happens
                    return;
                }
            }
        }

        protected void DeleteComments_Click(object sender, EventArgs e)
        {
            string commentsToBeDeleted = commentsToDelete.Value;
            if (commentsToBeDeleted == String.Empty)
            {
                //error message
                AlertBox.Attributes["class"] = "alert_error";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля изберете коментари, които да бъдат изтрити!";
                return;
            }

            commentsToBeDeleted = commentsToBeDeleted.Replace("Comment", "");
            string[] IDs = commentsToBeDeleted.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;

            foreach (string id in IDs)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        string query = "DELETE FROM ReporterComments WHERE ID = @id";
                        SqlCommand command = new SqlCommand(query, con);
                        command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                        command.Parameters["@id"].Value = Int32.Parse(id);
                        con.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //error message
                        return;
                    }
                }
            }
            //success message
            AlertBox.Attributes["class"] = "alert_success";
            AlertBox.Visible = true;
            AlertBox.InnerText = "Избраните коментари бяха изтрити успешно!";
        }

        protected void EditSingleNewsWithID_Command(object sender, CommandEventArgs e)
        {
            Response.Redirect("EditAcceptedNews.aspx?id=" + e.CommandName);
        }
    }
}