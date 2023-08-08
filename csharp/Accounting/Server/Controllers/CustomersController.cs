using Accounting.Server.Authentication;
using Accounting.Server.Storage;
using Accounting.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Accounting.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<Customer> customersRepository;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        public CustomersController(IRepository<Customer> customersRepository, JwtAuthenticationManager jwtAuthenticationManager)
        {
            this.customersRepository = customersRepository;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return customersRepository.GetAll()
                .OrderBy(customer => customer.Name);
        }

    }
}
