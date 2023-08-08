using Accounting.Client.Authentication;
using Accounting.Client.Components;
using Accounting.Client.Components.Expense;
using Accounting.Client.Pages;
using Accounting.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.Net.Http.Json;


namespace Accounting.Client.Services
{
    public class CustomerService
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        public CustomerService(HttpClient httpClient, AuthenticationStateProvider authStateProvider)
        {
            this.httpClient = httpClient;
            this.authenticationStateProvider = authStateProvider;
        }

        private async Task SetAuthHeader()
        {
            try
            {
                var customAuthStateService = (CustomAuthenticationStateProvider)authenticationStateProvider;
                var token = await customAuthStateService.GetToken();
                if (!string.IsNullOrWhiteSpace(token))
                {
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                }
            }
            catch { }
        }

        public async Task<Customer[]> LoadCustomers()
        {
            //await SetAuthHeader();
            var data = await httpClient.GetFromJsonAsync<Customer[]>("api/Customers");
            return data;
        }
    }

}