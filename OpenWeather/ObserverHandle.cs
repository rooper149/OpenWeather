namespace OpenWeather
{
    internal sealed class ObserverHandle<T> : IObserverHandle, IDisposable
    {
        private readonly Station m_station;
        private readonly IObserver<T> m_observer;

        public ObserverHandle(Station station, IObserver<T> observer)
        {
            m_station = station;
            m_observer = observer;
        }

        public void Update(T value)
        {
            m_observer.OnNext(value);
        }

        public bool TryUpdate(object value)
        {
            if(value.GetType().IsAssignableTo(typeof(T)))
            {
                Update((T)value);
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            m_station.RemoveObserver(this);
        }
    }
}
