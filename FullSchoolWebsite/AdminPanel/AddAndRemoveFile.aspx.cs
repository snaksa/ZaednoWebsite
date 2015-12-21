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
    public partial class AddFileToArchive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllFiles();
            }
        }

        protected void ButtonAddFile_Click(object sender, EventArgs e)
        {
            if (TextBoxTitle.Text == String.Empty || TextBoxDescription.Text == String.Empty || FilePathTextBox.Text == String.Empty || !FileImageFileUpload.HasFile)
            {
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля попълнете всички полета!";
                return;
            }

            string name = TextBoxTitle.Text;
            string description = TextBoxDescription.Text;
            string fileName = FilePathTextBox.Text;

            MyImage image = null;
            string imagePath = "";

            try
            {
                image = new MyImage("/PermanentFiles/", FileImageFileUpload.PostedFile);
                imagePath = image.UploadImageToServer();
            }
            catch (ArgumentException ex)
            {
                AlertBox.Attributes["class"] = "alert_error";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля изберете снимка във валиден формат!";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "INSERT INTO Files(Name, Description, ImagePath, FilePath) " +
                "VALUES(@name, @description, @imagePath, @filePath)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);

                    command.Parameters.Add("@name", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@description", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@imagePath", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@filePath", System.Data.SqlDbType.NVarChar);

                    command.Parameters["@name"].Value = name;
                    command.Parameters["@description"].Value = description;
                    command.Parameters["@imagePath"].Value = imagePath;
                    command.Parameters["@filePath"].Value = fileName;

                    con.Open();
                    command.ExecuteNonQuery();
                    LoadAllFiles();

                    //success message
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Файлът беше добавен успешно!";
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

        protected void EditSingleFileWithID_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandName;
            Response.Redirect("EditSingleFile.aspx?id=" + id);
        }

        protected void LoadAllFiles()
        { 
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT ID, Name FROM Files";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    List<MyFile> files = new List<MyFile>();

                    while (reader.Read())
                    {
                        int id = Int32.Parse(reader["ID"].ToString());
                        string name = reader["Name"].ToString();

                        MyFile file = new MyFile(id, name);
                        files.Add(file);
                    }

                    AllFilesRepeater.DataSource = files;
                    AllFilesRepeater.DataBind();
                }
                catch (Exception ex)
                { 
                    //error message
                }
            }
        }

        protected void ButtonDeleteFiles_Click(object sender, EventArgs e)
        {
            string files = filesToBeDeleted.Value;
            if (files == String.Empty)
            { 
                //error message
                return;
            }

            string[] IDs = files.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;

            foreach (string id in IDs)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        DeleteMainImage(Int32.Parse(id));
                        string query = "DELETE FROM Files WHERE ID = @id ";
                        SqlCommand command = new SqlCommand(query, con);
                        command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                        command.Parameters["@id"].Value = Int32.Parse(id);
                        con.Open();
                        command.ExecuteNonQuery();
                        //success message
                    }
                    catch (Exception ex)
                    { 
                        //error message
                        return;
                    }
                }
            }
            LoadAllFiles();
            //success message
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
                    string imagePath = reader["ImagePath"].ToString();
                    System.IO.File.Delete(Server.MapPath(imagePath));
                }
            }
        }
    }
}