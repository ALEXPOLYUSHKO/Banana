using Banana.Core.Helpers;
using Banana.Razor.Enums;
using Banana.Razor.Extensions;
using Banana.Razor.Interop;
using Microsoft.AspNetCore.Components;
using System.Drawing;

namespace BlazorGridPanel.Components
{
    public partial class ScrollViewer
    {
        [Inject]
        public IBananaJsInterop? BananaJsInterop { get; set; }

        [CascadingParameter(Name = "CurrentDOMSize")]
        public DOMSize CurrentDOMSize { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter, EditorRequired]
        public StretchDirection Stretch { get; set; }

        [Parameter, EditorRequired]
        public string Id { get; set; }

        [Parameter]
        public ScrollBarVisibility Visibility { get; set; }

        private string? scrollablePanelStyle;
        private readonly DOMSizeComparer _sizeComparer = new();
        private DOMSize _cachedBrowserViewport;

        protected async override Task OnParametersSetAsync()
        {
            if (!_sizeComparer.Equals(_cachedBrowserViewport, CurrentDOMSize))
            {
                _cachedBrowserViewport = CurrentDOMSize;
                var rc = await GetBoundingRectAsync();
#if DEBUG
                Console.WriteLine($"Scroll viewer bounding rect after browswer resized: X={rc?.X}, Y={rc?.Y}, W={rc?.Width}, H={rc?.Height}");
#endif
                if (rc != null && TryGetViewportUpdate(rc, out int newWidth, out int newHeight))
                {
                    scrollablePanelStyle = UpdateViewerViewport(newWidth, newHeight);
                    StateHasChanged();
                }
            }
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var rc = await GetBoundingRectAsync();
                if (rc != null && TryGetViewportUpdate(rc, out int newWidth, out int newHeight))
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

            return style.ToString();
        }

        private bool TryGetViewportUpdate(DOMRect rect, out int width, out int height)
        {
            bool isVerticalyChanged = false, isHorizontalyChanged = false;

            height = (int)Math.Round(rect.Height, 0);

            if (Stretch.HasFlag(StretchDirection.Vertical))
            {
                int extentY = (int)Math.Round(rect.Y + rect.Height, 0);
                int overflowY = extentY - _cachedBrowserViewport.Height;
                if (overflowY > 0)
                {
                    isVerticalyChanged = true;
                    height = (int)Math.Round(rect.Height, 0) - overflowY;
                }
            }

            width = (int)Math.Round(rect.Width, 0);

            if (Stretch.HasFlag(StretchDirection.Horizontal))
            {
                int extentX = (int)Math.Round(rect.X + rect.Width, 0);
                int overflowX = extentX - _cachedBrowserViewport.Width;
                if (overflowX > 0)
                {
                    isHorizontalyChanged = true;
                    width = (int)Math.Round(rect.Width, 0) - overflowX;
                }
            }

            return isVerticalyChanged || isHorizontalyChanged;
        }

        private async Task<DOMRect?> GetBoundingRectAsync()
        {
            ArgumentNullException.ThrowIfNull(BananaJsInterop, nameof(BananaJsInterop));
            return await BananaJsInterop.GetElementRect(Id);
        }
    }
}
