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
    public partial class EditGalleries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGalleries();
            }
        }

        protected void LoadGalleries()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT ID, Title, Status FROM Galleries";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    command.Dispose();
                    List<Gallery> allGalleries = new List<Gallery>();
                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string title = (string)reader["Title"];
                        bool status = (bool)reader["Status"];

                        string getCount = "SELECT COUNT(*) FROM PicturesInGalleries WHERE GalleryID = @id";

                        int count;
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand numImgs = new SqlCommand(getCount, connection);
                            numImgs.Parameters.Add("@id", System.Data.SqlDbType.Int);
                            numImgs.Parameters["@id"].Value = id;

                            connection.Open();
                            count = Int32.Parse(numImgs.ExecuteScalar().ToString());
                        }

                        Gallery news = new Gallery(id, title, status, count);
                        allGalleries.Add(news);
                    }
                    AllGalleriesRepeater.DataSource = allGalleries;
                    AllGalleriesRepeater.DataBind();
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

        protected void UploadImages_Click(object sender, EventArgs e)
        {
            if (AddImagesFileUpload.HasFiles)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
                string query = "INSERT INTO PicturesInGalleries(GalleryID, ImagePath, PositiveVotes, NegativeVotes) VALUES(@id, @imagePath, 0, 0)";

                string toolTip = GalleryID.Value;
                int id = Int32.Parse(toolTip);

                int countNoImages = 0;
                foreach (var item in AddImagesFileUpload.PostedFiles)
                {
                    try
                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            MyImage image = new MyImage("/PermanentFiles/", item);
                            string imagePath = image.UploadImageToServer();

                            SqlCommand command = new SqlCommand(query, con);
                            command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                            command.Parameters.Add("@imagePath", System.Data.SqlDbType.NVarChar);

                            command.Parameters["@id"].Value = id;
                            command.Parameters["@imagePath"].Value = imagePath;

                            con.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                    catch (ArgumentException ex)
                    { 
                        //not an image
                        countNoImages++;
                    }
                }
                LoadGalleries();
                if (countNoImages == 0)
                {
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Всички снимки бяха качени успешно!";
                }
                else
                { 
                    string error = "";
                    if (countNoImages == 1) error = "1 снимка не беше качена, понеже не е във валиден формат!";
                    else error = countNoImages + " снимки не бяха качени, понеже не са във валиден формат!";

                    AlertBox.Attributes["class"] = "alert_warning";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = error;
                }
            }
        }

        protected void DeleteImage_Command(object sender, CommandEventArgs e)
        {
            int id = Int32.Parse(e.CommandName);
            LoadAllPictures(sender, id);
        }

        protected void DeleteSelectedImages_Click(object sender, EventArgs e)
        {
            List<int> IDsToBeDeleted = new List<int>();
            foreach (RepeaterItem item in AllImagesRepeater.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("chkDelete");
                if (chk.Checked)
                {
                    IDsToBeDeleted.Add(Int32.Parse(chk.ToolTip));
                }
            }

            foreach (int item in IDsToBeDeleted)
            {
                DeleteSelectedImagesOnTheServer(item);
                DeleteSelectedImagesFromTheDatabase(item);
            }

            LoadGalleries();
            //success message
            AlertBox.Attributes["class"] = "alert_success";
            AlertBox.Visible = true;
            AlertBox.InnerText = "Избраните снимки бяха изтрити успешно!";
        }

        protected void DeleteSelectedImagesOnTheServer(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT ImagePath FROM PicturesInGalleries WHERE ID = @id";

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

        protected void DeleteSelectedImagesFromTheDatabase(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "DELETE FROM PicturesInGalleries WHERE ID = @id";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = id;

                    con.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //nothing happens
            }
        }

        protected void LoadAllPictures(object sender, int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM PicturesInGalleries WHERE GalleryID = @id";
            List<PictureInGallery> pictures = new List<PictureInGallery>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = id;

                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int picID = Int32.Parse(reader["ID"].ToString());
                        string path = reader["ImagePath"].ToString();
                        PictureInGallery pic = new PictureInGallery(picID, path, 0, 0);
                        pictures.Add(pic);
                    }
                    AllImagesRepeater.DataSource = pictures;
                    AllImagesRepeater.DataBind();
                    Page.ClientScript.RegisterStartupScript(sender.GetType(), "ShowArticle", "ShowArticle('#allImages')", true);
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

        protected void DeleteGallery_Command(object sender, CommandEventArgs e)
        {
            int galleryID = Int32.Parse(e.CommandName);
            DeleteAllImagesOnTheServer(galleryID);
            DeleteAllImagesInGallery(galleryID);
            DeleteMainImage(galleryID);

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "DELETE FROM Galleries WHERE ID = @id";

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = galleryID;

                    con.Open();
                    command.ExecuteNonQuery();
                }
                LoadGalleries();

                //success message
                AlertBox.Attributes["class"] = "alert_success";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Галерията беше изтрита успешно!";
            }
            catch (Exception ex)
            {
                //nothing happens
            }
        }

        protected void DeleteAllImagesOnTheServer(int galleryID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT ImagePath FROM PicturesInGalleries WHERE GalleryID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                command.Parameters["@id"].Value = galleryID;

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

        protected void DeleteAllImagesInGallery(int galleryID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "DELETE FROM PicturesInGalleries WHERE GalleryID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                command.Parameters["@id"].Value = galleryID;

                con.Open();
                command.ExecuteNonQuery();
            }
        }

        protected void DeleteMainImage(int galleryID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Galleries WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                command.Parameters["@id"].Value = galleryID;

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

        protected void EditGalleryWithID_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandName;
            Response.Redirect("EditSingleGallery.aspx?id=" + id);
        }
    }
}