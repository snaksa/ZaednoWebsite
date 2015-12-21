using System;
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
    public partial class EditSingleGallery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null) Response.Redirect("Index.aspx");

                Gallery gallery = GetGallery(Request.QueryString["id"]);
                if (gallery != null) SetGalleryTextFields(gallery);
                else Response.Redirect("Index.aspx");
            }
        }

        protected Gallery GetGallery(string id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Galleries WHERE ID = @id";

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
                    int galleryID = Int32.Parse(reader["ID"].ToString());
                    string title = reader["Title"].ToString();
                    string description = Server.HtmlDecode(reader["Description"].ToString());
                    bool status = Boolean.Parse(reader["Status"].ToString());

                    Gallery gallery = new Gallery(galleryID, title, description, status);
                    return gallery;
                }
                catch (Exception ex)
                {
                    //error message
                    return null;
                }
            }
        }

        protected void SetGalleryTextFields(Gallery gallery)
        {
            TextBoxTitle.Text = gallery.Title;
            TextBoxDescription.Text = gallery.Description;
            if (gallery.Status == true) status.SelectedValue = "1";
            else status.SelectedValue = "0";            
        }

        protected void EditThisGallery_Click(object sender, EventArgs e)
        {
            if (TextBoxTitle.Text == String.Empty || TextBoxDescription.Text == String.Empty)
            { 
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля попълнете всички текстови полета!";
                return;
            }

            string ID = Request.QueryString["id"];
            string title = TextBoxTitle.Text;
            string description = TextBoxDescription.Text;
            string imagePath = "";
            if (GalleryImageFileUpload.HasFiles)
            {
                try
                {
                    MyImage image = new MyImage("/PermanentFiles/", GalleryImageFileUpload.PostedFile);
                    imagePath = image.UploadImageToServer();
                }
                catch (ArgumentException ex)
                {
                    //error message
                    AlertBox.Attributes["class"] = "alert_warning";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Моля изберете снимка във валиден формат!";
                    return;
                }
            }
            bool stat = true;
            if (status.SelectedValue == "0") stat = false;
            else stat = true;

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string queryNoImg = "UPDATE Galleries SET Title = @title, Description = @description, Status = @status WHERE ID = @id";
            string queryImg = "UPDATE Galleries SET Title = @title, Description = @description, Status = @status, ImagePath = @imagePath WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    string finalQuery = imagePath.Length == 0 ? queryNoImg : queryImg;
                    SqlCommand command = new SqlCommand(finalQuery, con);

                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@description", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@imagePath", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@status", System.Data.SqlDbType.Bit);

                    command.Parameters["@id"].Value = Int32.Parse(Request.QueryString["id"]);
                    command.Parameters["@title"].Value = title;
                    command.Parameters["@description"].Value = description;
                    command.Parameters["@imagePath"].Value = imagePath;
                    command.Parameters["@status"].Value = stat;

                    con.Open();
                    command.ExecuteNonQuery();

                    //success message
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Галерията беше редактирана успешно!";
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
