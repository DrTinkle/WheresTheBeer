using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Initialize the HttpClient
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Build the client host
var host = builder.Build();

// Ensure that BaseAddress is fully initialized
await EnsureBaseAddressIsSetAsync(host.Services.GetRequiredService<HttpClient>());

await host.RunAsync();

async Task EnsureBaseAddressIsSetAsync(HttpClient httpClient)
{
    int retries = 10; // Retry more times if needed
    while (retries > 0 && httpClient.BaseAddress == null)
    {
        Console.WriteLine("BaseAddress is null, retrying...");
        retries--;
        await Task.Delay(500); // Adjust the delay to your needs
    }

    if (httpClient.BaseAddress == null)
    {
        throw new InvalidOperationException("HttpClient BaseAddress is still null after multiple retries.");
    }

    Console.WriteLine($"BaseAddress is set to: {httpClient.BaseAddress}");
}
