using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullSchoolWebsite.AdminPanel.Classes
{
    public class Statement
    {
        private string title;
        private string text;
        private string imagePath;

        public Statement(string title, string text, string imagePath)
        {
            this.title = title;
            this.text = text;
            this.imagePath = imagePath;
        }

        public string Title
        {
            get { return this.title; }
        }

        public string Text
        {
            get { return this.text; }
        }

        public string ImagePath
        {
            get { return this.imagePath;}
        }
    }
}