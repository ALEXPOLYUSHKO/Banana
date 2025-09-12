using Banana.Razor.Extensions;
using Microsoft.AspNetCore.Components;

namespace Banana.Razor.Controls
{
    public partial class TabControl
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public RenderFragment<TabItem>? ItemTemplate { get; set; }

        [Parameter]
        public int SelectedTabIndex { get; set; }

        [Parameter]
        public EventCallback<int> SelectedTabIndexChanged { get; set; }

        public TabItem? ActiveItem { get; set; }
        public readonly List<TabItem> Items = [];

        public string ViewportId { get; init; } = Guid.NewGuid().ToString("N");
#if false
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            parameters.ToOutput("TabControl");

            await base.SetParametersAsync(parameters);
        }
#endif
        protected override void OnParametersSet()
        {
            var tab = Items.ElementAtOrDefault(SelectedTabIndex);

            if (tab != null && !tab.IsSelected)
            {
                ActivateItem(tab);
            }

            base.OnParametersSet();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                // at this point all items should be added into collection
                // activate Default tab
                var defaultItem = Items.FirstOrDefault(x => x.IsDefault);

                if (defaultItem != null)
                {
                    var indx = ActivateItem(defaultItem);
                    UpdateSelectedTabIndex(indx);
                }
            }
            base.OnAfterRender(firstRender);
        }

        internal void AddItem(TabItem item)
        {
            Items.Add(item);
        }

        internal void RemoveItem(TabItem item)
        {
            var result = Items?.Remove(item);
#if false
            Console.WriteLine($"PanelItem removed: {result}");
#endif
        }

        internal int ActivateItem(TabItem item)
        {
            if (ActiveItem != item)
            {
                ActiveItem = item;

                StateHasChanged();
                return Items.IndexOf(item);
            }

            return -1;
        }

        internal void UpdateSelectedTabIndex(int indxSelected)
        {
            if (indxSelected >= 0)
            {
                SelectedTabIndex = indxSelected;
                SelectedTabIndexChanged.InvokeAsync(indxSelected);
            }
        }

        internal bool IsTabActive(TabItem item)
        {
            return ActiveItem == item;
        }
    }
}
