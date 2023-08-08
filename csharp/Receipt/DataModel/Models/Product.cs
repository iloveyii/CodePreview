using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class Product
    {
        //public int Id { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        //public virtual ICollection<Service> Services { get; set; }
        public List<Service> Services { get; set; } = new List<Service>();


        public ICollection<Product> ToProducts(ICollection<ProductDto> productDtos)
        {
            var products = new List<Product>();
            foreach (var productDto in productDtos)
            {
                Product product = new Product();
                product.Id = productDto.id;
                product.Name = productDto.name;
                var s = new Service();
                product.Services = (List<Service>) s.ToServices(productDto.services);
                products.Add(product);
            }

            return products;
        }

    }

    public class ProductDto
    {
        public string id { get; set; }
        public string name { get; set; }
        public virtual ICollection<ServiceDto> services { get; set; }
    }
}
