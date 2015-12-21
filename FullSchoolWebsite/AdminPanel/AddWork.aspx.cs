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
    public partial class AddWork : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddNewNews_Click(object sender, EventArgs e)
        {
            if (TextBoxTitle.Text == String.Empty || CKEditor.Text == String.Empty || !NewsImageFileUpload.HasFile || TextBoxAuthor.Text == String.Empty)
            {
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля попълнете всички полета!";
                return;
            }

            if (enableEmail.Checked && TextBoxEmail.Text == String.Empty)
            {
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля посочете email!";
                return;
            }

            string title = TextBoxTitle.Text;
            string text = CKEditor.Text;
            text = text.Replace("&nbsp;", " ");

            MyImage image = null;
            string imagePath = "";

            try
            {
                image = new MyImage("/PermanentFiles/", NewsImageFileUpload.PostedFile);
                imagePath = image.UploadImageToServer();
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

            string author = TextBoxAuthor.Text;

            bool enEmail = enableEmail.Checked;
            string email = TextBoxEmail.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "INSERT INTO AcceptedWorks(Title, Text, ImagePath, Status, Author, ShowEmail, Email, Date, PositiveVotes, NegativeVotes, Views) " +
                "VALUES(@title, @text, @imagePath," +
                " @status, @author, @showEmail, @email, @date, 0, 0, 0)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);

                    command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@text", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@imagePath", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@status", System.Data.SqlDbType.Bit);
                    command.Parameters.Add("@author", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@showEmail", System.Data.SqlDbType.Bit);
                    command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@date", System.Data.SqlDbType.DateTime);

                    command.Parameters["@title"].Value = title;
                    command.Parameters["@text"].Value = text;
                    command.Parameters["@imagePath"].Value = imagePath;
                    command.Parameters["@status"].Value = stat;
                    command.Parameters["@author"].Value = author;
                    command.Parameters["@showEmail"].Value = enEmail;
                    command.Parameters["@email"].Value = email;
                    command.Parameters["@date"].Value = DateTime.Now.ToUniversalTime();

                    con.Open();
                    command.ExecuteNonQuery();

                    //success message
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Творбата беше добавена успешно!";
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