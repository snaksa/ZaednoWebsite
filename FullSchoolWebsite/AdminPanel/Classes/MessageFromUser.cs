using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullSchoolWebsite.AdminPanel.Classes
{
    public class MessageFromUser
    {
        private int id;
        private string senderName;
        private string senderEmail;
        private string message;
        private DateTime date;

        public MessageFromUser(int id, string senderName, string senderEmail, string message, DateTime date)
        {
            this.id = id;
            this.senderName = senderName;
            this.senderEmail = senderEmail;
            this.message = message;
            this.date = date;
        }

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string SenderName
        {
            get { return this.senderName; }
            set { this.senderName = value; }
        }

        public string SenderEmail
        {
            get { return this.senderEmail; }
            set { this.senderEmail = value; }
        }

        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }

        public DateTime Date
        {
            get { return this.date; }
            set { this.date = value; }
        }



    }
}