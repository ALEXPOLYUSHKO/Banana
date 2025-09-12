namespace Banana.Razor.Services
{
    public interface IParameterChangeNotificationService<T>
    {
        event Action<T>? OnNotify;

        void Notify(T parameter);
    }
}
