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
    public partial class ShowContest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                if (id == null)
                {
                    Session["ErrorMessage"] = "Не сте избрали новина, която да бъде показана. ";
                    Response.Redirect("Error.aspx");
                }

                Contest contest = GetContest(Int32.Parse(id));
                if (contest == null)
                {
                    Session["ErrorMessage"] = "Конкурсът не съществува. Възможно е да е бил премахнат от администраторите. ";
                    Response.Redirect("Error.aspx");
                }
                SetFields(contest);
            }
            var li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("contestPage");
            li.Attributes.Add("class", "current-menu-item");
        }

        protected Contest GetContest(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Contests WHERE ID = @id and Status = 1";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = id;
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();
                    int newsID = (int)reader["ID"];
                    string title = (string)reader["Title"];
                    string text = (string)reader["Text"];
                    string smallImagePath = (string)reader["SmallImagePath"];
                    string largeImagePath = (string)reader["LargeImagePath"];
                    bool status = Boolean.Parse(reader["Status"].ToString());
                    string email = (string)reader["Email"];
                    DateTime date = (DateTime)reader["Date"];
                    Contest contest = new Contest(newsID, title, text, smallImagePath, largeImagePath, status, email, date.AddHours(2));
                    return contest;
                }
                catch (Exception ex)
                {
                    //error message
                    return null;
                }
            }
        }

        protected void SetFields(Contest contest)
        {
            ContestImage.Src = contest.LargeImagePath;
            TitleSpan.InnerText = contest.Title;
            DateSpan.InnerText = contest.Date.ToString();
            EmailSpan.InnerText = contest.Email;
            TextSpan.InnerHtml = contest.Text;

            SidebarHelpEmail.InnerText = contest.Email;
            SidebarDateOfPublication.InnerText = contest.Date.ToString();
        }
    }
}