using Banana.Razor.Interop;

namespace Banana.Razor.Services
{
    public sealed class BrowserResizeMonitorService(IBananaJsInterop jsInterop) : IAsyncDisposable
    {
        private readonly IBananaJsInterop _jsInterop = jsInterop;

        public event Action<int, int>? OnResize;

        public async Task StartMonitorAsync()
        {
            if (OnResize != null)
            {
                await _jsInterop.SubscribeBrowserResizeEvents(OnResize);
            }
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
