using Accounting.Client.Extensions;
using Accounting.Shared;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Accounting.Client.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService sessionStorage;
        private readonly ClaimsPrincipal anonymous = new ClaimsPrincipal( new ClaimsIdentity());
        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorage)
        {
            this.sessionStorage = sessionStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await sessionStorage.ReadEncryptedItemAsync<UserSession>("UserSession");
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(anonymous));

                var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim(ClaimTypes.Role, userSession.Role)
                }, "JwtAuth"));

                return await Task.FromResult(new AuthenticationState(claimPrincipal));
            } catch
            {
                return await Task.FromResult(new AuthenticationState(anonymous));

            }
        }

        public async Task UpdateAuthenticationState(UserSession? userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if(userSession == null)
            {
                await sessionStorage.RemoveItemAsync("UserSession");
                claimsPrincipal = anonymous;
            } else
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim(ClaimTypes.Role, userSession.Role)
                }));
                userSession.ExpiryTimestamp = DateTime.Now.AddSeconds(userSession.ExpiresIn);
                await sessionStorage.SaveItemEncryptedAsync("UserSession", userSession);
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task<string> GetToken()
        {
            var result = string.Empty;
            try
            {
                var userSession = await sessionStorage.ReadEncryptedItemAsync<UserSession>("UserSession");
                
                if(userSession != null && DateTime.Now < userSession.ExpiryTimestamp)
                {
                    result = userSession.Token;
                }
            } catch { }

            return result;
        }
    }
}
