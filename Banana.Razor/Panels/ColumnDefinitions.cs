using Blazr.BaseComponents;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Banana.Razor.Panels
{
    public class ColumnDefinitions : BlazrComponentBase
    {
        private readonly Dictionary<Guid, TrackSize> _columns = [];

        [CascadingParameter]
        private GridPanel? Parent { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        public IEnumerable<TrackSize> Columns => _columns.Values;

        protected override void OnInitialized()
        {
            ArgumentNullException.ThrowIfNull(Parent, nameof(GridPanel));
            // once initialized - propogate itself to the parent
            Parent.AddColumnDefinitions(this);
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<ColumnDefinitions>>(0);
            builder.AddAttribute(1, "Value", this);
            builder.AddAttribute(2, "ChildContent", ChildContent);
            builder.CloseComponent();
        }

        internal void AddColumn(ColumnDefinition column)
        {
            if (!_columns.ContainsKey(column.ComponentUid))
            {
                _columns.Add(column.ComponentUid, column.TrackSize);
            }
        }
    }
}
