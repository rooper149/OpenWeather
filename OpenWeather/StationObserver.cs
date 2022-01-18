namespace OpenWeather
{
    internal sealed class StationObserver : IDisposable
    {
        private bool m_disposed = false;
        private readonly StationObservable m_observable;

        public StationObserver(StationObservable observable)
        {
            m_observable = observable;
        }

        public void Dispose()
        {
            if (m_disposed) { return; }

            m_disposed = true;
            m_observable.RemoveObserver(this);
            GC.SuppressFinalize(this);
        }
    }
}
