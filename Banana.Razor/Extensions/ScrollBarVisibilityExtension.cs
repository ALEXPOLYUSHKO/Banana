using Banana.Razor.Enums;

namespace Banana.Razor.Extensions
{
    public static class ScrollBarVisibilityExtension
    {
        public static string ToCss(this ScrollBarVisibility visibility)
        {
            return visibility switch
            {
                ScrollBarVisibility.Hidden => "hidden",
                ScrollBarVisibility.Auto => "auto",
                ScrollBarVisibility.Disabled => "hidden",
                _ => "scroll",
            };
        }
    }
}
