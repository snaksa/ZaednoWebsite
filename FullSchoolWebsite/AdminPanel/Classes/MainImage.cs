using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullSchoolWebsite.AdminPanel.Classes
{
    public class MainImage
    {
        private string imagePath;
        private int position;

        public MainImage(string imagePath, int position)
        {
            this.imagePath = imagePath;
            this.position = position;
        }
        public string ImagePath
        {
            get { return this.imagePath; }
        }

        public int Position
        {
            get { return this.position; }
        }
    }
}