using System.ComponentModel;

namespace Banana.Razor.Enums
{
    public enum ScreenDimension
    {
        [Description("less than 600px")]
        XS,

        [Description("between 600px and 960px")]
        Small,

        [Description("between 960px and 1280px")]
        Medium,

        [Description("between 1280px and 1920px")]
        Large,

        [Description("more than 1920px")]
        XL
    }
}
