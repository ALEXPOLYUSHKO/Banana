using Banana.Razor.Interop;
using Banana.Razor.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorGridPanel.Layout
{
    public partial class MainLayout
    {
        [Inject]
        public required BrowserResizeMonitorService ResizeMonitorService { get; set; }

        private DOMSize browswerVieport;

        public void OnResize(int width, int height)
        {
            var newSize = new DOMSize(width, height);

            if (browswerVieport != newSize)
            {
                Console.WriteLine($"Browswer resized: Width={width}, Height={height}");
                browswerVieport = newSize;
                StateHasChanged();
            }
        }

        protected async override Task OnInitializedAsync()
        {
            ResizeMonitorService.OnResize += OnResize;
            await ResizeMonitorService.StartMonitorAsync();
        }

        public async ValueTask DisposeAsync()
        {
            ResizeMonitorService.OnResize -= OnResize;
            await ResizeMonitorService.StopMonitorAsync();
        }
    }
}
