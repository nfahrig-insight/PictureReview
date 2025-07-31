using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.Configure<PhotoAnalyzer.Client.Services.AzureAdOptions>(options =>
    builder.Configuration.GetSection("AzureAd").Bind(options)
);
builder.Services.AddSingleton<PhotoAnalyzer.Client.Services.GraphServiceFactory>();

var host = builder.Build();
await host.RunAsync();