using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullSchoolWebsite.AdminPanel.Classes
{
    public class NewsComment
    {
        private int id;
        private string authorName;
        private string authorEmail;
        private string text;
        private DateTime date;

        public NewsComment(int id, string authorName, string authorEmail, string text, DateTime date)
        {
            this.id = id;
            this.authorName = authorName;
            this.authorEmail = authorEmail;
            this.text = text;
            this.date = date;
        }

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string AuthorName
        {
            get { return this.authorName; }
            set { this.authorName = value; }
        }

        public string AuthorEmail
        {
            get { return this.authorEmail; }
            set { this.authorEmail = value; }
        }

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public DateTime Date
        {
            get { return this.date; }
            set { this.date = value; }
        }
    }
}