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
    public partial class ReporterShowNews : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UpdateViews();
                string newsID = Request.QueryString["id"];
                if (newsID == null)
                {
                    Session["ErrorMessage"] = "Не сте избрали новина, която да бъде показана. ";
                    Response.Redirect("Error.aspx");
                }
                MyNews news = LoadNews(newsID);
                if (news == null)
                {
                    Session["ErrorMessage"] = "Новината не съществува. Възможно е да е била премахната от администраторите. ";
                    Response.Redirect("Error.aspx");
                }
                SetFields(news);
                LoadComments(news.ID);
            }

            var li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("reporterPage");
            li.Attributes.Add("class", "current-menu-item");
        }

        protected MyNews LoadNews(string id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM AcceptedNews WHERE ID = @id and Status = 1";

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
                        int posVotes = Int32.Parse(reader["PositiveVotes"].ToString());
                        int negVotes = Int32.Parse(reader["NegativeVotes"].ToString());
                        int views = Int32.Parse(reader["Views"].ToString());

                        MyNews news = new MyNews(newsID, title, text, imagePath, true, author, showEmail, email, date.AddHours(2), posVotes, negVotes, views);
                        return news;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    Response.Redirect("serverError.html");
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
            SidebarPositiveVotes.InnerText = news.PositiveVotes.ToString();
            SidebarNegativeVotes.InnerText = news.NegativeVotes.ToString();
            SidebarVisitors.InnerText = news.Views.ToString();

            string vote = Session["ReporterVote" + Request.QueryString["id"]] == null ? null : Session["ReporterVote" + Request.QueryString["id"]].ToString();
            if (vote != null)
            {
                if (vote == "0")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "inactive", "document.getElementById('voteDiv').className='inactiveVoteIcons';", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "active", "document.getElementById('negativeVoteImg').className='voteIconClicked';", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "inactive", "document.getElementById('voteDiv').className='inactiveVoteIcons';", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "active", "document.getElementById('positiveVoteImg').className='voteIconClicked';", true);
                }
            }
        }

        protected void LoadComments(int id)
        {
            int commentsCount = GetNumberOfComments(id);

            if (commentsCount == 0) numberOfComments.InnerText = "Няма налични коментари";
            else if (commentsCount == 1) numberOfComments.InnerText = "1 коментар";
            else numberOfComments.InnerText = commentsCount + " коментара";

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM ReporterComments WHERE NewsID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                List<NewsComment> comments = new List<NewsComment>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int commentID = Int32.Parse(reader["ID"].ToString());
                        string authorName = reader["AuthorName"].ToString();
                        string email = reader["AuthorEmail"].ToString();
                        string text = reader["Text"].ToString();
                        DateTime date = DateTime.Parse(reader["Date"].ToString());

                        NewsComment comment = new NewsComment(commentID, authorName, email, text, date.AddHours(2));
                        comments.Add(comment);
                    }
                }
                AllCommentsRepeater.DataSource = comments;
                AllCommentsRepeater.DataBind();
            }
        }

        protected int GetNumberOfComments(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT COUNT(*) FROM ReporterComments WHERE NewsID = @id";

            int count = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                con.Open();
                count = Int32.Parse(command.ExecuteScalar().ToString());
            }
            return count;
        }

        protected void PostComment_ServerClick(object sender, EventArgs e)
        {
            int id = Int32.Parse(Request.QueryString["id"]);
            string name1 = name.Value;
            string email1 = email.Value;
            string text1 = comments.Value;

            if (name1 == String.Empty || email1 == String.Empty || text1 == String.Empty)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Моля попълнете всички полета')", true);
                return;
            }

            name.Value = String.Empty;
            email.Value = String.Empty;
            comments.Value = String.Empty;

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "INSERT INTO ReporterComments(NewsID, AuthorName, AuthorEmail, Text, Date) VALUES(@newsID, @authorName, @authorEmail, @text, @date)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add("@newsID", System.Data.SqlDbType.Int);
                command.Parameters.Add("@authorName", System.Data.SqlDbType.NVarChar);
                command.Parameters.Add("@authorEmail", System.Data.SqlDbType.NVarChar);
                command.Parameters.Add("@text", System.Data.SqlDbType.NVarChar);
                command.Parameters.Add("@date", System.Data.SqlDbType.DateTime);

                command.Parameters["@newsID"].Value = id;
                command.Parameters["@authorName"].Value = name1;
                command.Parameters["@authorEmail"].Value = email1;
                command.Parameters["@text"].Value = text1;
                command.Parameters["@date"].Value = DateTime.Now.ToUniversalTime();

                con.Open();
                command.ExecuteNonQuery();
                LoadComments(id);
            }
        }

        protected void PositiveVote_Click(object sender, EventArgs e)
        {
            Session["ReporterVote" + Request.QueryString["id"]] = "1";
            ScriptManager.RegisterStartupScript(this, GetType(), "inactive", "document.getElementById('voteDiv').className='inactiveVoteIcons';", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "active", "document.getElementById('positiveVoteImg').className='voteIconClicked';", true);

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "UPDATE AcceptedNews SET PositiveVotes = PositiveVotes + 1 WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = Int32.Parse(Request.QueryString["id"]);
                    con.Open();
                    command.ExecuteNonQuery();
                    int n = Int32.Parse(SidebarPositiveVotes.InnerText) + 1;
                    Response.Redirect("ReporterShowNews.aspx?id=" + Request.QueryString["id"]);
                }
                catch (Exception ex)
                { 
                    
                }
            }            
        }

        protected void NegativeVote_Click(object sender, EventArgs e)
        {
            Session["ReporterVote" + Request.QueryString["id"]] = "0";
            ScriptManager.RegisterStartupScript(this, GetType(), "inactive", "document.getElementById('voteDiv').className='inactiveVoteIcons';", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "active", "document.getElementById('negativeVoteImg').className='voteIconClicked';", true);

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "UPDATE AcceptedNews SET NegativeVotes = NegativeVotes + 1 WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = Int32.Parse(Request.QueryString["id"]);
                    con.Open();
                    command.ExecuteNonQuery();
                    int n = Int32.Parse(SidebarNegativeVotes.InnerText) + 1;
                    Response.Redirect("ReporterShowNews.aspx?id=" + Request.QueryString["id"]);
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void UpdateViews()
        {
            if (Session["ReporterView" + Request.QueryString["id"]] != null)
            {
                DateTime time = DateTime.Parse(Session["ReporterView" + Request.QueryString["id"]].ToString());
                TimeSpan s = new TimeSpan(1, 0, 0);
                if (DateTime.Now - time < s)
                {
                    return;
                }
                else
                {
                    Session["ReporterView" + Request.QueryString["id"]] = DateTime.Now;
                }
            }
            else
            {
                Session["ReporterView" + Request.QueryString["id"]] = DateTime.Now;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "UPDATE AcceptedNews SET Views = Views + 1 WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = Int32.Parse(Request.QueryString["id"]);
                    con.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Response.Redirect("serverError.html");
                }
            }
        }
    }
}