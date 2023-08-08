using Accounting.Server.Authentication;
using Accounting.Server.Storage;
using Accounting.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Dynamic.Core.Tokenizer;

namespace Accounting.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EarningsController : Controller
    {
        private readonly IRepository<Earning> earningRepository;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;
        public EarningsController(IRepository<Earning> earningRepository, JwtAuthenticationManager jwtAuthenticationManager)
        {
            this.earningRepository = earningRepository;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpGet]
        public IEnumerable<Earning> Get()
        {
            var userSession = jwtAuthenticationManager.GetUserSessionFromBearerToken(Request);
            if( string.IsNullOrEmpty( userSession.UserName) )
            {
                var earinigs = new List<Earning>()
                {
                    new Earning() {}
                };
                return earinigs;
            }

            return earningRepository.GetAll()
                .Where(x => x.UserName == userSession.UserName)
                .OrderBy(earning => earning.Date);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public void Post(Earning earning)
        {
            var userSession = jwtAuthenticationManager.GetUserSessionFromBearerToken(Request);
            earning.UserName = userSession.UserName;
            earningRepository.Add(earning);
        }

        [HttpDelete("{id?}")]
        [Authorize(Roles = "User")]
        public void Delete(Guid id)
        {
            var userSession = jwtAuthenticationManager.GetUserSessionFromBearerToken(Request);
            var entity = earningRepository.GetAll()
                .Single(item => item.Id == id);
            if(entity != null && entity.UserName == userSession.UserName)
            {
                earningRepository.Remove(entity);
            }
        }
    }
}
