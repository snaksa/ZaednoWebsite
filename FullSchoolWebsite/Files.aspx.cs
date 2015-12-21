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
    public partial class Files : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllFiles();
            }
            var li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("filesPage");
            li.Attributes.Add("class", "current-menu-item");
        }

        protected void LoadAllFiles()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Files ORDER BY ID DESC";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<MyFile> allFiles = new List<MyFile>();
                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string name = (string)reader["Name"];
                        string description = (string)reader["Description"];
                        string imagePath = (string)reader["ImagePath"];
                        string filePath = (string)reader["FilePath"];
                        MyFile file = new MyFile(id, name, description, imagePath, filePath);
                        allFiles.Add(file);
                    }
                    AllFilesRepeater.DataSource = allFiles;
                    AllFilesRepeater.DataBind();
                }
                catch (Exception ex)
                {
                    //error message
                    Response.Redirect("serverError.html");
                }
            }
        }
    }
}