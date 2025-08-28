using Microsoft.JSInterop;

namespace Banana.Razor.Interop;


public sealed class BananaJsInterop(IJSRuntime jsRuntime) : IBananaJsInterop, IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Banana.Razor/bananaJsInterop.js").AsTask());

    public event Action<int, int>? _resizeCallback;

    [JSInvokable]
    public void NotifyResize(int width, int height)
    {
        _resizeCallback?.Invoke(width, height);
    }

    public async ValueTask<DOMRect?> GetElementRect(string id)
    {
        var module = await moduleTask.Value;
        var result = await module.InvokeAsync<DOMRect>("getElementRect", id);

        if (result == null)
        {
            Console.Error.WriteLine("getElementRect result is null");
        }

        return result;
    }

    public async ValueTask SubscribeBrowserResizeEvents(Action<int, int> callback)
    {
        _resizeCallback = callback;
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("addResizeListener", DotNetObjectReference.Create(this));
    }

    public async ValueTask UnsubscribeBrowserResizeEvents()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("removeResizeListener");
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
