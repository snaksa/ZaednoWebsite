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
    public partial class EditAcceptedWorks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null) Response.Redirect("Index.aspx");

                MyNews news = GetWorks(Request.QueryString["id"]);
                if (news != null) SetTextFields(news);
                else Response.Redirect("Index.aspx");
            }
        }

        protected MyNews GetWorks(string id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM AcceptedWorks WHERE ID = @id";

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
                    int newsID = Int32.Parse(reader["ID"].ToString());
                    string title = reader["Title"].ToString();
                    string text = Server.HtmlDecode(reader["Text"].ToString());
                    bool status = Boolean.Parse(reader["Status"].ToString());
                    string author = reader["Author"].ToString();
                    bool showEmail = Boolean.Parse(reader["ShowEmail"].ToString());
                    string email = reader["Email"].ToString();
                    DateTime date = DateTime.Parse(reader["Date"].ToString());

                    MyNews news = new MyNews(newsID, title, text, null, status, author, showEmail, email, date);
                    return news;
                }
                catch (Exception ex)
                {
                    //error message
                    return null;
                }
            }
        }

        protected void SetTextFields(MyNews news)
        {
            NewsTitleTextBox.Text = news.Title;
            CKEditor.Text = news.Text;
            AuthorNameTextBox.Text = news.Author;
            AuthorEmailTextBox.Text = news.Email;
            if (news.ShowEmail == true) ShowEmailDropDownList.SelectedIndex = 0;
            else ShowEmailDropDownList.SelectedIndex = 1;
            if (news.Status == true) StatusDropDownList.SelectedIndex = 0;
            else StatusDropDownList.SelectedIndex = 1;
        }

        protected void EditWorks_Click(object sender, EventArgs e)
        {
            if (NewsTitleTextBox.Text == String.Empty || AuthorEmailTextBox.Text == String.Empty || AuthorNameTextBox.Text == String.Empty || CKEditor.Text == String.Empty)
            {
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля попълнете всички текстови полета!";
                return;
            }

            int id = Int32.Parse(Request.QueryString["id"].ToString());
            string title = NewsTitleTextBox.Text;
            string author = AuthorNameTextBox.Text;

            string text = CKEditor.Text;
            text = text.Replace("&nbsp;", " ");

            string email = AuthorEmailTextBox.Text;
            bool showEmail;
            if (ShowEmailDropDownList.SelectedValue == "0") showEmail = false;
            else showEmail = true;
            bool status;
            if (StatusDropDownList.SelectedValue == "0") status = false;
            else status = true;

            MyImage image = null;
            string imagePath = "";
            if (ImageFileUpload.HasFile)
            {
                try
                {
                    image = new MyImage("/PermanentFiles/", ImageFileUpload.PostedFile);
                    imagePath = image.UploadImageToServer();
                    DeleteMainImage(id);
                }
                catch (ArgumentException ex)
                {
                    AlertBox.Attributes["class"] = "alert_warning";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Моля изберете снимка във валиден формат!";
                    return;
                }
            }

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string queryWithImage = "UPDATE AcceptedWorks SET Title = @title, Text = @text, ImagePath = @imagePath, Author = @author, ShowEmail = @showEmail, Email = @email, Status = @status WHERE ID = @id";
            string queryWithoutImage = "UPDATE AcceptedWorks SET Title = @title, Text = @text, Author = @author, ShowEmail = @showEmail, Email = @email, Status = @status WHERE ID = @id";

            string finalQuery = image != null ? queryWithImage : queryWithoutImage;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(finalQuery, con);

                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@text", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@imagePath", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@author", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@showEmail", System.Data.SqlDbType.Bit);
                    command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@status", System.Data.SqlDbType.Bit);

                    command.Parameters["@id"].Value = id;
                    command.Parameters["@title"].Value = title;
                    command.Parameters["@text"].Value = text;
                    command.Parameters["@imagePath"].Value = imagePath;
                    command.Parameters["@author"].Value = author;
                    command.Parameters["@showEmail"].Value = showEmail;
                    command.Parameters["@email"].Value = email;
                    command.Parameters["@status"].Value = status;

                    con.Open();
                    command.ExecuteNonQuery();

                    //success message                    
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Творбата беше редактирана успешно!";
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
            string query = "SELECT * FROM AcceptedWorks WHERE ID = @id";

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
    }
}