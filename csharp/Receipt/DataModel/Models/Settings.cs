using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class Settings
    {
        public int Id { get; set; }
        // Api tab
        public string ApiEndpoint { get; set; }
        public string ApiDatabasePath { get; set; }
        // Receipt tab
        public string ReceiptLogoPath { get; set; }
        public string ReceiptLogoPath2 { get; set; }
        public string ReceiptReceiptType { get; set; }
        public string ReceiptReceiptCut { get; set; }
        // Sync tab
    }
}
