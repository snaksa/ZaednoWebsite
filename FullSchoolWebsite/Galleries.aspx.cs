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
    public partial class Galleries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllGalleries();
            }
            var li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("galleryPage");
            li.Attributes.Add("class", "current-menu-item");
        }

        protected void LoadAllGalleries()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Galleries WHERE Status = 1 ORDER BY ID DESC";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Gallery> allGalleries = new List<Gallery>();
                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string title = (string)reader["Title"];
                        string description = (string)reader["Description"];
                        string imagePath = (string)reader["ImagePath"];
                        bool status = Boolean.Parse(reader["Status"].ToString());
                        Gallery gallery = new Gallery(id, title, description, status, imagePath);
                        allGalleries.Add(gallery);
                    }
                    AllContestsRepeater.DataSource = allGalleries;
                    AllContestsRepeater.DataBind();
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