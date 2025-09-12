using Banana.Razor.Services;
using Microsoft.AspNetCore.Components;
using Banana.Razor.Extensions;

namespace BlazorTabControl.Pages
{
    public partial class TabbedContent : IDisposable
    {
        [Inject]
        public required IParameterChangeNotificationService<int> TabChangeNotificationService { get; set; }

        private int CurrentTabIndex { get; set; }

        // called when activating tab-item programatically 
        private void TabChangeNotificationHandler(int tabIndex)
        {
            CurrentTabIndex = tabIndex;
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            parameters.ToOutput("TabbedContent");

            await base.SetParametersAsync(parameters);
        }

        protected override Task OnInitializedAsync()
        {
            // subscribe on tab notification event
            TabChangeNotificationService.OnNotify += TabChangeNotificationHandler;
            return base.OnInitializedAsync();
        }

        public void Dispose()
        {
            TabChangeNotificationService.OnNotify -= TabChangeNotificationHandler;
            GC.SuppressFinalize(this);
        }
    }
}
