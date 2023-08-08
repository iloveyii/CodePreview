using DataAccess.Models;
using DataModel.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess.Models
{
    public class SqliteVendorAccess
    {
        public static void Init()
        {
            DatabaseFacade facade = new DatabaseFacade(new VendorContext());
            facade.EnsureCreated();
        }

        public static bool create(Vendor vendor)
        {
            using (VendorContext context = new VendorContext())
            {
                var vendors = context.Vendors.ToList();
                context.Vendors.Add(vendor);
                context.SaveChanges();
                return true;
            }
        }

        public static List<Vendor>? read()
        {
            using (VendorContext context = new VendorContext())
            {
                var vendors = context.Vendors.Include(v => v.Products).ThenInclude(p => p.Services);
                if (vendors == null)
                {
                    return null;
                }
                return vendors.ToList();
            }
        }
    }
}
