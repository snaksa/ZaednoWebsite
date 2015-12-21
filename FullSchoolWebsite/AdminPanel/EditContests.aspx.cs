using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using FullSchoolWebsite.AdminPanel.Classes;

namespace AdminPanel
{
    public partial class EditContests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadContests();
            }
        }

        protected void LoadContests()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT ID, Title, Status, Date FROM Contests";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    List<Contest> allNews = new List<Contest>();
                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string title = (string)reader["Title"];
                        bool status = (bool)reader["Status"];
                        DateTime date = (DateTime)reader["Date"];

                        Contest news = new Contest(id, title, status, date.AddHours(2));
                        allNews.Add(news);
                    }
                    ContestsRepeater.DataSource = allNews;
                    ContestsRepeater.DataBind();
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

        protected void EditContestWithID_Command(object sender, CommandEventArgs e)
        {
            string id = e.CommandName;
            Response.Redirect("EditSingleContest.aspx?id=" + id);
        }

        protected void ButtonDeleteSelectedContests_Click(object sender, EventArgs e)
        {
            string contests = contestsToDelete.Value;
            if (contests == String.Empty)
            {
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля изберете конкурси, които да бъдат изтрити!";
                return;
            }

            string[] IDs = contests.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "DELETE FROM Contests WHERE ID = @id";

            foreach (string id in IDs)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    try
                    {
                        DeleteMainImage(Int32.Parse(id));
                        SqlCommand command = new SqlCommand(query, con);
                        command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                        command.Parameters["@id"].Value = Int32.Parse(id);
                        con.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //error message
                        return;
                    }
                }
            }
            LoadContests();
            //success message
            AlertBox.Attributes["class"] = "alert_success";
            AlertBox.Visible = true;
            AlertBox.InnerText = "Конкурсите бяха изтрити успешно!";
        }

        protected void DeleteMainImage(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Contests WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                con.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    try
                    {
                        string smallImagePath = reader["SmallImagePath"].ToString();
                        string largeImagePath = reader["LargeImagePath"].ToString();
                        System.IO.File.Delete(Server.MapPath(smallImagePath));
                        System.IO.File.Delete(Server.MapPath(largeImagePath));
                    }
                    catch (Exception ex)
                    { 
                        //error message
                    }
                }
            }
        }
    }
}