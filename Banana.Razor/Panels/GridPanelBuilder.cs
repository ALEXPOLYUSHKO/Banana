using Banana.Core.Helpers;
using Banana.Razor.Extensions;
using Microsoft.AspNetCore.Components;

namespace Banana.Razor.Panels
{
    public static class GridPanelBuilder
    {
        private const string DIV = "div";

        /* CONTAINER */

        public static string BuildGridContainer(IEnumerable<TrackSize> rows, IEnumerable<TrackSize> columns)
        {
            return GetContainerStyle(rows, columns);
        }

        public static string GetContainerStyle(IEnumerable<TrackSize> rows, IEnumerable<TrackSize> columns)
        {
            return new CssStyleBuilder()
                .Add("display", "grid")
                .Add("grid-template-rows", GetTemplatedTrackSizes(rows))
                .Add("grid-template-columns", GetTemplatedTrackSizes(columns))
                .Add("height", ComputeTrackSizesStretch(rows))
                .Add("width", ComputeTrackSizesStretch(columns)).ToString();
        }

        /* PANELS */

        public static RenderFragment BuildGridPanels(IEnumerable<PanelItem> items) => builder =>
        {
            foreach (var item in items)
            {
                builder.OpenElement(0, DIV);

                // set style for the item
                builder.AddAttribute(1, "style", GetPanelItemStyle(item));
                builder.AddContent(2, item.ChildContent);
                builder.CloseElement();
            }
        };

        private static string GetPanelItemStyle(PanelItem item)
        {
            int rowStart = item.GridRow;
            int rowEnd = item.GridRow + item.GridRowSpan;
            int columnStart = item.GridColumn;
            int columnEnd = item.GridColumn + item.GridColumnSpan;

            var style = new CssStyleBuilder()
                .Add("grid-row-start", rowStart.ToString())
                .Add("grid-row-end", rowEnd.ToString())
                .Add("grid-column-start", columnStart.ToString())
                .Add("grid-column-end", columnEnd.ToString());

            // stretching content to take all available space
            style.Add("justify-self", "stretch");
            style.Add("align-self", "stretch");

            return style.ToString();
        }

        private static string GetTemplatedTrackSizes(IEnumerable<TrackSize> trackSizes)
        {
            List<string> templates = [];

            foreach (var track in trackSizes)
            {
                templates.Add(track.ToCss());
            }

            return templates.Count > 0 ? string.Join(" ", templates) : "auto";
        }

        // returns either 'auto' or '100%'
        // if the last track is fractional, and the length is 1.0, it must occupy the remaining
        // browser space, it should not be 100% - it has to be a max-content
        private static string ComputeTrackSizesStretch(IEnumerable<TrackSize> trackSizes)
        {
            // check last track to see if we have to limit vertical stretching to max-content
            var last = trackSizes.LastOrDefault();

            if (last != null && last.IsFraction && last.Length == 1.0 && 
                last.StretchDirection.HasFlag(Enums.StretchDirection.Vertical))
            {
                return "max-content";
            }

            var stretch = trackSizes.Any(x => x.IsFraction);

            return stretch ? "100%" : "auto";
        }
    }
}
