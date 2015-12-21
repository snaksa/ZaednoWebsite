using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using FullSchoolWebsite.AdminPanel.Classes;

namespace AdminPanel
{
    public partial class EditSingleContest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null) Response.Redirect("Index.aspx");

                Contest contest = GetContest(Request.QueryString["id"]);
                if (contest != null) SetTextFields(contest);
                else Response.Redirect("Index.aspx");
            }
        }

        protected Contest GetContest(string id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Contests WHERE ID = @id";

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
                    int contestID = Int32.Parse(reader["ID"].ToString());
                    string title = reader["Title"].ToString();
                    string text = reader["Text"].ToString();
                    bool status = Boolean.Parse(reader["Status"].ToString());
                    string email = reader["Email"].ToString();

                    Contest contest = new Contest(contestID, title, text, status, email);
                    return contest;
                }
                catch (Exception ex)
                {
                    //error message
                    return null;
                }
            }
        }

        protected void SetTextFields(Contest contest)
        {
            TextBoxTitle.Text = contest.Title;
            CKEditor.Text = contest.Text;
            if (contest.Status == true) status.SelectedValue = "1";
            else status.SelectedValue = "0";
            emailForQuestions.Text = contest.Email;
        }

        protected void EditThisContest_Click(object sender, EventArgs e)
        {
            if (TextBoxTitle.Text == String.Empty || CKEditor.Text == String.Empty || emailForQuestions.Text == String.Empty)
            {
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля попълнете всички текстови полета!";
                return;
            }

            int id = Int32.Parse(Request.QueryString["id"]);
            string title = TextBoxTitle.Text;
            string text = CKEditor.Text;
            text = text.Replace("&nbsp;", " ");
            bool stat = true;
            if (status.SelectedValue == "0") stat = false;
            else stat = true;
            string email = emailForQuestions.Text;

            string smallImagePath = "";
            string largeImagePath = "";

            try
            {
                if (smallContestImageFileUpload.HasFile)
                {
                    MyImage smallImage = new MyImage("/PermanentFiles/", smallContestImageFileUpload.PostedFile);
                    smallImagePath = smallImage.UploadImageToServer();
                    DeleteSmallMainImage(id);
                }
                if (largeContestImageFIleUpload.HasFile)
                {
                    DeleteLargeMainImage(id);
                    MyImage largeImage = new MyImage("/PermanentFiles/", largeContestImageFIleUpload.PostedFile);
                    largeImagePath = largeImage.UploadImageToServer();
                }
            }
            catch (ArgumentException ex)
            {
                AlertBox.Attributes["class"] = "alert_error";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля изберете снимка във валиден формат!";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string fullQuery = "UPDATE Contests SET Title = @title, Text = @text," +
                " SmallImagePath = @smallImagePath, LargeImagePath = @largeImagePath, Status = @status, Email = @email WHERE ID = @id";
            string smallQuery = "UPDATE Contests SET Title = @title, Text = @text," +
                " SmallImagePath = @smallImagePath, Status = @status, Email = @email WHERE ID = @id";
            string largeQuery = "UPDATE Contests SET Title = @title, Text = @text," +
                " LargeImagePath = @largeImagePath, Status = @status, Email = @email WHERE ID = @id";
            string noImagesQuery = "UPDATE Contests SET Title = @title, Text = @text," +
                " Status = @status, Email = @email WHERE ID = @id";

            string finalQuery = "";
            if (smallImagePath != String.Empty && largeImagePath != String.Empty) finalQuery = fullQuery;
            else if (smallImagePath != String.Empty) finalQuery = smallQuery;
            else if (largeImagePath != String.Empty) finalQuery = largeQuery;
            else finalQuery = noImagesQuery;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(finalQuery, con);

                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@text", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@smallImagePath", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@largeImagePath", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@status", System.Data.SqlDbType.Bit);
                    command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);

                    command.Parameters["@id"].Value = Int32.Parse(Request.QueryString["id"]);
                    command.Parameters["@title"].Value = title;
                    command.Parameters["@text"].Value = text;
                    command.Parameters["@smallImagePath"].Value = smallImagePath;
                    command.Parameters["@largeImagePath"].Value = largeImagePath;
                    command.Parameters["@status"].Value = stat;
                    command.Parameters["@email"].Value = email;

                    con.Open();
                    command.ExecuteNonQuery();

                    //success message
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Конкурсът беше редактиран успешно!";
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

        protected void DeleteSmallMainImage(int contestID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Contests WHERE ID = @id";

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
                        string path = reader["SmallImagePath"].ToString();
                        System.IO.File.Delete(Server.MapPath(path));
                    }
                    catch (Exception ex)
                    { 
                        //nothing happens
                    }
                }
            }
        }

        protected void DeleteLargeMainImage(int contestID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Contests WHERE ID = @id";

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
                        string path = reader["LargeImagePath"].ToString();
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