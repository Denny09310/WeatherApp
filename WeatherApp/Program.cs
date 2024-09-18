using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using WeatherApp;
using WeatherApp.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ResponseCachingHandler>();

builder.Services.AddRefitClient<IWeatherClient>()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://api.open-meteo.com/v1"))
    .AddHttpMessageHandler<ResponseCachingHandler>()
    .AddStandardResilienceHandler();

await builder.Build().RunAsync();
