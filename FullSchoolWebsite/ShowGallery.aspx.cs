using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using FullSchoolWebsite.AdminPanel.Classes;

namespace FullSchoolWebsite
{
    public partial class ShowGallery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string galleryID = Request.QueryString["id"];
                if (galleryID == null)
                {
                    Session["ErrorMessage"] = "Не сте избрали галерия, която да бъде показана. ";
                    Response.Redirect("Error.aspx");
                }
                LoadImages(galleryID);
                string galleryName = GetGalleryName(galleryID);
                pageTitle.InnerText = galleryName + pageTitle.InnerText;
                headerTitle.InnerText = galleryName;
            }

            var li = (System.Web.UI.HtmlControls.HtmlGenericControl)this.Master.FindControl("galleryPage");
            li.Attributes.Add("class", "current-menu-item");
        }

        protected void LoadImages(string id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM PicturesInGalleries WHERE GalleryID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = Int32.Parse(id);

                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        throw new Exception();
                    }

                    List<PictureInGallery> pictures = new List<PictureInGallery>();
                    while (reader.Read())
                    {
                        int picID = Int32.Parse(reader["ID"].ToString());
                        string path = reader["ImagePath"].ToString();
                        int positiveVotes = Int32.Parse(reader["PositiveVotes"].ToString());
                        int negativeVotes = Int32.Parse(reader["NegativeVotes"].ToString());
                        PictureInGallery pic = new PictureInGallery(picID, path, positiveVotes, negativeVotes);
                        pictures.Add(pic);
                    }
                    AllImagesRepeater.DataSource = pictures;
                    AllImagesRepeater.DataBind();
                }
                catch (Exception ex)
                {
                    Session["ErrorMessage"] = "Галерията не съществува. Възможно е да е била премахната от администраторите. ";
                    Response.Redirect("Error.aspx");
                }
            }
        }

        protected string GetGalleryName(string id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Galleries WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = Int32.Parse(id);

                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        throw new Exception();
                    }

                    reader.Read();
                    return reader["Title"].ToString();
                }
                catch (Exception ex)
                {
                    Session["ErrorMessage"] = "Галерията не съществува. Възможно е да е била премахната от администраторите. ";
                    Response.Redirect("Error.aspx");
                    return null;
                }
            }
        }

        protected void PositiveVoteClicked_Command(object sender, CommandEventArgs e)
        {
            Session["PicVote" + e.CommandName] = "1";
            ScriptManager.RegisterStartupScript(this, GetType(), "inactive", "document.getElementById('galleryVoting').className='inactiveVoteIcons';", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "active", "document.getElementById('PositiveVoteButton" + e.CommandArgument + "').className='voteIconClicked';", true);

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "UPDATE PicturesInGalleries SET PositiveVotes = PositiveVotes + 1 WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = Int32.Parse(e.CommandName);
                    con.Open();
                    command.ExecuteNonQuery();
                    Response.Redirect("ShowGallery.aspx?id=" + Request.QueryString["id"]);
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void NegativeVoteClicked_Command(object sender, CommandEventArgs e)
        {
            Session["PicVote" + e.CommandName] = "0";
            ImageButton button = sender as ImageButton;
            button.CssClass = "voteIconClicked";
            ScriptManager.RegisterStartupScript(this, GetType(), "inactive", "document.getElementById('galleryVoting').className='inactiveVoteIcons';", true);
            
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "UPDATE PicturesInGalleries SET NegativeVotes = NegativeVotes + 1 WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@id", System.Data.SqlDbType.Int);
                    command.Parameters["@id"].Value = Int32.Parse(e.CommandName);
                    con.Open();
                    command.ExecuteNonQuery();
                    Response.Redirect("ShowGallery.aspx?id=" + Request.QueryString["id"]);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}