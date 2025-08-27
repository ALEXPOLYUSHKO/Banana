using Banana.Razor.Enums;
using Banana.Razor.Panels;

namespace Banana.Tests
{
    public class TrackSizeTest
    {
        [Fact]
        public void TrackSize_200px_Ctor_InitializedCorrectly()
        {
            var testSubject = new TrackSize("200px");

            Assert.Equal(200.0, testSubject.Length);
            Assert.Equal(TrackSizeUnit.Pixels, testSubject.Unit);

            Assert.True(testSubject.IsFixed);
            Assert.False(testSubject.IsAuto);
            Assert.False(testSubject.IsFraction);
        }

        [Theory]
        [InlineData("400px", 400.0, TrackSizeUnit.Pixels)]
        [InlineData("0.7*", 0.7, TrackSizeUnit.Fraction)]
        [InlineData("*", 1, TrackSizeUnit.Fraction)]
        [InlineData("auto", 0, TrackSizeUnit.Auto)]
        public void TrackSize_ProperlyFormed_Ctor_InitializedCorrectly(string track, double expectedLength, TrackSizeUnit expectedUnit)
        {
            var testSubject = new TrackSize(track);

            Assert.Equal(expectedLength, testSubject.Length);
            Assert.Equal(expectedUnit, testSubject.Unit);
        }

        [Theory]
        [InlineData("100", 100.0, TrackSizeUnit.None)]
        public void TrackSize_InproperlyFormed_Ctor_InitializedCorrectly(string track, double expectedLength, TrackSizeUnit expectedUnit)
        {
            var testSubject = new TrackSize(track);

            Assert.Equal(expectedLength, testSubject.Length);
            Assert.Equal(expectedUnit, testSubject.Unit);
        }
    }
}