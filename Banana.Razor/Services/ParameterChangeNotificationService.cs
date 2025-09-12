namespace Banana.Razor.Services
{
    public class ParameterChangeNotificationService<T> : IParameterChangeNotificationService<T>
    {
        public event Action<T>? OnNotify;

        public void Notify(T parameter)
        {
            OnNotify?.Invoke(parameter);
        }
    }
}
