using Banana.Razor.Enums;
using Banana.Razor.Extensions;
using Microsoft.AspNetCore.Components;

namespace Banana.Razor.Controls
{
    public partial class TabItem
    {
        [CascadingParameter]
        private TabControl? Parent { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public string? Header { get; set; }

        [Parameter]
        public bool IsDefault { get; set; } = false;

        [Parameter]
        public bool IsEnabled { get; set; } = true;

        internal bool _tabRendered = false;

        [Parameter, EditorRequired]
        public StretchDirection Stretch { get; set; } = StretchDirection.None;

        public bool IsSelected
        {
            get => Parent?.IsTabActive(this) ?? false;
        }

        public bool CanContentScroll
        {
            get => Stretch.HasFlag(StretchDirection.Horizontal) || Stretch.HasFlag(StretchDirection.Vertical);
        }
#if false
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            parameters.ToOutput("TabItem");

            await base.SetParametersAsync(parameters);
        }
#endif
        protected override Task OnInitializedAsync()
        {
            if (Parent == null)
            {
                throw new ArgumentNullException(nameof(Parent), "TabItem must exist within a TabControl");
            }

            Parent.AddItem(this);

            return base.OnInitializedAsync();
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _tabRendered = true;
            }

            return base.OnAfterRenderAsync(firstRender);
        }
    }
}
