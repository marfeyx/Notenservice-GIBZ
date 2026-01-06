using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Notenservice.Web;

namespace Notenservice.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // 1. Deinen CustomAuthStateProvider registrieren
            builder.Services.AddScoped<CustomAuthStateProvider>();

            // 2. Blazor sagt: "AuthenticationStateProvider = CustomAuthStateProvider"
            builder.Services.AddScoped<AuthenticationStateProvider>(
                sp => sp.GetRequiredService<CustomAuthStateProvider>()
            );

            await builder.Build().RunAsync();
        }
    }
}
