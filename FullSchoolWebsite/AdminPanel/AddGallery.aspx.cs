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
    public partial class AddGallery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AddNewGallery_Click(object sender, EventArgs e)
        {
            if (TextBoxTitle.Text == String.Empty || TextBoxDescription.Text == String.Empty || !GalleryImageFileUpload.HasFile)
            {
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля попълнете всички полета!";
                return;
            }

            string title = TextBoxTitle.Text;
            string description = TextBoxDescription.Text;
            bool stat = true;
            if (status.SelectedValue == "0") stat = false;
            else stat = true;

            MyImage image = null;
            string imagePath = "";

            try
            {
                image = new MyImage("/PermanentFiles/", GalleryImageFileUpload.PostedFile);
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

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "INSERT INTO Galleries(Title, Description, Status, ImagePath) VALUES(@title, @description, @status, @imagePath)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@description", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@status", System.Data.SqlDbType.Bit);
                    command.Parameters.Add("@imagePath", System.Data.SqlDbType.NVarChar);

                    command.Parameters["@title"].Value = title;
                    command.Parameters["@description"].Value = description;
                    command.Parameters["@status"].Value = stat;
                    command.Parameters["@imagePath"].Value = imagePath;

                    con.Open();
                    command.ExecuteNonQuery();
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