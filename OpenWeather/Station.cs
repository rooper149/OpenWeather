namespace OpenWeather
{
    public abstract class Station : IDisposable
    {
        private readonly Timer? m_timer;
        private bool m_disposed = false;

        public event EventHandler<Weather>? WeatherUpdated;

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
        public Weather Weather { get; protected set; }

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
        protected Station(StationInfo stationInfo, bool autoUpdate = false, int intervalInSeconds = 600)//600 = 10 minutes
        {
            StationInfo = stationInfo;
            if (!autoUpdate) { return; }//nothing to do
            m_timer = new Timer(Timer_Elapsed, null, TimeSpan.FromSeconds(intervalInSeconds), TimeSpan.FromSeconds(intervalInSeconds));
        }

        private async void Timer_Elapsed(object? state) => await Update();

        /// <summary>
        /// Get this station as an IObservable
        /// that publishes on the update interval
        /// </summary>
        /// <returns></returns>
        public IObservable<Weather> ToObservable()
        {
            return new StationObservable(this);
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
                result = result.Value.ConvertTo(Units);
                WeatherUpdated?.Invoke(this, result.Value);
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
            if (!double.TryParse(value, out var result)) { return null; }
            return result;
        }

        protected static int? GetElementAsInt32(string? value)
        {
            if (!int.TryParse(value, out var result)) { return null; }
            return result;
        }

        public void Dispose()
        {
            if (m_disposed) { return; }

            m_disposed = true;
            m_timer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
