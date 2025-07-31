using PhotoAnalyzer.Client.Pages;
using PhotoAnalyzer.Components;
using System.Collections.Concurrent;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// In-memory store for task statuses (for demo purposes)
var taskStatuses = new ConcurrentDictionary<string, string>();

app.MapGet("/api/ServerTask/status/{taskId}", (string taskId) =>
{
    if (taskStatuses.TryGetValue(taskId, out var status))
        return Results.Ok(status);
    return Results.NotFound("Task not found");
});

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PhotoAnalyzer.Client._Imports).Assembly);

app.Run();
