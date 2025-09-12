using Banana.Razor.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorGridPanel.Layout
{
    public partial class MainLayout : IAsyncDisposable
    {
        [Inject]
        public required BrowserResizeMonitorService ResizeMonitorService { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await ResizeMonitorService.StartMonitorAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await ResizeMonitorService.StopMonitorAsync();
            GC.SuppressFinalize(this);
        }
    }
}
