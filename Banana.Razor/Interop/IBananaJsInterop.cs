using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banana.Razor.Interop
{
    public interface IBananaJsInterop
    {
        ValueTask<DOMRect?> GetElementRect(string id);

        ValueTask SubscribeBrowserResizeEvents(Action<int, int> callback);

        ValueTask UnsubscribeBrowserResizeEvents();
    }
}
