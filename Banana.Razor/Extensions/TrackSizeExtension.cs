using Banana.Razor.Enums;
using Banana.Razor.Panels;

namespace Banana.Razor.Extensions
{
    public static class TrackSizeExtension
    {
        public static TrackSizeUnit UnitFromString(this string? u)
        {
            if (!string.IsNullOrWhiteSpace(u))
            {
                if (string.Compare("px", u, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return TrackSizeUnit.Pixels;
                }

                if (string.Compare("auto", u, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return TrackSizeUnit.Auto;
                }

                if (u == "*")
                {
                    return TrackSizeUnit.Fraction;
                }
            }
            return TrackSizeUnit.None;
        }

        public static string ToCss(this TrackSize trackSize)
        {
            if (trackSize.IsFraction)
            {
                return $"{trackSize.Length}fr";
            }

            if (trackSize.IsFixed)
            {
                return $"{trackSize.Length}px";
            }

            if (trackSize.IsAuto)
            {
                return $"max-content";
            }

            return "auto";
        }
    }
}
