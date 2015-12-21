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
    public partial class EditHomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadImages();
        }

        protected void LoadImages()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM NewsOnTheHomePage";

            string[] images = new string[4];

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    images[count] = reader["ImagePath"].ToString();
                    count++;
                }

                Image1.Src = images[0];
                Image2.Src = images[1];
                Image3.Src = images[2];
                Image4.Src = images[3];
            }
        }

        protected void UploadImage(int position, HttpPostedFile File)
        {
            string folder = "/PermanentFiles/";
            MyImage image = null;
            string imagePath = "";

            try
            {
                image = new MyImage(folder, File);
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
            string query = "UPDATE NewsOnTheHomePage SET ImagePath = @imagePath WHERE Position = @position";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);

                    command.Parameters.Add("@imagePath", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@position", System.Data.SqlDbType.NVarChar);

                    command.Parameters["@imagePath"].Value = imagePath;
                    command.Parameters["@position"].Value = position;

                    DeleteImage(position);
                    con.Open();
                    command.ExecuteNonQuery();

                    //success message
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Слайдшоуто беше обновено успешно!";
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

        protected void DeleteImage(int position)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM NewsOnTheHomePage WHERE Position = @position";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add("@position", System.Data.SqlDbType.Int);
                command.Parameters["@position"].Value = position;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        string imagePath = reader["ImagePath"].ToString();
                        if (File.Exists(Server.MapPath(imagePath)))
                        {
                            System.IO.File.Delete(Server.MapPath(imagePath));
                        }
                    }
                    catch (Exception ex)
                    {
                        //nothing happens
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DeleteImage(1);
            UploadImage(1, FileUpload1.PostedFile);
            LoadImages();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DeleteImage(2);
            UploadImage(2, FileUpload2.PostedFile);
            LoadImages();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            DeleteImage(3);
            UploadImage(3, FileUpload3.PostedFile);
            LoadImages();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            DeleteImage(4);
            UploadImage(4, FileUpload4.PostedFile);
            LoadImages();
        }


    }
}