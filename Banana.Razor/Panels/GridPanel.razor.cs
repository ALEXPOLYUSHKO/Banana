using Banana.Razor.Extensions;
using Microsoft.AspNetCore.Components;

namespace Banana.Razor.Panels
{
    public partial class GridPanel
    {
        private readonly List<PanelItem> Children = [];

        private RenderFragment? _cachedContent;
        private ColumnDefinitions? _columnDefinitions;
        private RowDefinitions? _rowDefinitions;
        private bool _allChildrenRendered = false;
        private string? _containerStyle;

        [Parameter]
        public string ContainerStyle { get; set; } = "width:auto;height:auto";

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        protected override bool ShouldRender()
        {
            return _allChildrenRendered;
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            parameters.ToOutput("GridPanel");

            await base.SetParametersAsync(parameters);
        }

        protected override void OnAfterRender(bool firstRender)
        {
#if true
            var renderType = firstRender ? "First" : "Another";
            Console.WriteLine($"{renderType} OnAfterRender::Items in the panel: {Children.Count}");
#endif
            if (firstRender)
            {
                // on the first render we create container
                _containerStyle = GridPanelBuilder.GetContainerStyle(_rowDefinitions?.Rows ?? [], _columnDefinitions?.Columns ?? [], ContainerStyle);

                _cachedContent = GridPanelBuilder.BuildGridPanels(Children);

                ChildContent = _cachedContent;
                StateHasChanged();
            }

            ChildContent = _cachedContent;

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
#if true
            Console.WriteLine($"PanelItem ID={item.Id} added. Children={Children.Count}");
#endif
        }

        internal void RemoveItem(PanelItem panelItem)
        {
            var result = Children.Remove(panelItem);
#if true
            Console.WriteLine($"PanelItem ID={panelItem.Id} removed:{result}. Children={Children.Count}");
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
