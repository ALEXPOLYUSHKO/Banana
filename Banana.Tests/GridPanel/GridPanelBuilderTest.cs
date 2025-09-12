using Banana.Razor.Interop;
using Banana.Razor.Panels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banana.Tests.GridPanel
{
    public class GridPanelBuilderTest
    {
        [Fact]
        public void GridPanelBuilder_ContainerStyle_OneRow_OneColumn_ReturnsCorrectContainer()
        {
            var rows = new List<TrackSize>
            {
                new("150px")
            };

            var columns = new List<TrackSize>
            {
                new("*")
            };

            var testSubject = GridPanelBuilder.BuildGridContainer(rows, columns);

            Assert.NotEmpty(testSubject);


        }
    }
}
