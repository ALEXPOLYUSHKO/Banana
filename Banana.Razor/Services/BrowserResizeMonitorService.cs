using Banana.Razor.Enums;
using Banana.Razor.Extensions;
using Banana.Razor.Interop;

namespace Banana.Razor.Services
{
    public sealed class BrowserResizeMonitorService(
        IBananaJsInterop jsInterop,
        BrowserResizeNotification notification) : IAsyncDisposable
    {
        private readonly IBananaJsInterop _jsInterop = jsInterop;
        private readonly BrowserResizeNotification _notification = notification;

        private DOMSize browswerViewport;

        private ScreenOrientation orientation;

        private ScreenDimension dimension;

        public void OnResize(int width, int height)
        {
            var newsize = new DOMSize(width, height);

            if (browswerViewport != newsize)
            {
#if false
                Console.WriteLine($"Browswer Resized: Width={width}, Height={height}");
#endif
                browswerViewport = newsize;

                var newdimention = newsize.ToDimention();

                if (newdimention != dimension)
                {
                    dimension = newdimention;
                    // fire an event for the components monitoring size breakpoints only
                }

                var neworientation = newsize.ToOrientation();

                if (neworientation != orientation)
                {
                    orientation = neworientation;
                    // fire an event for the components monitoring orientration only
                }

                _notification.Viewport = newsize;
            }
        }

        public async Task StartMonitorAsync()
        {
            await _jsInterop.SubscribeBrowserResizeEvents(OnResize);
        }

        public async Task StopMonitorAsync()
        {
            await _jsInterop.UnsubscribeBrowserResizeEvents();
        }

        public async ValueTask DisposeAsync()
        {
            await StopMonitorAsync();
        }
    }
}
