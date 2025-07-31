using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/.default");
});

builder.Services.Configure<PhotoAnalyzer.Client.Services.AzureAdOptions>(options =>
    builder.Configuration.GetSection("AzureAd").Bind(options)
);
builder.Services.AddSingleton<PhotoAnalyzer.Client.Services.GraphServiceFactory>();

await builder.Build().RunAsync();