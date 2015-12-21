using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullSchoolWebsite.AdminPanel.Classes
{
    public class Admin
    {
        private int id;
        private string name;
        private string password;
        private string email;
        private string role;

        public Admin(int id, string name, string password, string email, string role)
        {
            this.id = id;
            this.name = name;
            this.password = password;
            this.email = email;
            this.role = role;
        }

        public int ID
        {
            get { return this.id; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public string Password
        {
            get { return this.password; }
        }

        public string Email
        {
            get { return this.email; }
        }

        public string Role
        {
            get { return this.role; }
        }
    }
}