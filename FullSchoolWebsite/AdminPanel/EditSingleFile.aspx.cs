using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using FullSchoolWebsite.AdminPanel.Classes;

namespace AdminPanel
{
    public partial class EditSingleFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null) Response.Redirect("Index.aspx");

                MyFile file = GetFile(Request.QueryString["id"]);
                if (file != null) SetTextFields(file);
                else Response.Redirect("Index.aspx");
            }
        }

        protected MyFile GetFile(string id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Files WHERE ID = @id";

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
                    int fileID = Int32.Parse(reader["ID"].ToString());
                    string name = reader["Name"].ToString();
                    string description = reader["Description"].ToString();
                    string filePath = reader["FilePath"].ToString();

                    MyFile file = new MyFile(fileID, name, description, filePath);
                    return file;
                }
                catch (Exception ex)
                {
                    //error message
                    return null;
                }
            }
        }

        protected void SetTextFields(MyFile file)
        {
            TextBoxTitle.Text = file.Name;
            TextBoxDescription.Text = file.Description;
            FilePathTextBox.Text = file.FilePath;
        }

        protected void EditFile_Click(object sender, EventArgs e)
        {
            if (TextBoxTitle.Text == String.Empty || TextBoxDescription.Text == String.Empty || FilePathTextBox.Text == String.Empty)
            { 
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля попълнете всички текстови полета!";
                return;
            }

            int id = Int32.Parse(Request.QueryString["id"]);
            string name = TextBoxTitle.Text;
            string description = TextBoxDescription.Text;
            string filePath = FilePathTextBox.Text;

            string imagePath = "";
            if (FileImageFileUpload.HasFile)
            {
                DeleteMainImage(id);
                try
                {
                    MyImage image = new MyImage("/PermanentFiles/", FileImageFileUpload.PostedFile);
                    imagePath = image.UploadImageToServer();
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
            string queryWithImage = "UPDATE Files SET Name = @name, Description = @description, ImagePath = @imagePath, FilePath = @filePath WHERE ID = @id";
            string queryWithoutImage = "UPDATE Files SET Name = @name, Description = @description, FilePath = @filePath WHERE ID = @id";

            string finalQuery;
            if (imagePath != String.Empty) finalQuery = queryWithImage;
            else finalQuery = queryWithoutImage;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(finalQuery, con);

                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@description", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@imagePath", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@filePath", System.Data.SqlDbType.NVarChar);

                    command.Parameters["@id"].Value = id;
                    command.Parameters["@name"].Value = name;
                    command.Parameters["@description"].Value = description;
                    command.Parameters["@imagePath"].Value = imagePath;
                    command.Parameters["@filePath"].Value = filePath;

                    con.Open();
                    command.ExecuteNonQuery();

                    //success message
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Файлът беше редактиран успешно!";
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

        protected void DeleteMainImage(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Files WHERE ID = @id";

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
    }
}