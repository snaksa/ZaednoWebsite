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
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMainImages();
                LoadStatements();
            }

            var li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("indexPage");
            li.Attributes.Add("class", "current-menu-item");
        }

        protected void LoadMainImages()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM NewsOnTheHomePage";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    List<MainImage> images = new List<MainImage>();
                    while (reader.Read())
                    {
                        string imagePath = reader["ImagePath"].ToString();
                        int position = Int32.Parse(reader["Position"].ToString());
                        MainImage image = new MainImage(imagePath, position);
                        images.Add(image);
                    }
                    MainImagesRepeater.DataSource = images;
                    MainImagesRepeater.DataBind();

                }
                catch (Exception ex)
                {
                    Response.Redirect("serverError.html");
                }
            }
        }

        protected void LoadStatements()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Statements";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    List<Statement> firstStatements = new List<Statement>();
                    List<Statement> secondStatements = new List<Statement>();
                    int count = 1;
                    while (reader.Read())
                    {
                        string title = reader["Title"].ToString();
                        string text = reader["Text"].ToString();
                        string imagePath = reader["ImagePath"].ToString();
                        Statement statement = new Statement(title, text, imagePath);

                        if (count <= 3) firstStatements.Add(statement);
                        else secondStatements.Add(statement);

                        count++;
                    }
                    FirstStatementsRepeater.DataSource = firstStatements;
                    SecondStatementsRepeater.DataSource = secondStatements;
                    FirstStatementsRepeater.DataBind();
                    SecondStatementsRepeater.DataBind();

                }
                catch (Exception ex)
                {
                    Response.Redirect("serverError.html");
                }
            }
        }
    }
}