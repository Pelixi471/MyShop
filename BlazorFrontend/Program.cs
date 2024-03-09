using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyShop.FrontendBlazor;
using MudBlazor.Services;
using MyShop.ShopClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient() { 
    Timeout = TimeSpan.FromSeconds(5)
});
builder.Services.AddSingleton(provider 
    => new ShopClient(provider.GetRequiredService<HttpClient>(), "https://localhost:7183"));

//builder.Services.AddScoped<BlazorGeneratedApiClient>(provider => 
//{
//    var httpClient = provider.GetRequiredService<HttpClient>();
//    // API requires no authentication, so use the anonymous
//    // authentication provider
//    var authProvider = new AnonymousAuthenticationProvider();
//    // Create request adapter using the HttpClient-based implementation
//    var adapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient)
//    {
//        BaseUrl = "https://localhost:7183"
//    };
//    // Create the API client
//    var client = new BlazorGeneratedApiClient(adapter);
//    return client;
//});



//builder.Services.AddScoped<IProductCatalog, ProductCatalog>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
