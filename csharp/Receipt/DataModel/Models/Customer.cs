using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataModel.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public System.Drawing.Image Prefix
        {
            get
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(PrefixPath());
                return img;
            }
        }

        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();

        private string PrefixPath()
        {
            var basePath = ConfigurationManager.AppSettings.Get("basePathIcons");

            if (FirstName != String.Empty)
            {
                return basePath + "/Letter-" + FirstName[0] + "-dg-icon.png";
            }
            return basePath != null ? basePath : "no-base-path-set";
        }
    }
}
