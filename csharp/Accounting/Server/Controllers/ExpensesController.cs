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
    public class ExpensesController : Controller
    {
        private readonly IRepository<Expense> expenseRepository;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public ExpensesController(IRepository<Expense> expenseRepository, JwtAuthenticationManager jwtAuthenticationManager)
        {
            this.expenseRepository = expenseRepository;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpGet]
        //[Authorize(Roles = "Administrator,User")]
        public IEnumerable<Expense> Get()
        {
            var userSession = jwtAuthenticationManager.GetUserSessionFromBearerToken(Request);
            return expenseRepository.GetAll()
                .Where(x => x.UserName == userSession.UserName)
                .OrderBy(expense => expense.Date);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public void Post(Expense expense)
        {
            var userSession = jwtAuthenticationManager.GetUserSessionFromBearerToken(Request);
            expense.UserName = userSession.UserName;
            expenseRepository.Add(expense);
        }

        [HttpDelete("{id?}")]
        [Authorize(Roles = "User")]
        public void Delete(Guid id)
        {
            var userSession = jwtAuthenticationManager.GetUserSessionFromBearerToken(Request);
            var entity = expenseRepository.GetAll()
                .Single(item => item.Id == id);
            if (entity != null && entity.UserName == userSession.UserName)
            {
                expenseRepository.Remove(entity);
            }
        }
    }
}
