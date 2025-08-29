using Banana.Razor.Enums;
using Blazr.BaseComponents;
using Microsoft.AspNetCore.Components;

namespace Banana.Razor.Panels
{
    public class ColumnDefinition : BlazrComponentBase, IHaveTrackSize
    {
        [Parameter, EditorRequired]
        public string Width { get; set; }

        public TrackSize TrackSize { get; private set; } = TrackSize.Default;

        [CascadingParameter]
        private ColumnDefinitions? Parent { get; set; }

        protected override void OnParametersSet()
        {
            ArgumentNullException.ThrowIfNull(Parent, nameof(Parent));

            TrackSize = new TrackSize(Width, StretchDirection.Horizontal);

            Parent.AddColumn(this);
        }
    }
}
