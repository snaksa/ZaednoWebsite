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
    public partial class Works : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    List<MyNews> allNews = LoadAllWorks();
                    int showResultsFromIndex;
                    string pageN = Request.QueryString["page"] == null ? "1" : Request.QueryString["page"];
                    int pageNumber = Int32.Parse(pageN);
                    int newsCount = allNews.Count;
                    if ((pageNumber - 1) * 3 > newsCount) showResultsFromIndex = -1;
                    else showResultsFromIndex = (pageNumber - 1) * 3;
                    ShowWorks(showResultsFromIndex, allNews);
                    LoadPager(allNews);
                }
                catch (Exception ex)
                { 
                    
                }
            }

            var li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("newsPage");
            li.Attributes.Add("class", "current-menu-item");
        }

        protected List<MyNews> LoadAllWorks()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM AcceptedWorks WHERE Status = 1 ORDER BY ID DESC";

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

        protected void ShowWorks(int index, List<MyNews> allNews)
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

            WorksToBeShownRepeater.DataSource = newsToBeShown;
            WorksToBeShownRepeater.DataBind();
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