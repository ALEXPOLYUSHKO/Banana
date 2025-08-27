using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Banana.Razor.Interop;

// This class provides an example of how JavaScript functionality can be wrapped
// in a .NET class for easy consumption. The associated JavaScript module is
// loaded on demand when first needed.
//
// This class can be registered as scoped DI service and then injected into Blazor
// components for use.

public class BananaJsInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    [SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "<Pending>")]
    public BananaJsInterop(IJSRuntime jsRuntime)
    {
        moduleTask = new (() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Banana.Razor/bananaJsInterop.js").AsTask());
    }

    public async ValueTask<string> Prompt(string message)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<string>("showPrompt", message);
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

    public async ValueTask<bool> RegisterBrowserViewportObserver(object dotRef)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("registerViewportChangeCallback", dotRef);
    }

    [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "<Pending>")]
    [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
