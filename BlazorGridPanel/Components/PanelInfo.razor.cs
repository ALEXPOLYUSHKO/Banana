using Banana.Razor.Interop;
using Banana.Razor.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorGridPanel.Components
{
    public partial class PanelInfo
    {
        [Inject]
        public IBananaJsInterop? BananaJsInterop { get; set; }

        [CascadingParameter]
        public BrowserResizeNotification? BrowserViewport { get; set; }

        [Parameter]
        public string? Title { get; set; }

        [Parameter, EditorRequired]
        public string ParentId { get; set; }

        private string? _panelInfo;
        private readonly DOMSizeComparer _sizeComparer = new();
        private DOMSize _cachedBrowserViewport;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var parentRect = await GetParentRectAsync();
                UpdateInfo(parentRect);
                StateHasChanged();
            }
        }

        protected async override Task OnParametersSetAsync()
        {
            if (BrowserViewport != null && !_sizeComparer.Equals(_cachedBrowserViewport, BrowserViewport.Viewport))
            {
                _cachedBrowserViewport = BrowserViewport.Viewport;
                var parentRect = await GetParentRectAsync();
                UpdateInfo(parentRect);
                StateHasChanged();
            }

            await base.OnParametersSetAsync();
        }

        private async Task<DOMRect?> GetParentRectAsync()
        {
            ArgumentNullException.ThrowIfNull(BananaJsInterop, nameof(BananaJsInterop));
            return await BananaJsInterop.GetElementRect(ParentId);
        }

        private void UpdateInfo(DOMRect? rc)
        {
            if (rc != null)
            {
                _panelInfo = $"{double.Round(rc.Width, 0)} x {double.Round(rc.Height, 0)}";
            }
            else
            {
                _panelInfo = "Could not determine panel's bounding rectangle";
            }
        }
    }
}
