using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string? EmailAddress { get; set; }

        public override string ToString()
        {
           if(EmailAddress == null)
            {
                EmailAddress = string.Empty;
            }
           return EmailAddress;
        }
    }
}
