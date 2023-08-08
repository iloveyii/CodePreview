using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared.Upload
{
    public class UploadingFile
    {
        public UploadingFile()
        {
            Done = false;
        }
        public IBrowserFile File { get; set; }
        public Stream Stream { get; set; }
        public string ImageSource { get; set; }
        public long Uploaded { get; set; }
        public double Percentage { get; set; }
        public bool Done { get; set; }  
    }
}
