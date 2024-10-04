using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using WheresTheBeer.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("apiKeys.json", optional: true, reloadOnChange: true);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpClient();
builder.Services.AddControllers();

var app = builder.Build();

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

app.Lifetime.ApplicationStarted.Register(() =>
{
    var serverAddresses = app.Services.GetService<IHostApplicationLifetime>()?.ApplicationStarted;
    var server = app.Services.GetService<IServer>();

    if (server is KestrelServer kestrelServer)
    {
        var addresses = kestrelServer.Features.Get<IServerAddressesFeature>()?.Addresses;

        if (addresses != null)
        {
            foreach (var address in addresses)
            {
                Console.WriteLine($"Server is listening on: {address}");
            }
        }
    }
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WheresTheBeer.Client._Imports).Assembly);

app.MapControllers();

Console.WriteLine("Server has started and BaseAddress is ready.");

app.Run();

