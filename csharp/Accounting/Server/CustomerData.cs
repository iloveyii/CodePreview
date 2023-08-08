using Accounting.Server.Storage;
using Accounting.Shared;

namespace Accounting.Server
{
    public static class CustomerData
    {
        public static void AddCustomersRepository(this IServiceCollection services)
        {
            var customersRepository = new MemoryRepository<Customer>();

            customersRepository.Add(new Customer { Name = "Aqib" });
            customersRepository.Add(new Customer { Name = "Aqio" });
            customersRepository.Add(new Customer { Name = "Aqib & Pino" });
            customersRepository.Add(new Customer { Name = "Mano" });
            customersRepository.Add(new Customer { Name = "Mano & Piano" });
            customersRepository.Add(new Customer { Name = "Malo" });
            customersRepository.Add(new Customer { Name = "Malamo" });
            customersRepository.Add(new Customer { Name = "Malamjaba" });
            customersRepository.Add(new Customer { Name = "Montreal" });
            customersRepository.Add(new Customer { Name = "Sunny" });
            customersRepository.Add(new Customer { Name = "Suno tare" });
            customersRepository.Add(new Customer { Name = "Sunnyrio" });
            customersRepository.Add(new Customer { Name = "SAM" });
            customersRepository.Add(new Customer { Name = "Pinoccio" });

            services.AddSingleton<IRepository<Customer>>(customersRepository);
        }
    }
}
