using Accounting.Client.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Runtime.CompilerServices;

namespace Accounting.Client.Authentication
{
    public static class Helper
    {
        public static async Task<bool> UserLoggedIn(AuthenticationStateProvider authStateProvider)
        {
            try
            {
                var customAuthStateService = (CustomAuthenticationStateProvider)authStateProvider;
                var token = await customAuthStateService.GetToken();
                if (string.IsNullOrWhiteSpace(token))
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
