using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public List<OrderService> OrderServices { get; set; } = new List<OrderService>();

        public decimal TotalPrice {
            get
            {
                decimal _total = 0;
                foreach (var service in OrderServices)
                {
                    _total = _total + service.Price;
                    Total = _total;
                }
                return decimal.Round( _total, 2);
            }
        }
    }
}
