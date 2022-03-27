using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WeddingInvitation;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
if (builder.HostEnvironment.Environment == "Development")
{
    builder.Services.AddScoped(x=>new ApiClient(false));
}
else
{
    builder.Services.AddScoped(x=>new ApiClient(true));
}
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
await builder.Build().RunAsync();