using Banana.Razor.Enums;
using Banana.Razor.Interop;

namespace Banana.Razor.Extensions
{
    public static class ScreenOrientationExtension
    {
        public static ScreenOrientation ToOrientation(this DOMSize size)
        {
            return (size.Width > size.Height) ? ScreenOrientation.Landscape : ScreenOrientation.Portrait;
        }
    }
}
