using Banana.Razor.Interop;

namespace Banana.Tests
{
    public class DOMSizeComparisonTest
    {
        [Fact]
        public void DOMSizeComparison_Equals_DefaultTolerance_ComparesCorrectly()
        {
            var testSubject = new DOMSizeComparer();

            var prime = new DOMSize(200, 100);

            var b = new DOMSize(200, 100);
            var c = new DOMSize(200, 101);
            var d = new DOMSize(201, 100);

            Assert.True(testSubject.Equals(prime, b));
            Assert.False(testSubject.Equals(prime, c));
            Assert.False(testSubject.Equals(prime, d));
        }

        [Fact]
        public void DOMSizeComparison_Equals_WithTolerance_ComparesCorrectly()
        {
            var testSubject = new DOMSizeComparer(10, 4);

            var prime = new DOMSize(200, 100);

            // within the tolerance
            var b = new DOMSize(209, 97);

            var c = new DOMSize(210, 96);
            var d = new DOMSize(180, 101);

            Assert.True(testSubject.Equals(prime, b));
            Assert.False(testSubject.Equals(prime, c));
            Assert.False(testSubject.Equals(prime, d));
        }

        [Fact]
        public void DOMSizeComparison_Equals_WithMaxTolerance_ComparesCorrectly()
        {
            var testSubject = new DOMSizeComparer(int.MaxValue, 5);

            var prime = new DOMSize(200, 100);

            // within the tolerance
            var b = new DOMSize(600, 96);

            var c = new DOMSize(0, 95);
            var d = new DOMSize(900, 105);

            Assert.True(testSubject.Equals(prime, b));
            Assert.False(testSubject.Equals(prime, c));
            Assert.False(testSubject.Equals(prime, d));
        }
    }
}
