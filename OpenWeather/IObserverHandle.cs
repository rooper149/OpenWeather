namespace OpenWeather
{
    internal interface IObserverHandle
    {
        bool TryUpdate(object value);
    }

}
