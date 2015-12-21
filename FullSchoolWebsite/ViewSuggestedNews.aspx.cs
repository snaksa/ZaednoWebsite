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
    public partial class ViewSuggestedNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string newsID = Request.QueryString["id"];
                if (newsID == null) Response.Redirect("Default.aspx");

                MyNews news = LoadNews(newsID);
                if (news == null) Response.Redirect("Default.aspx");
                SetFields(news);
            }

            var li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("reporterPage");
            li.Attributes.Add("class", "current-menu-item");
        }

        protected MyNews LoadNews(string id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM SuggestedNews WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = Int32.Parse(id);

                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows) return null;

                    while (reader.Read())
                    {
                        int newsID = Int32.Parse(reader["ID"].ToString());
                        string title = reader["Title"].ToString();
                        string text = reader["Text"].ToString();
                        string imagePath = reader["ImagePath"].ToString();
                        string author = reader["Author"].ToString();
                        bool showEmail = Boolean.Parse(reader["ShowEmail"].ToString());
                        string email = reader["Email"].ToString();
                        DateTime date = DateTime.Parse(reader["Date"].ToString());

                        MyNews news = new MyNews(newsID, title, text, imagePath, true, author, showEmail, email, date.AddHours(2));
                        return news;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        protected void SetFields(MyNews news)
        {
            pageTitle.InnerText = news.Title + pageTitle.InnerText;

            NewsImage.Src = news.ImagePath;
            TitleSpan.InnerText = news.Title;
            TextSpan.InnerHtml = news.Text;
            AuthorNameSpan.InnerText = news.Author;
            DateSpan.InnerText = news.Date.ToString();

            SidebarAuthorName.InnerText = news.Author;
            SidebarAuthorEmail.InnerText = news.Email;
            SidebarDateOfPublication.InnerText = news.Date.ToString();
            if (news.ShowEmail == true) authorEmailListElement.Visible = true;

        }
    }
}