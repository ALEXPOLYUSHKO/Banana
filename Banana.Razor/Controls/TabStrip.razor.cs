using Microsoft.AspNetCore.Components;

namespace Banana.Razor.Controls
{
    public partial class TabStrip
    {
        [CascadingParameter]
        private TabControl? Parent { get; set; }
    }
}
