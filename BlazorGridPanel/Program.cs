using Banana.Razor.Extensions;
using Banana.Razor.Interop;
using Banana.Razor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorGridPanel
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IBananaJsInterop, BananaJsInterop>();

            var notifications = new BrowserResizeNotification();
            builder.Services.AddScoped(sp => notifications);
            builder.Services.AddNotifyingCascadingValue(notifications);
            builder.Services.AddScoped<BrowserResizeMonitorService>();

            await builder.Build().RunAsync();
        }
    }
}
