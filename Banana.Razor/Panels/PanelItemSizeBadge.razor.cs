using Banana.Razor.Controls;
using Banana.Razor.Interop;
using Banana.Razor.Services;
using Microsoft.AspNetCore.Components;

namespace Banana.Razor.Panels
{
    public partial class PanelItemSizeBadge
    {
        [Inject]
        public IBananaJsInterop? BananaJsInterop { get; set; }

        [Parameter]
        public PanelItem? ParentReference { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [CascadingParameter]
        public BrowserResizeNotification? BrowserViewport { get; set; }

        protected async override Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            if (BrowserViewport != null)
            {
                var parentRect = await GetParentRectAsync(ParentReference?.Id);

                if (parentRect != null)
                {
                    var text = UpdateText(parentRect);
                    ChildContent = UpdateParameter(text);
                    StateHasChanged();
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                var parentRect = await GetParentRectAsync(ParentReference?.Id);

                if (parentRect != null)
                {
                    var text = UpdateText(parentRect);
                    ChildContent = UpdateParameter(text);
                    StateHasChanged();
                }
            }
        }

        private async Task<DOMRect?> GetParentRectAsync(string? id)
        {
            ArgumentNullException.ThrowIfNull(BananaJsInterop, nameof(BananaJsInterop));
            if (!string.IsNullOrEmpty(id))
            {
                return await BananaJsInterop.GetElementRect(id);
            }

            return null;
        }

        private static string UpdateText(DOMRect? rc)
        {
            if (rc != null)
            {
                return $"{double.Round(rc.Width, 0)} x {double.Round(rc.Height, 0)}";
            }
            else
            {
                return "empty";
            }
        }

        private static RenderFragment UpdateParameter(string text) => builder =>
        {
            builder.OpenComponent<CascadingValue<string?>>(0);
            builder.AddAttribute(1, "Value", text);
            builder.AddAttribute(2, "Name", "Text");
            builder.AddAttribute(3, "ChildContent", (RenderFragment)((childBuilder) =>
            {
                childBuilder.OpenElement(0, "div");
                childBuilder.AddAttribute(2, "style", "position:relative;");
                childBuilder.OpenComponent<Badge>(3);
                childBuilder.CloseComponent();
                childBuilder.CloseElement();
            }));

            builder.CloseComponent();
        };
    }
}
