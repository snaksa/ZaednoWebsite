using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullSchoolWebsite.AdminPanel.Classes
{
    public class PostOnTheWall
    {
        private string name;
        private string email;
        private string message;
        private int id;

        public PostOnTheWall(int id, string name, string email, string message)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.message = message;
        }

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }

        public string Message
        {
            get { return this.message; }
            set { this.message = value; }
        }
    }
}