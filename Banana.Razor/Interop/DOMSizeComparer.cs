using System.Diagnostics.CodeAnalysis;

namespace Banana.Razor.Interop
{
    public class DOMSizeComparer(int widthTolerance = 1, int heightTolerance = 1) : IEqualityComparer<DOMSize>
    {
        private readonly int _widthTolerance = widthTolerance;
        private readonly int _heightTolerance = heightTolerance;

        public bool Equals(DOMSize x, DOMSize y)
        {
            return Math.Abs(x.Width - y.Width) < _widthTolerance &&
                Math.Abs(x.Height - y.Height) < _heightTolerance;
        }

        public int GetHashCode([DisallowNull] DOMSize obj)
        {
            return HashCode.Combine(obj.Width + _widthTolerance, obj.Height + _heightTolerance);
        }
    }
}
