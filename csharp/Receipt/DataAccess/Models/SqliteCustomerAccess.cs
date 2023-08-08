using DataAccess.Models;
using DataModel.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Storage;

namespace DbAccess.Models
{
    public class SqliteCustomerAccess
    {
        public static void Init()
        {
            DatabaseFacade facade = new DatabaseFacade(new CustomerContext());
            facade.EnsureCreated();
        }

        public static bool create(Customer customer)
        {
            using (CustomerContext context = new CustomerContext())
            {
                var customers = context.Customers.ToList();
                context.Customers.Add(customer);
                context.SaveChanges();
                return true;
            }
        }

        public static List<Customer>? read()
        {
            using (CustomerContext context = new CustomerContext())
            {
                var customers = context.Customers.Include(c => c.Orders).ThenInclude(o => o.OrderServices);
                if (customers == null)
                {
                    return null;
                }
                return customers.ToList();
            }
        }

        public static Customer? readOne(int Id)
        {
            var customers = read();
            var customer = customers.Find(x => x.Id == Id);
            return customer;
        }

        public static Customer? update(Customer customer)
        {
            var _customer = readOne(customer.Id);
            var orderServices = _customer.Orders.ToList().First().OrderServices;
            //removeOrderServices( orderServices);
            using (CustomerContext context = new CustomerContext())
            {
                if (_customer != null)
                {
                    _customer = customer;
                    context.Update(customer);
                    context.SaveChanges();
                    return _customer;
                }
                return null;
            }
        }

        /**
         * If Order.Id exists in new order then update it in the older
         * 
         * */
        private static void addOrder(List<Order> olds, List<Order> news)
        {
            foreach(var newOrder in news)
            {
                var _exists = olds.Find(x =>x.Id == newOrder.Id);
                if(_exists == null) 
                { 
                    olds.Add(newOrder); 
                } else
                {
                    _exists = newOrder;
                }
            }
        }

        private static bool removeOrderServices(  List<OrderService> orderServices)
        {
            using (CustomerContext context = new CustomerContext())
            {
                foreach (var orderService in orderServices)
                {
                    context.Remove(orderService);
                }
                context.SaveChanges();
            }
            return true;
        }

        public static bool deleteOrderService(OrderService orderService)
        {
            using (CustomerContext context = new CustomerContext())
            {
                context.Remove(orderService);
                context.SaveChanges();
            }

            return true;
        }

        public static bool deleteCustomer(Customer customer)
        {
            using (CustomerContext context = new CustomerContext())
            {
                context.Remove(customer);
                context.SaveChanges();
            }

            return true;
        }
    }
}
