using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullSchoolWebsite.AdminPanel.Classes
{
    public class MyNews
    {
        private int id;
        private string title;
        private string text;
        private string imagePath;
        private bool status;
        private string author;
        private bool showEmail;
        private string email;
        private DateTime date;
        private int positiveVotes;
        private int negativeVotes;
        private int views;

        public MyNews(int id, string title, string author, string email, DateTime date)
        {
            this.id = id;
            this.title = title;
            this.author = author;
            this.date = date;
            this.email = email;
        }


        public MyNews(int id, string title, bool status, string author, DateTime date)
        {
            this.id = id;
            this.title = title;
            this.status = status;
            this.author = author;
            this.date = date;
        }

        public MyNews(int id, string title, string text, string author, string imagePath, DateTime date)
        {
            this.id = id;
            this.title = title;
            this.text = text;
            this.author = author;
            this.imagePath = imagePath;
            this.date = date;
        }

        public MyNews(int id, string title, string text, string imagePath, bool status, 
            string author, bool showEmail, string email, DateTime date)
        {
            this.id = id;
            this.title = title;
            this.text = text;
            this.imagePath = imagePath;
            this.status = status;
            this.author = author;
            this.showEmail = showEmail;
            this.email = email;
            this.date = date;
        }

        public MyNews(int id, string title, string text, string imagePath, bool status,
            string author, bool showEmail, string email, DateTime date, int positiveVotes, int negativeVotes, int views)
        {
            this.id = id;
            this.title = title;
            this.text = text;
            this.imagePath = imagePath;
            this.status = status;
            this.author = author;
            this.showEmail = showEmail;
            this.email = email;
            this.date = date;
            this.positiveVotes = positiveVotes;
            this.negativeVotes = negativeVotes;
            this.views = views;
        }

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public string ImagePath
        {
            get { return this.imagePath; }
            set { this.imagePath = value; }
        }

        public bool Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public string Author
        {
            get { return this.author; }
            set { this.author = value; }
        }

        public bool ShowEmail
        {
            get { return this.showEmail; }
            set { this.showEmail = value; }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        public DateTime Date
        {
            get { return this.date; }
            set { this.date = value; }
        }

        public int PositiveVotes
        {
            get { return positiveVotes; }
            set { this.positiveVotes = value; }
        }

        public int NegativeVotes
        {
            get { return negativeVotes; }
            set { this.negativeVotes = value; }
        }

        public int Views
        {
            get { return this.views; }
            set { this.views = value; }
        }
    }
}