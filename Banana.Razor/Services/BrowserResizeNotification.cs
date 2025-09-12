using Banana.Razor.Interop;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Banana.Razor.Services
{
    public class BrowserResizeNotification : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private DOMSize _viewport;

        public DOMSize Viewport
        {
            get => _viewport;
            set
            {
                if (_viewport != value)
                {
                    _viewport = value;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = default)
            => PropertyChanged?.Invoke(this, new(propertyName));

    }
}
