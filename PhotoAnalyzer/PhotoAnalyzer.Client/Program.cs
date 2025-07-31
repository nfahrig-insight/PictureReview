using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.Configure<PhotoAnalyzer.Client.Services.AzureAdOptions>(options =>
    builder.Configuration.GetSection("AzureAd").Bind(options)
);
builder.Services.AddSingleton<PhotoAnalyzer.Client.Services.GraphServiceFactory>();

var host = builder.Build();
await host.RunAsync();

// Main method to run after the app has started
await MainAsync(host);

static async Task MainAsync(WebAssemblyHost host)
{
    var factory = host.Services.GetRequiredService<PhotoAnalyzer.Client.Services.GraphServiceFactory>();

    var client = factory.CreateGraphServiceClient();

    Console.WriteLine("GraphServiceClient created successfully.");
    // Example: resolve a service and do something
    // var graphFactory = host.Services.GetRequiredService<PhotoAnalyzer.Client.Services.GraphServiceFactory>();
    // var client = graphFactory.CreateGraphServiceClient();
    // await Task.CompletedTask;
}
