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
    public partial class Contests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllContests();
            }
            var li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("contestPage");
            li.Attributes.Add("class", "current-menu-item");
        }

        protected void LoadAllContests()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Contests WHERE Status = 1 ORDER BY ID DESC";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Contest> allContests = new List<Contest>();
                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string title = (string)reader["Title"];
                        string text = (string)reader["Text"];
                        string smallImagePath = (string)reader["SmallImagePath"];
                        string largeImagePath = (string)reader["LargeImagePath"];
                        bool status = Boolean.Parse(reader["Status"].ToString());
                        string email = (string)reader["Email"];
                        DateTime date = (DateTime)reader["Date"];
                        Contest contest = new Contest(id, title, text, smallImagePath, largeImagePath, status, email, date.AddHours(2));
                        allContests.Add(contest);
                    }
                    AllContestsRepeater.DataSource = allContests;
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