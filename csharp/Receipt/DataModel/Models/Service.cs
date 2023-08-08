using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<Service> ToServices(ICollection<ServiceDto> serviceDtos)
        {
            var services = new List<Service>();
            foreach (var serviceDto in serviceDtos)
            {
                Service service = new Service();
                service.Id = serviceDto.id;
                service.Name = serviceDto.name;
                service.Price = serviceDto.price;
                services.Add(service);
            }

            return services;
        }

        public static List<Service> FromOrderServices(List<OrderService> orderServices)
        {
            var services = new List<Service>();
            foreach (var orderService in orderServices)
            {
                Service service = new Service();
                service.Id = orderService.Id;
                service.Name = orderService.Name;
                service.Price = orderService.Price;
                services.Add(service);
            }

            return services.ToList();
        }

        public void ShowService()
        {
            Console.WriteLine($"Id:{Id}, Name:{Name}, Price:{Price}");
        }
    }

    public class ServiceDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
    }
}
