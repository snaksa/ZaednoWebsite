using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using FullSchoolWebsite.AdminPanel.Classes;

namespace FullSchoolWebsite
{
    public partial class SuggestWork : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("newsPage");
            li.Attributes.Add("class", "current-menu-item");
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            if (!ImageFileUpload.HasFile || CKEditor.Text == String.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert1", "alert('Моля попълнете всички полета');", true);
                return;
            }

            string name = AuthorNameTextBox.Value;
            string email = AuthorEmailTextBox.Value;
            bool showEmail = ShowEmailDropDownList.SelectedValue == "0" ? false : true;
            string title = NewsTitleTextBox.Value;
            MyImage image = null;
            string imagePath;
            try
            {
                image = new MyImage("/PermanentFiles/", ImageFileUpload.PostedFile);
                imagePath = image.UploadImageToServer();
            }
            catch (ArgumentException ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert4", "alert('Моля изберете валидна снимка');", true);
                return;

            }
            string text = CKEditor.Text;
            text = text.Replace("&nbsp;", " ");

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "INSERT INTO SuggestedWorks(Title, Text, ImagePath, Author, ShowEmail, Email, Date) "
                + "VALUES(@title, @text, @imagePath, @author, @showEmail, @email, @date)";

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

                    command.Parameters["@title"].Value = title;
                    command.Parameters["@text"].Value = text;
                    command.Parameters["@imagePath"].Value = imagePath;
                    command.Parameters["@author"].Value = name;
                    command.Parameters["@showEmail"].Value = showEmail;
                    command.Parameters["@email"].Value = email;
                    command.Parameters["@date"].Value = DateTime.Now.ToUniversalTime();

                    con.Open();
                    command.ExecuteNonQuery();
                    //success message
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert2", "alert('Творбата е предложена успешно!');", true);
                    return;
                }
                catch (Exception ex)
                {
                    //error message
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert3", "alert('Нещо се обърка! Моля опитайте отново!');", true);
                    return;

                }
            }
        }
    }
}