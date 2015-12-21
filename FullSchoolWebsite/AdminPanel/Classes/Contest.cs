using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullSchoolWebsite.AdminPanel.Classes
{
    public class Contest
    {
        private int id;
        private string title;
        private string text;
        private string smallImagePath;
        private string largeImagePath;
        private bool status;
        private string email;
        private DateTime date;

        public Contest(int id, string title, bool status, DateTime date)
        {
            this.id = id;
            this.title = title;
            this.status = status;
            this.date = date;
        }

        public Contest(int id, string title, string text, bool status, string email)
        {
            this.id = id;
            this.title = title;
            this.text = text;
            this.status = status;
            this.email = email;
        }

        public Contest(int id, string title, string text, string smallImagePath, 
            string largeImagePath, bool status, string email, DateTime date)
        {
            this.id = id;
            this.title = title;
            this.text = text;
            this.smallImagePath = smallImagePath;
            this.largeImagePath = largeImagePath;
            this.status = status;
            this.email = email;
            this.date = date;
        }

        public int ID
        {
            get { return this.id; }
        }

        public string Title
        {
            get { return this.title; }
        }

        public string Text
        {
            get { return this.text; }
        }

        public string SmallImagePath
        {
            get { return this.smallImagePath; }
        }

        public string LargeImagePath
        {
            get { return this.largeImagePath; }
        }

        public bool Status
        {
            get { return this.status; }
        }

        public string Email
        {
            get { return this.email; }
        }

        public DateTime Date
        {
            get { return this.date; }
        }

    }
}