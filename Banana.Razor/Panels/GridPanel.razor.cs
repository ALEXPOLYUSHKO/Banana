using Microsoft.AspNetCore.Components;

namespace Banana.Razor.Panels
{
    public partial class GridPanel
    {
        private string? ContainerStyle;

        private readonly List<PanelItem> Children = [];

        private RenderFragment? _cachedPanel;
        private ColumnDefinitions? _columnDefinitions;
        private RowDefinitions? _rowDefinitions;
        private bool _allChildrenRendered = false;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        protected override bool ShouldRender()
        {
            base.ShouldRender();
            return _allChildrenRendered;
        }

        protected override void OnAfterRender(bool firstRender)
        {
#if false
            var renderType = firstRender ? "First" : "Another";
            Console.WriteLine($"{renderType} OnAfterRender::Items in the panel: {Children.Count}");
#endif
            if (firstRender)
            {
                // on the first render we create container
                ContainerStyle = GridPanelBuilder.GetContainerStyle(_rowDefinitions?.Rows ?? [], _columnDefinitions?.Columns ?? []);

                _cachedPanel = GridPanelBuilder.BuildGridPanels(Children);

                ChildContent = _cachedPanel;
                StateHasChanged();
            }

            ChildContent = _cachedPanel;

            base.OnAfterRender(firstRender);
        }

        internal void AddRowDefinitions(RowDefinitions definitions)
        {
            _rowDefinitions = definitions;
        }

        internal void AddColumnDefinitions(ColumnDefinitions definitions)
        {
            _columnDefinitions = definitions;
        }

        internal void AddItem(PanelItem item)
        {
            Children.Add(item);
        }

        internal void RemoveItem(PanelItem panelItem)
        {
            var result = Children?.Remove(panelItem);
#if false
            Console.WriteLine($"PanelItem removed: {result}");
#endif
        }

        internal void UpdateState()
        {
            _allChildrenRendered = Children.All(x => x.IsRendered);

            if (_allChildrenRendered)
            {
                StateHasChanged();
            }
        }
    }
}
