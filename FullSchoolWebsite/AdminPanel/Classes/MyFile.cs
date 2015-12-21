using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullSchoolWebsite.AdminPanel.Classes
{
    public class MyFile
    {
        private int id;
        private string name;
        private string description;
        private string imagePath;
        private string filePath;

        public MyFile(int id, string name, string description, string imagePath, string filePath)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.imagePath = imagePath;
            this.filePath = filePath;
        }

        public MyFile(int id, string name, string description, string filePath)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.filePath = filePath;
        }

        public MyFile(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int ID
        {
            get { return this.id; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public string Description
        {
            get { return this.description; }
        }

        public string ImagePath
        {
            get { return this.imagePath; }
        }

        public string FilePath
        {
            get { return this.filePath; }
        }
    }
}