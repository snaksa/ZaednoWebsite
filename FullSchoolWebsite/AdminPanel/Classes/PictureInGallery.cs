using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FullSchoolWebsite.AdminPanel.Classes
{
    public class PictureInGallery
    {
        private int id;
        private string imagePath;
        private int positiveVotes;
        private int negativeVotes;

        public PictureInGallery(int id, string path, int pV, int nV)
        {
            this.id = id;
            this.imagePath = path;
            this.positiveVotes = pV;
            this.negativeVotes = nV;
        }

        public int ID
        {
            get { return this.id; }
        }

        public string ImagePath
        {
            get { return this.imagePath; }
        }

        public int PositiveVotes
        {
            get { return this.positiveVotes; }
        }

        public int NegativeVotes
        {
            get { return this.negativeVotes; }
        }   
    }
}