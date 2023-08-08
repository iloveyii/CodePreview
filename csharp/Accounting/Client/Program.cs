using Accounting.Client;
using Accounting.Client.Authentication;
using Accounting.Client.Extensions;
using Accounting.Client.Services;
using Blazored.SessionStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<JSFunctionHandler, JSFunctionHandler>();
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddScoped<CustomerService, CustomerService>();
builder.Services.AddBlazoredSessionStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddBlazoredToast();
builder.Services.AddHttpClient();

await builder.Build().RunAsync();
