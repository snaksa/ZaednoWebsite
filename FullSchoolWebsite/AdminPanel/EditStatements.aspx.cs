using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace AdminPanel
{
    public partial class EditStatements : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void EditStatementButton_Click(object sender, EventArgs e)
        {
            if (TextBoxTitle.Text == String.Empty || TextBoxDescription.Text == String.Empty || Request.Form["imgGroup"] == null)
            { 
                //error message
                AlertBox.Attributes["class"] = "alert_warning";
                AlertBox.Visible = true;
                AlertBox.InnerText = "Моля попълнете всички полета!";
                return;
            }

            string title = TextBoxTitle.Text;
            string description = TextBoxDescription.Text;
            int position = Int32.Parse(Position.SelectedValue);
            string image = Request.Form["imgGroup"].ToString();

            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "UPDATE Statements " +
                "SET Title = @title, Text = @text, ImagePath = @imagePath WHERE Position = @position";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);

                    command.Parameters.Add("@title", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@text", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@imagePath", System.Data.SqlDbType.NVarChar);
                    command.Parameters.Add("@position", System.Data.SqlDbType.Int);

                    command.Parameters["@title"].Value = title;
                    command.Parameters["@text"].Value = description;
                    command.Parameters["@imagePath"].Value = "/AdminPanel/images/" + image;
                    command.Parameters["@position"].Value = position;

                    con.Open();
                    command.ExecuteNonQuery();

                    //success message
                    AlertBox.Attributes["class"] = "alert_success";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Изявлението беше редактирано успешно!";
                }
                catch (Exception ex)
                { 
                    //error message
                    AlertBox.Attributes["class"] = "alert_error";
                    AlertBox.Visible = true;
                    AlertBox.InnerText = "Текстът е прекалено дълъг!";
                }
            }
        }
    }
}