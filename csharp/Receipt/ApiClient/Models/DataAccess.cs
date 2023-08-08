using DataModel.Models;
using DbAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiClient.Models
{
    public class DataAccess
    {
        static HttpClient client = new HttpClient();

        // Get all vendors
        public static async Task<List<Vendor>> GetVendorsAsync(string path)
        {
            List<Vendor> vendors = new List<Vendor>() { };
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                ResultDto result = JsonSerializer.Deserialize<ResultDto>(data);

                foreach (VendorDto vendorDto in result.vendors)
                {
                    Vendor vendor = new Vendor();
                    vendor.Id = vendorDto.id;
                    vendor.Name = vendorDto.name;
                    var p = new Product();
                    vendor.Products = (List<Product>) p.ToProducts(vendorDto.products);
                    vendors.Add(vendor);
                }
                return vendors;
            }

            return null;
        }

        public static async Task RunAsync()
        {
            client.BaseAddress = new Uri(LoadUriString());
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Get all products
                List<Vendor> vendors = await GetVendorsAsync("api/v1/vendors");
                var opt = new JsonSerializerOptions() { WriteIndented = true };
                string str = JsonSerializer.Serialize<List<Vendor>>(vendors, opt);
                Console.WriteLine(str);
                client = new HttpClient();

                SqliteVendorAccess.Init();

                foreach(var vendor in vendors)
                {
                    SqliteVendorAccess.create(vendor);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static string LoadUriString(string endPoint = "baseUri")
        {
            return ConfigurationManager.AppSettings.Get(endPoint);
        }

        public static void Run()
        {
            RunAsync().GetAwaiter().GetResult();
        }
    }

}
