using Blazr.BaseComponents;
using Microsoft.AspNetCore.Components;

namespace Banana.Razor.Panels
{
    public class RowDefinition : BlazrComponentBase, IHaveTrackSize
    {
        [Parameter, EditorRequired]
        public string Height { get; set; }

        public TrackSize TrackSize { get; private set; } = TrackSize.Default;

        [CascadingParameter]
        private RowDefinitions? Parent { get; set; }

        protected override void OnParametersSet()
        {
            ArgumentNullException.ThrowIfNull(Parent, nameof(Parent));

            TrackSize = new TrackSize(Height);

            Parent.AddRow(this);
        }
    }
}
