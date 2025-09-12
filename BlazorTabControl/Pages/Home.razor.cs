using Banana.Razor.Services;
using Microsoft.AspNetCore.Components;
using Banana.Razor.Extensions;
using Banana.Razor.Interop;

namespace BlazorTabControl.Pages
{
    public partial class Home
    {
        private async Task BannerMeasuredHandler(DOMRect boundingRect)
        {
            await Task.Delay(100);
            Console.WriteLine($"Banner measured: {boundingRect.Width} x {boundingRect.Height}");
        }
    }
}
