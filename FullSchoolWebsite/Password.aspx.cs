using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;

namespace FullSchoolWebsite
{
    public partial class Password : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SentPassword_ServerClick(object sender, EventArgs e)
        {
            string email = emailTextBox.Value;
            string password = CheckIfEmailExists(email);

            if (password == null)
            {
                headerText.InnerText = "Не е намерен админ с този email!";
                return;
            }

            SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("snaksa2@live.com");
            mail.To.Add(email);
            mail.Subject = "Парола за администраторски профил - СОУ 'Христо Ботев' гр. Кубрат";
            mail.IsBodyHtml = false;
            string body;
            body = "Данните, с които можете да влезете в администраторския си профил са: \n Email: " + email + " \n Парола: " + password;
            mail.Body = body;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("snaksa2@live.com", "6703315158");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);

            headerText.InnerText = "Паролата е изпратена успешно!";
        }

        protected string CheckIfEmailExists(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NewsDBConnectionString"].ConnectionString;
            string query = "SELECT * FROM Admins WHERE Email = @email";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.Add("@email", System.Data.SqlDbType.NVarChar);
                    command.Parameters["@email"].Value = email;
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (!reader.HasRows) return null;

                    while (reader.Read())
                    {
                        string password = reader["Password"].ToString();
                        return password;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}