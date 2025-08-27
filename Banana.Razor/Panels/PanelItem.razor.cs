using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

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

        public bool IsRendered { get; set; } = false;

        protected override void OnInitialized()
        {
            if (Parent == null)
            {
                throw new ArgumentNullException(nameof(Parent), "PanelItem must exist within a Panel");
            }

            base.OnInitialized();

            Parent.AddItem(this);
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                IsRendered = true;
                Parent?.UpdateState();
            }

            base.OnAfterRender(firstRender);
        }

        [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "<Pending>")]
        [SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>")]
        public override void Dispose()
        {
            Parent?.RemoveItem(this);
            base.Dispose();
        }
    }
}
