using Banana.Core.Helpers;
using Banana.Razor.Enums;
using Banana.Razor.Extensions;
using Banana.Razor.Interop;
using Banana.Razor.Services;
using Microsoft.AspNetCore.Components;

namespace Banana.Razor.Controls
{
    public partial class ScrollViewer
    {
        [Inject]
        public IBananaJsInterop? BananaJsInterop { get; set; }

        [CascadingParameter]
        public BrowserResizeNotification? BrowserViewport { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter, EditorRequired]
        public StretchDirection Stretch { get; set; }

        [Parameter, EditorRequired]
        public string ViewportElementId { get; set; }

        [Parameter]
        public ScrollBarVisibility Visibility { get; set; }

        private string? scrollablePanelStyle;
        private readonly DOMSizeComparer _sizeComparer = new();
        private DOMSize _cachedBrowserViewport;

#if false
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            parameters.ToOutput("ScrollViewer");

            await base.SetParametersAsync(parameters);
        }
#endif
        protected async override Task OnParametersSetAsync()
        {
            if (BrowserViewport != null && !_sizeComparer.Equals(_cachedBrowserViewport, BrowserViewport.Viewport))
            {
                bool success = await TryGetBoundingRectAsync((rc) =>
                {
#if DEBUG
                    Console.WriteLine($"ScrollViewer bounding rect after Parameters Set: X={rc?.X}, Y={rc?.Y}, W={rc?.Width}, H={rc?.Height}");
#endif
                    if (rc != null && TryGetViewportUpdate(rc, BrowserViewport.Viewport, out int newwidth, out int newheight))
                    {
                        scrollablePanelStyle = UpdateViewerViewport(newwidth, newheight);
                        StateHasChanged();
                    }

                }).ConfigureAwait(false);

                if (success)
                {
                    _cachedBrowserViewport = BrowserViewport.Viewport;
                }
            }
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var rc = await GetBoundingRectAsync();
#if DEBUG
                Console.WriteLine($"ScrollViewer bounding rect after First Render: X={rc?.X}, Y={rc?.Y}, W={rc?.Width}, H={rc?.Height}");
#endif
                if (rc != null && TryGetViewportUpdate(rc, _cachedBrowserViewport, out int newWidth, out int newHeight))
                {
                    scrollablePanelStyle = UpdateViewerViewport(newWidth, newHeight);
                    StateHasChanged();
                }
            }
        }

        private string? UpdateViewerViewport(int newwidth, int newheight)
        {
            var style = new CssStyleBuilder();

            if (Stretch.HasFlag(StretchDirection.Vertical))
            {
                style.Add("overflow-y", Visibility.ToCss());
                style.Add("height", $"{newheight}px");
            }

            if (Stretch.HasFlag(StretchDirection.Horizontal))
            {
                style.Add("overflow-x", Visibility.ToCss());
                style.Add("width", $"{newwidth}px");
            }

            return style.ToString();
        }

        private bool TryGetViewportUpdate(DOMRect rect, DOMSize viewport, out int width, out int height)
        {
            bool isVerticalyChanged = false, isHorizontalyChanged = false;

            height = (int)Math.Round(rect.Height, 0);

            if (Stretch.HasFlag(StretchDirection.Vertical))
            {
                int extentY = (int)Math.Round(rect.Y + rect.Height, 0);
                int overflowY = extentY - viewport.Height;
                if (overflowY > 0)
                {
                    isVerticalyChanged = true;
                    height = (int)Math.Round(rect.Height, 0) - overflowY;
                }
                if (overflowY < 0)
                {
                    isVerticalyChanged = true;
                    height = viewport.Height - (int)Math.Round(rect.Y, 0);
                }
            }

            width = (int)Math.Round(rect.Width, 0);

            if (Stretch.HasFlag(StretchDirection.Horizontal))
            {
                int extentX = (int)Math.Round(rect.X + rect.Width, 0);
                int overflowX = extentX - viewport.Width;
                if (overflowX > 0)
                {
                    isHorizontalyChanged = true;
                    width = (int)Math.Round(rect.Width, 0) - overflowX;
                }
                if (overflowX < 0)
                {
                    isHorizontalyChanged = true;
                    width = viewport.Width - (int)Math.Round(rect.X, 0);
                }
            }

            return isVerticalyChanged || isHorizontalyChanged;
        }

        private async Task<DOMRect?> GetBoundingRectAsync()
        {
            ArgumentNullException.ThrowIfNull(BananaJsInterop, nameof(BananaJsInterop));
            return await BananaJsInterop.GetElementRect(ViewportElementId);
        }

        private async Task<bool> TryGetBoundingRectAsync(Action<DOMRect> callback)
        {
            ArgumentNullException.ThrowIfNull(BananaJsInterop, nameof(BananaJsInterop));

            if (!string.IsNullOrEmpty(ViewportElementId))
            {
                try
                {
                    var rect = await BananaJsInterop.GetElementRect(ViewportElementId);
                    if (rect != null)
                    {
                        callback.Invoke(rect);
                        return true;
                    }
                }
                catch
                {
                    // swallowing is intended
                }
            }
            return false;
        }
    }
}
