using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FullSchoolWebsite.AdminPanel.Classes
{
    public class MyImage
    {
        private string savePath;
        private HttpPostedFile postedImage;

        public MyImage(string path, HttpPostedFile file)
        {
            savePath = "/AdminPanel" + path;
            postedImage = file;
        }

        private string SavePath
        {
            get { return this.savePath; }
            set { this.savePath = value; }
        }

        private HttpPostedFile PostedImage
        {
            get { return this.postedImage; }
            set { this.postedImage = value; }
        }

        public string UploadImageToServer()
        {
            string fileName = Path.GetFileName(this.postedImage.FileName);
            string extension = Path.GetExtension(fileName).ToLower();

            if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
            {
                int count = 2;
                string tempName = "";
                string nameToCheck = fileName;
                while (File.Exists(HttpContext.Current.Server.MapPath(this.savePath) + nameToCheck))
                {
                    tempName = count + fileName;
                    nameToCheck = tempName;
                    count++;
                }

                fileName = nameToCheck;
                this.postedImage.SaveAs(HttpContext.Current.Server.MapPath(this.savePath) + fileName);
            }
            else
            {
                throw new ArgumentException("Може да добавяте само снимки!");
                //return exception
            }

            return savePath + fileName;
        }


    }
}