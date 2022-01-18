using System.Collections.Concurrent;

namespace OpenWeather
{
    internal sealed class StationObservable : IObservable<Weather>
    {
        public readonly ConcurrentDictionary<IDisposable, IObserver<Weather>> m_observers = new();

        public StationObservable(Station station)
        {
            station.WeatherUpdated += Station_WeatherUpdated;
        }

        private void Station_WeatherUpdated(object? sender, Weather e)
        {
            foreach (var observer in m_observers) { observer.Value.OnNext(e); }
        }

        public IDisposable Subscribe(IObserver<Weather> observer)
        {
            var disposable = new StationObserver(this);
            m_observers.TryAdd(disposable, observer);
            return disposable;
        }

        public void RemoveObserver(IDisposable disposable)
        {
            m_observers.TryRemove(disposable, out var observer);
            observer?.OnCompleted();
        }
    }
}
