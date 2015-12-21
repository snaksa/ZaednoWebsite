using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullSchoolWebsite.AdminPanel.Classes
{
    public class Gallery
    {
        private int id;
        private string title;
        private string description;
        private string imagePath;
        private bool status;
        private int numberOfImages;

        public Gallery(int id, string title, string description, bool status)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.status = status;
        }

        public Gallery(int id, string title, string description, bool status, string imagePath)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.status = status;
            this.imagePath = imagePath;
        }

        public Gallery(int id, string title, bool status, int numberOfImages)
        {
            this.id = id;
            this.title = title;
            this.status = status;
            this.numberOfImages = numberOfImages;
        }

        public int ID
        {
            get { return this.id; }
        }

        public string Title
        {
            get { return this.title; }
        }

        public string Description
        {
            get { return this.description; }
        }

        public string ImagePath
        {
            get { return this.imagePath; }
        }

        public bool Status
        {
            get { return this.status; }
        }

        public int NumberOfImages
        {
            get { return this.numberOfImages; }
        }
        
    }
}