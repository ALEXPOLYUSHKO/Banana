using Banana.Razor.Interop;
using Banana.Razor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorTabControl
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
            builder.Services.AddScoped<BrowserResizeMonitorService>();

            // notification service for the tab-control current tab changed event
            builder.Services.AddSingleton<IParameterChangeNotificationService<int>, ParameterChangeNotificationService<int>>();

            await builder.Build().RunAsync();
        }
    }
}
