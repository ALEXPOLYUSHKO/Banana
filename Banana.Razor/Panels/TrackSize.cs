using Banana.Razor.Enums;
using Banana.Razor.Extensions;

namespace Banana.Razor.Panels
{
    public record class TrackSize
    {
        public TrackSize(string track)
        {
            var result = TryParse(track, out var value, out var unit);

            // processing special cases like single '*' which means value is 100% or 1.0 

            Length = result ? value : track == "*" ? 1.0 : 0.0;

            // in case of auto or star result is false, but units still parsed correctly
            Unit = unit.UnitFromString();
        }

        public static TrackSize Default => new("*");

        public bool IsFixed => Unit == TrackSizeUnit.Pixels;

        public bool IsFraction => Unit == TrackSizeUnit.Fraction;

        public bool IsAuto => Unit == TrackSizeUnit.Auto;

        public double Length;

        public TrackSizeUnit Unit;

        public override string ToString()
        {
            return this.ToCss();
        }

        private static bool TryParse(string track, out double value, out string? unit)
        {
            value = double.NaN;
            unit = null;

            // find index of the last digit in the sequence
            // to split the 'track' into value and units
            int indx = 0;
            for (int i = track.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(track[i]))
                {
                    indx = i + 1;
                    break;
                }
            }

            var digits = track[..indx];
            if (double.TryParse(digits, out double result))
            {
                value = result;
            }

            if (indx < track.Length)
            {
                unit = track[indx..];
            }

            // only values greater than zero

            return double.IsNormal(value) && value > 0.0;
        }
    }
}
