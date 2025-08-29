using Banana.Razor.Interop;
using Banana.Razor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorGridPanel.Layout
{
    public partial class MainLayout
    {
        [Inject]
        public required BrowserResizeMonitorService ResizeMonitorService { get; set; }

        //[Inject]
        //public required IJSRuntime JSRuntime { get; set; }

        private DOMSize browswerVieport;

//        [JSInvokable]
//        public void OnLoad(int width, int height)
//        {
//            var bv = new DOMSize(width, height);
//#if DEBUG
//            Console.WriteLine($"Browswer Loaded: Width={width}, Height={height}");
//#endif

//            if (bv != browswerVieport)
//            {
//                browswerVieport = bv;
//                StateHasChanged();
//            }
//        }

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
