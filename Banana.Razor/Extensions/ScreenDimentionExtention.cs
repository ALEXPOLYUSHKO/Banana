using Banana.Razor.Enums;
using Banana.Razor.Interop;

namespace Banana.Razor.Extensions
{
    public static class ScreenDimentionExtention
    {
        public static ScreenDimension ToDimention(this DOMSize size)
        {
            if (size.Width <= 600)
                return ScreenDimension.XS;
            else if (size.Width > 600 && size.Width <= 960)
                return ScreenDimension.Small;
            else if (size.Width > 960 && size.Width <= 1280)
                return ScreenDimension.Medium;
            else if (size.Width > 1280 && size.Width <= 1920)
                return ScreenDimension.Large;
            else
                return ScreenDimension.XL;
        }
    }
}
