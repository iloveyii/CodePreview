using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class OrderService
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public static List<OrderService> ToOrderServices(List<Service> selectedServices)
        {
            var services = new List<OrderService>();
            foreach(Service service in selectedServices)
            {
                OrderService orderService = new OrderService();
                orderService.OrderId = service.Id;
                orderService.Name = service.Name;
                orderService.Price = service.Price;
                services.Add(orderService);
            }
            return services;
        }

        public static OrderService FromService(Service service)
        {
            OrderService orderService = new OrderService();
            orderService.ServiceId = service.Id;
            orderService.Name = service.Name;
            orderService.Price = service.Price;
            return orderService;
        }
    }
}
