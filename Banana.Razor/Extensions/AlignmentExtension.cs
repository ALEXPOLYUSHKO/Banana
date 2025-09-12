using Banana.Razor.Enums;

namespace Banana.Razor.Extensions
{
    public static class AlignmentExtension
    {
        // aligns flex or grid item along the row axis (justify-self)
        public static string ToCss(this HorizontalContentAlignment alignment)
        {
            return alignment switch
            {
                HorizontalContentAlignment.Left => "start",
                HorizontalContentAlignment.Right => "end",
                HorizontalContentAlignment.Center => "center",
                HorizontalContentAlignment.Stretch => "stretch",
                _ => "auto",
            };
        }

        // aligns flex or grid item along the block (column) axis (align-self)

        public static string ToCss(this VerticalContentAlignment alignment)
        {
            return alignment switch
            {
                VerticalContentAlignment.Top => "start",
                VerticalContentAlignment.Bottom => "end",
                VerticalContentAlignment.Center => "center",
                VerticalContentAlignment.Stretch => "stretch",
                _ => "auto",
            };
        }
    }
}
