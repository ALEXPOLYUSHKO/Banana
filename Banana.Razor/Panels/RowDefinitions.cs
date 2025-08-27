using Blazr.BaseComponents;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Banana.Razor.Panels
{
    public class RowDefinitions : BlazrComponentBase
    {
        private readonly Dictionary<Guid, TrackSize> _rows = [];

        [CascadingParameter]
        private GridPanel? Parent { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        public IEnumerable<TrackSize> Rows => _rows.Values;

        protected override void OnInitialized()
        {
            ArgumentNullException.ThrowIfNull(Parent, nameof(GridPanel));
            // once initialized - propogate itself to the parent
            Parent.AddRowDefinitions(this);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<RowDefinitions>>(0);
            builder.AddAttribute(1, "Value", this);
            builder.AddAttribute(2, "ChildContent", ChildContent);
            builder.CloseComponent();
        }

        internal void AddRow(RowDefinition row)
        {
            if (!_rows.ContainsKey(row.ComponentUid))
            {
                _rows.Add(row.ComponentUid, row.TrackSize);
            }
        }
    }
}
