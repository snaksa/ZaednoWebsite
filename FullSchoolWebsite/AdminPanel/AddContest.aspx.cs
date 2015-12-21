using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Configuration;
using System.Data.SqlClient;
using FullSchoolWebsite.AdminPanel.Classes;

namespace AdminPanel
{
    public partial class AddContest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddNewContest_Click(object sender, EventArgs e)
        {
            if (TextBoxTitle.Text == String.Empty || CKEditor.Text == String.Empty || !smallContestImageFileUpload.HasFile || !largeContestImageFIleUpload.HasFile)
            {
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля попълнете всички полета!";
                return;
            }

            string title = TextBoxTitle.Text;
            string text = CKEditor.Text;
            text = text.Replace("&nbsp;", " ");

            MyImage smallImage = null;
            MyImage largeImage = null;
            string smallImageName = "";
            string largeImageName = "";

            try
            {
                smallImage = new MyImage("/PermanentFiles/", smallContestImageFileUpload.PostedFile);
                smallImageName = smallImage.UploadImageToServer();
                largeImage = new MyImage("/PermanentFiles/", largeContestImageFIleUpload.PostedFile);
                largeImageName = largeImage.UploadImageToServer();
            }
            catch (ArgumentException ex)
            { 
                //error message
                AlertBox.Attributes["class"] = "alert_error";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля изберете снимка във валиден формат!";
                return;
            }

            bool stat = true; ;
            if (status.SelectedValue == "0") stat = false;
            else stat = true;

            string email = emailForQuestionsTextBox.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "INSERT INTO Contests(Title, Text, SmallImagePath, LargeImagePath, Status, Email, Date) " +
                "VALUES(@title, @text, @smallImagePath, @largeImagePath, @status, @email, @date)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);

                    command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@text", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@smallImagePath", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@largeImagePath", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@status", System.Data.SqlDbType.Bit);
                    command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@date", System.Data.SqlDbType.DateTime);

                    command.Parameters["@title"].Value = title;
                    command.Parameters["@text"].Value = text;
                    command.Parameters["@largeImagePath"].Value = largeImageName;
                    command.Parameters["@smallImagePath"].Value = smallImageName;
                    command.Parameters["@status"].Value = stat;
                    command.Parameters["@email"].Value = email;
                    command.Parameters["@date"].Value = DateTime.Now.ToUniversalTime();

                    con.Open();
                    command.ExecuteNonQuery();

                    //success message
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Конкурсът беше добавен успешно!";
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



    }
}