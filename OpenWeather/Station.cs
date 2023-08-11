using System.ComponentModel;
using System.Reactive.Linq;

namespace OpenWeather
{
    public abstract class Station : IDisposable, IObservable<Weather>, INotifyPropertyChanged
    {
        private readonly IDisposable? m_handle;
        private readonly List<IObserverHandle> m_weatherSubcribers = new();

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// The backing station
        /// </summary>
        protected readonly StationInfo StationInfo;

        /// <summary>
        /// The units to take measurements in
        /// </summary>
        public Units Units { get; set; } = new Units();

        /// <summary>
        /// The current weather data
        /// </summary>
        public Weather Weather { get; set; }

        /// <summary>
        /// The generated lookup URL
        /// </summary>
        public string LookupUrl => GenerateLookupUrl();

        protected abstract string GenerateLookupUrl();
        protected abstract Task<Weather?> FetchUpdate();

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="autoUpdate">If the station should check for updates</param>
        protected Station(StationInfo stationInfo, int intervalInSeconds, bool autoUpdate = false)//600 = 30 minutes
        {
            StationInfo = stationInfo;
            if (!autoUpdate) { return; }//nothing to do

            m_handle = Observable.Interval(TimeSpan.FromSeconds(intervalInSeconds)).Subscribe(async x =>
            {
                await Update();
            });
        }

        protected void RaiseAndSetIfChanged(Weather? value)
        {
            if (value is null || Weather.Equals(value)) { return; }

            lock (m_weatherSubcribers)
            {
                Weather = value.Value;
                m_weatherSubcribers.ForEach(x => _ = x.TryUpdate(value));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Weather)));
            }
        }

        internal bool RemoveObserver(IObserverHandle handle)
        {
            lock (m_weatherSubcribers)
            {
                return m_weatherSubcribers.Remove(handle);
            }
        }

        /// <summary>
        /// Updates the weather information
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Weather?> Update()
        {
            try
            {
                var result = await FetchUpdate();

                if (result.HasValue) 
                {
                    result = result.Value.ConvertTo(Units);
                    RaiseAndSetIfChanged(result); 
                }

                return result;
            }
            catch (Exception ex) 
            {
#if DEBUG
#pragma warning disable CA2200 // Rethrow to preserve stack details
                throw ex;
#pragma warning restore CA2200 // Rethrow to preserve stack details
#endif
#pragma warning disable CS0162 // Unreachable code detected
                return null;
#pragma warning restore CS0162 // Unreachable code detected
            }
        }

        protected static double? GetElementAsDouble(string? value)
        {
            return !double.TryParse(value, out var result) ? null : result;
        }

        protected static int? GetElementAsInt32(string? value)
        {
            return !int.TryParse(value, out var result) ? null : result;
        }

        public void Dispose()
        {
            m_handle?.Dispose();
            GC.SuppressFinalize(this);
        }

        public IDisposable Subscribe(IObserver<Weather> observer)
        {
            lock (m_weatherSubcribers)
            {
                var handle = new ObserverHandle<Weather>(this, observer);
                m_weatherSubcribers.Add(handle);
                return handle;
            }
        }
    }
}
