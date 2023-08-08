using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Shared
{
    public class Response
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public UserSession UserSession { get; set; }
    }
}
