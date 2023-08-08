using Accounting.Server.Authentication;
using Accounting.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private Authentication.UserAccountService userAccountService;
        private JwtAuthenticationManager jwtAuthenticationManager;
        public AccountController(UserAccountService userAccountService, JwtAuthenticationManager jwtAuthenticationManager)
        {
            this.userAccountService = userAccountService;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public ActionResult<UserSession> Login([FromBody] LoginRequest loginRequest)
        {
            var userSession = jwtAuthenticationManager.GenerateJwtToken(loginRequest.UserName, loginRequest.Password);
            if(userSession is null)
                return Unauthorized();
            else 
                return userSession;
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public ActionResult<Response> RegisterUserAccount([FromBody] LoginRequest loginRequest)
        {
            var userSession = new UserSession();
            var response = new Response()
            {
                Status = "Fail",
                Message = "An unknow error occurred",
                UserSession = userSession
            };
            if(string.IsNullOrWhiteSpace(loginRequest.UserName) || string.IsNullOrWhiteSpace(loginRequest.Password))
            {
                response.Message = "User Name or Password is empty";
                return response;
            }
            if(loginRequest.UserName.Length > 50 ||  loginRequest.Password.Length > 50)
            {
                response.Message = "User Name or Password is too longer";
                return response;
            }
            if(userAccountService.UserNameExist(loginRequest.UserName))
            {
                response.Message = $"User Name {loginRequest.UserName} already registered.";
                return response;
            }

            var userAccount = new UserAccount
            {
                UserName = loginRequest.UserName,
                Password = loginRequest.Password,
                Role = "User"
            };
            userAccountService.AddUserAccount(userAccount);
            userSession = jwtAuthenticationManager.GenerateJwtToken(loginRequest.UserName, loginRequest.Password);
            response.Status = "Success";
            response.Message = $"User {loginRequest.UserName} registered successfully";
            response.UserSession = userSession;

            return response;
        }
    }
}
