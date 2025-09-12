using Banana.Core.Helpers;
using Microsoft.AspNetCore.Components;

namespace Banana.Razor.Controls
{
    public partial class TabItemPresenter
    {
        [CascadingParameter]
        public TabItem? TabItem { get; set; }

        [Parameter]
        public RenderFragment<TabItem>? Template { get; set; }

        private string GetPresenterStyle()
        {
            return new CssStyleBuilder()
                .Add("opacity", TabItem?.IsEnabled ?? false ? "1" : "0.5")              
                .ToString();
        }
    }
}
