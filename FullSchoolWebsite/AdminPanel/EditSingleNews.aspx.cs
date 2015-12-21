using System;
using System.IO;
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
    public partial class EditSingleNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null) Response.Redirect("Index.aspx");

                MyNews news = GetNews(Request.QueryString["id"]);
                if (news != null) SetTextFields(news);
                else Response.Redirect("Index.aspx");
            }
        }

        protected MyNews GetNews(string id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM News WHERE ID = @id";

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
            TextBoxTitle.Text = news.Title;
            CKEditor.Text = news.Text;
            if (news.Status == true) status.SelectedValue = "1";
            else status.SelectedValue = "0";
            TextBoxAuthor.Text = news.Author;
            if (news.ShowEmail == true)
            {
                enableEmail.Checked = true;
                TextBoxEmail.Text = news.Email;
                TextBoxEmail.Enabled = true;
            }
            else
            {
                enableEmail.Checked = false;
                TextBoxEmail.Text = String.Empty;
            }

        }

        protected void EditThisNews_Click(object sender, EventArgs e)
        {
            if (TextBoxTitle.Text == String.Empty || CKEditor.Text == String.Empty || TextBoxAuthor.Text == String.Empty 
                || (enableEmail.Checked && TextBoxEmail.Text == String.Empty))
            {
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля попълнете всички текстови полета!";
                return;
            }

            string ID = Request.QueryString["id"];
            string title = TextBoxTitle.Text;
            string text = CKEditor.Text;
            text = text.Replace("&nbsp;", " ");

            string imagePath = "";
            if (NewsImageFileUpload.HasFiles)
            {
                try
                {
                    MyImage image = new MyImage("/PermanentFiles/", NewsImageFileUpload.PostedFile);
                    imagePath = image.UploadImageToServer();
                    DeleteMainImage(Int32.Parse(ID));
                }
                catch (ArgumentException ex)
                {
                    AlertBox.Attributes["class"] = "alert_warning";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Моля изберете снимка във валиден формат!";
                    return;
                }
            }

            bool stat = true;
            if (status.SelectedValue == "0") stat = false;
            else stat = true;
            string author = TextBoxAuthor.Text;
            bool enEmail = enableEmail.Checked;
            string email = "";
            if (enEmail == true) email = TextBoxEmail.Text;
            else email = String.Empty;

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string queryNoImg = "UPDATE News SET Title = @title, Text = @text, Status = @status, Author = @author, ShowEmail = @showEmail, Email = @email WHERE ID = @id";
            string queryImg = "UPDATE News SET Title = @title, Text = @text, ImagePath = @imagePath, Status = @status, Author = @author, ShowEmail = @showEmail, Email = @email WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    string finalQuery = imagePath.Length == 0 ? queryNoImg : queryImg;
                    SqlCommand command = new SqlCommand(finalQuery, con);

                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@text", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@imagePath", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@status", System.Data.SqlDbType.Bit);
                    command.Parameters.Add("@author", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@showEmail", System.Data.SqlDbType.Bit);
                    command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);

                    command.Parameters["@id"].Value = Int32.Parse(Request.QueryString["id"]);
                    command.Parameters["@title"].Value = title;
                    command.Parameters["@text"].Value = text;
                    command.Parameters["@imagePath"].Value = imagePath;
                    command.Parameters["@status"].Value = stat;
                    command.Parameters["@author"].Value = author;
                    command.Parameters["@showEmail"].Value = enEmail;
                    command.Parameters["@email"].Value = email;

                    con.Open();
                    command.ExecuteNonQuery();

                    //success message                    
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Новината беше редактирана успешно!";
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

        protected void DeleteMainImage(int contestID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM News WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                command.Parameters["@id"].Value = contestID;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        string path = reader["ImagePath"].ToString();
                        System.IO.File.Delete(Server.MapPath(path));
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