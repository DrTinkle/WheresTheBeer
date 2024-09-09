using WheresTheBeer.Components;

var builder = WebApplication.CreateBuilder(args);

// Load the apiKeys.json configuration
builder.Configuration.AddJsonFile("apiKeys.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Register HttpClient for both server and client-side rendering
builder.Services.AddHttpClient();

// Add support for controllers (for API routing)
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WheresTheBeer.Client._Imports).Assembly);

// Map API controllers
app.MapControllers();

app.Run();
