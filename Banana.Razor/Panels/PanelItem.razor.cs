using Banana.Razor.Enums;
using Banana.Razor.Extensions;
using Banana.Razor.Interop;
using Microsoft.AspNetCore.Components;

namespace Banana.Razor.Panels
{
    public partial class PanelItem
    {
        [CascadingParameter]
        private GridPanel? Parent { get; set; }

        [Parameter, EditorRequired]
        public int GridRow { get; set; }

        [Parameter, EditorRequired]
        public int GridColumn { get; set; }

        [Parameter]
        public int GridRowSpan { get; set; } = 1;

        [Parameter]
        public int GridColumnSpan { get; set; } = 1;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public string? PanelStyle { get; set; }

        public bool IsRendered { get; set; } = false;

        public string Id = Guid.NewGuid().ToString()[..4];

        public DOMRect? BoundingRect { get; set; } = null;

#if true
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            parameters.ToOutput("PanelItem");

            await base.SetParametersAsync(parameters);
        }
#endif
        protected override void OnInitialized()
        {
            if (Parent == null)
            {
                throw new ArgumentNullException(nameof(Parent), "PanelItem must exist within a Panel");
            }

            base.OnInitialized();

            Parent.AddItem(this);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                IsRendered = true;

                Parent?.UpdateState();
            }
        }
    }
}
