using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using FullSchoolWebsite.AdminPanel.Classes;

namespace FullSchoolWebsite
{
    public partial class Reporter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<MyNews> allNews = LoadAllNews();
                int showResultsFromIndex;
                string pageN = Request.QueryString["page"];
                if (pageN == null) pageN = "1";
                int pageNumber = Int32.Parse(pageN);
                int newsCount = allNews.Count;
                if ((pageNumber - 1) * 3 > newsCount) showResultsFromIndex = -1;
                else showResultsFromIndex = (pageNumber - 1) * 3;
                ShowNews(showResultsFromIndex, allNews);
                LoadPager(allNews);
            }

            var li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("reporterPage");
            li.Attributes.Add("class", "current-menu-item");
        }

        protected List<MyNews> LoadAllNews()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM AcceptedNews WHERE Status = 1 ORDER BY ID DESC";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<MyNews> allNews = new List<MyNews>();
                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string title = (string)reader["Title"];
                        string text = (string)reader["Text"];
                        string author = (string)reader["Author"];
                        string imagePath = (string)reader["ImagePath"];
                        DateTime date = (DateTime)reader["Date"];
                        MyNews news = new MyNews(id, title, text, author, imagePath, date.AddHours(2));
                        allNews.Add(news);
                    }
                    return allNews;
                }
                catch (Exception ex)
                {
                    Session["ErrorMessage"] = "Тази страница не съществува. ";
                    Response.Redirect("Error.aspx");
                    return null;
                }
            }
        }

        protected void ShowNews(int index, List<MyNews> allNews)
        {
            if (index == -1)
            {
                Session["ErrorMessage"] = "Тази страница не съществува. ";
                Response.Redirect("Error.aspx");
            }

            List<MyNews> newsToBeShown = new List<MyNews>();
            int count = 0;
            while (count < 3 && index < allNews.Count)
            {
                MyNews n = allNews[index];
                n.Text = Regex.Replace(n.Text, "<.*?>", String.Empty);
                newsToBeShown.Add(n);
                count++;
                index++;
            }

            NewsToBeShownRepeater.DataSource = newsToBeShown;
            NewsToBeShownRepeater.DataBind();
        }

        protected void LoadPager(List<MyNews> allNews)
        {
            List<int> pages = new List<int>();
            int newsCount = allNews.Count;
            int pagesCount;
            if (newsCount % 3 == 0) pagesCount = newsCount / 3;
            else pagesCount = newsCount / 3 + 1;

            int n = 1;
            while (n <= pagesCount)
            {
                pages.Add(n);
                n++;
            }

            PagerRepeater.DataSource = pages;
            PagerRepeater.DataBind();
        }
    }
}