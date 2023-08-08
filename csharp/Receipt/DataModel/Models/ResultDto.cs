using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class ResultDto
    {
        public bool success { get; set; }
        public VendorDto[] vendors { get; set; }
    }
}
