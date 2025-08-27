using Banana.Razor.Interop;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorGridPanel.Layout
{
    public partial class MainLayout
    {
        [Inject]
        public IJSRuntime? JSRuntime { get; set; }

        private DotNetObjectReference<MainLayout>? _dotNetRef;

        private DOMSize browswerVieport;

        [JSInvokable]
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

        protected override void OnInitialized()
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            base.OnInitialized();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            ArgumentNullException.ThrowIfNull(JSRuntime, nameof(JSRuntime));

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("window.registerViewportChangeCallback", _dotNetRef);
            }
        }

        public void Dispose()
        {
            _dotNetRef?.Dispose();
        }
    }
}
