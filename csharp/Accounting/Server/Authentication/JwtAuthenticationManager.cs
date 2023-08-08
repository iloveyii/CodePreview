using Accounting.Client.Authentication;
using Accounting.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq.Dynamic.Core.Tokenizer;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Accounting.Server.Authentication
{
    public class JwtAuthenticationManager
    {
        private UserAccountService userAccountService;
        public const string JWT_SECURITY_KEY = "yPGhwjdggnsjWitnvjd73Djwu9Swi834";
        private const int JWT_TOKEN_VALIDITY_MINUTES = 25;
        public JwtAuthenticationManager(UserAccountService userAccountService)
        {
            this.userAccountService = userAccountService;
        }

        public UserSession GenerateJwtToken(string userName, string password)
        {
            if(string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return null;
            /* Validating the user credentials */
            var userAccount = userAccountService.GetUserAccountByUserName(userName);
            if(userAccount == null || userAccount.Password != password) 
                return null;
            /* Generate JWT token */
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINUTES);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userAccount.UserName),
                new Claim(ClaimTypes.Role, userAccount.Role),
            });
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature
                );
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            /*Create UserSession */
            var userSession = new UserSession
            {
                UserName = userAccount.UserName,
                Role = userAccount.Role,
                Token = token,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };
            return userSession;
        }

        public UserSession GetUserSessionFromBearerToken(HttpRequest request)
        {
            JwtSecurityToken? token = GetTokenFromRequest(request);
            if (token == null || ( !IsValidToken(request)))
            {
                return new UserSession();
            }
            var name = token.Claims.First(claim => claim.Type == "unique_name").Value;
            var role = token.Claims.First(claim => claim.Type == "role").Value;
            var userSession = new UserSession
            {
                UserName = name,
                Role = role,
            };

            return userSession;
        }


        private string GetStringToken(HttpRequest request)
        {
            var jwt = string.Empty;

            if (request.Headers.Keys.Contains("Authorization"))
            {
                StringValues values;
                if (request.Headers.TryGetValue("Authorization", out values))
                {
                    jwt = values.ToString();
                    if (jwt.Contains("Bearer"))
                    {
                        jwt = jwt.Replace("Bearer", "").Trim();
                    }
                    if (jwt.Contains("bearer"))
                    {
                        jwt = jwt.Replace("bearer", "").Trim();
                    }
                }
            }
            return jwt;

        }
        private JwtSecurityToken? GetTokenFromRequest(HttpRequest request)
        {
            JwtSecurityToken token = null;
            var jwt = GetStringToken(request);
            if (!string.IsNullOrEmpty(jwt))
            {
                var handler = new JwtSecurityTokenHandler();
                token = handler.ReadJwtToken(jwt);
            }

            return token;
        }
    
        public bool IsValidToken(HttpRequest request)
        {
            var stringToken = GetStringToken(request);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var securityKey = new SymmetricSecurityKey(tokenKey);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                jwtSecurityTokenHandler.ValidateToken(stringToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);
            } catch
            {
                return false;
            }
            return true;

        }
    }
}
