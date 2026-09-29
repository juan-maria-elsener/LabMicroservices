using Frontend.Blazor;
using Frontend.Blazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var customerApiUrl = builder.Configuration["MicroservicesUrls:CustomerApi"];
var productApiUrl = builder.Configuration["MicroservicesUrls:ProductApi"];
var orderApiUrl = builder.Configuration["MicroservicesUrls:OrderApi"];

builder.Services.AddHttpClient("CustomerApi", client => client.BaseAddress = new Uri(customerApiUrl!));
builder.Services.AddHttpClient("ProductApi", client => client.BaseAddress = new Uri(productApiUrl!));
builder.Services.AddHttpClient("OrderApi", client => client.BaseAddress = new Uri(orderApiUrl!));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<CustomerApiService>();

builder.Services.AddScoped<ProductApiService>();

builder.Services.AddScoped<OrderApiService>();

await builder.Build().RunAsync();