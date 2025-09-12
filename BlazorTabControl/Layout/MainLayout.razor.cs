using Banana.Razor.Enums;
using Banana.Razor.Interop;
using Banana.Razor.Services;
using Banana.Razor.Extensions;
using Microsoft.AspNetCore.Components;

namespace BlazorTabControl.Layout
{
    public partial class MainLayout : IAsyncDisposable
    {
        [Inject]
        public required BrowserResizeMonitorService ResizeMonitorService { get; set; }

        //private DOMSize browswerViewport;

//        private ScreenOrientation orientation;

//        private ScreenDimension dimension;

//        public void OnResize(int width, int height)
//        {
//            var newsize = new DOMSize(width, height);

//            if (browswerViewport != newsize)
//            {
//#if DEBUG
//                Console.WriteLine($"Browswer Resized: Width={width}, Height={height}");
//#endif
//                browswerViewport = newsize;

//                var newdimention = newsize.ToDimention();

//                if (newdimention != dimension)
//                {
//                    dimension = newdimention;
//                    // fire an event for the components monitoring size breakpoints only
//                }

//                var neworientation = newsize.ToOrientation();

//                if (neworientation != orientation)
//                {
//                    orientation = neworientation;
//                    // fire an event for the components monitoring orientration only
//                }

//                StateHasChanged();
//            }
//        }

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
