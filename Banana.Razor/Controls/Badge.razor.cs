using Microsoft.AspNetCore.Components;

namespace Banana.Razor.Controls
{
    public partial class Badge
    {
        [CascadingParameter(Name = "Text")]
        public string? Text { get; set; }
    }
}
