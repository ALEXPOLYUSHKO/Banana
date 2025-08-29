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
            var newsize = new DOMSize(width, height);

            if (browswerVieport != newsize)
            {
#if DEBUG
                Console.WriteLine($"Browswer Resized: Width={width}, Height={height}");
#endif
                browswerVieport = newsize;
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
