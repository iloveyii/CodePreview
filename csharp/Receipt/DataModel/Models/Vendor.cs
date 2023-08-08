using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class Vendor
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        //public virtual ICollection<Product> Products { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }

    public class VendorDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public virtual ICollection<ProductDto> products { get; set; }
    }

}
