using System;
using System.Threading;

#if !ANDROID

using System.Device.Location;

#endif

namespace OpenWeather
{
    /// <summary>
    /// Struct to hold station information/locale
    /// </summary>
    public struct StationInfo
    {
        #region META

        /// <summary>
        /// Station ICAO code
        /// </summary>
        public string ICAO { get; set; }

        /// <summary>
        /// Station name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The elevation of the weather station in meters
        /// </summary>
        public double Elevation { get; set; }

        /// <summary>
        /// Two character coutry code
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Two character state/region code
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Station location (city)/name
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Contains the latitude and logitude of the station with in a GeoCoordinate object.
        /// </summary>
        public GeoCoordinate Location { get; set; }

        #endregion META
    }

    public abstract class Station : IDisposable
    {
        /// <summary>
        /// Station information/locale
        /// </summary>
        protected StationInfo StationInfo;

        /// <summary>
        /// Getter to publically get Station information/locale
        /// </summary>
        public StationInfo GetStationInfo => StationInfo;

        /// <summary>
        /// A generated url to lookup the past 24 hours of data from NOAA from the weatehr station.
        /// </summary>
        public string LookupUrl { get; set; }

        /// <summary>
        /// Timer for updating weather data from NOAA
        /// </summary>
        private Timer updateIntervalTimer;

        /// <summary>
        /// Holds current weather at the station
        /// </summary>
        public Weather Weather { get; protected set; }

        /// <summary>
        /// Holds the unit preferences for the data
        /// </summary>
        public Units Units { get; private set; }

        /// <summary>
        /// Event to notify when weather data has being retrieved
        /// </summary>
        public event LocationWeatherUpdatedEventHandler Updated;

        /// <summary>
        /// Default interval that updateIntervalTimer runs at
        /// </summary>
        private const int UPDATE_INTERVAL = 600;

        /// <summary>
        /// Cunstructor
        /// </summary>
        /// <param name="autoUpdate">Boolean to set whether this station should auto update data</param>
        protected Station(bool autoUpdate)
        {
            Units = new Units();

            if (autoUpdate)
                updateIntervalTimer = new Timer(_timer_Elapsed, null, TimeSpan.FromSeconds(UPDATE_INTERVAL),
                    TimeSpan.FromSeconds(UPDATE_INTERVAL));
        }

        /// <summary>
        /// Method to change the interval updateIntervalTimer runs at.
        /// </summary>
        /// <param name="minutes"></param>
        public void SetUpdateInterval(int minutes)
        {
            checked
            {
                var seconds = minutes * 60;
                updateIntervalTimer.Change(seconds, seconds);
            }
        }

        /// <summary>
        /// Called when updateIntervalTimer's interval elapses
        /// </summary>
        /// <param name="state">null</param>
        private void _timer_Elapsed(object state) => Update();

        /// <summary>
        /// Called by Update(), should be used to get data from APIs
        /// </summary>
        protected abstract void GetWeather();

        /// <summary>
        /// Updates the weather and data for the station, calls GetWeather()
        /// </summary>
        public virtual void Update()
        {
            try
            {
                GetWeather();
                Updated?.Invoke(this,
                    new LocationUpdateEventArgs(
                        $"Updated current weather conditions for {StationInfo.Location.Latitude}, {StationInfo.Location.Longitude} at {DateTime.Now.ToString("dd/MM/yy HH:mm:ss")}"));
            }
            catch (Exception ex)
            {
                throw new WeatherUpdateException(
                    $"Unable to update current weather conditions for {StationInfo.Location.Latitude}, {StationInfo.Location.Longitude} at {DateTime.Now.ToString("dd/MM/yy HH:mm:ss")}",
                    ex);
            }
        }

        /// <summary>
        /// Changes the unit preferences for the data
        /// </summary>
        /// <param name="units"></param>
        public void ChangeUnits(Units units)
        {
            Weather = new Weather(
                Units.ConvertTemperature(Units, units.TemperatureUnit, Weather.Temperature),
                Weather.Dewpoint, Units.ConvertWindSpeed(Units, units.WindSpeedUnit, Weather.WindSpeed),
                Weather.WindHeading, Units.ConvertPressure(Units, units.PressureUnit, Weather.Pressure),
                Units.ConvertDistance(Units, units.VisibilityUnit, Weather.Visibility));

            Units = units;
            Updated?.Invoke(this,
                new LocationUpdateEventArgs(
                    $"Updated units for {StationInfo.Location.Latitude}, {StationInfo.Location.Longitude} at {DateTime.Now.ToString("dd/MM/yy HH:mm:ss")}"));
        }

        /// <summary>
        /// Generates URL to lookup weather data
        /// </summary>
        /// <returns>Lookup URL</returns>
        public abstract string GenerateLookupUrl();

        #region IDisposable Support

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue) return;

            if (disposing)
                updateIntervalTimer.Dispose();
            updateIntervalTimer = null;
            disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }

    /// <summary>
    /// Delegate for LocationWeatherUpdated event
    /// </summary>
    /// <param name="source">Source object</param>
    /// <param name="e">EventArgs</param>
    public delegate void LocationWeatherUpdatedEventHandler(object source, LocationUpdateEventArgs e);

    /// <summary>
    /// EventArgs for LocationWeatherUpdated event
    /// </summary>
    public class LocationUpdateEventArgs : EventArgs
    {
        /// <summary>
        /// Contains information about the event
        /// </summary>
        private readonly string eventInfo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info">Information about the event</param>
        public LocationUpdateEventArgs(string info)
        {
            eventInfo = info;
        }

        /// <summary>
        /// Accessor for eventInfo
        /// </summary>
        /// <returns>Returns information about the event</returns>
        public string GetInfo() => eventInfo;
    }

    /// <summary>
    /// Structure to hold weather data
    /// </summary>
    public struct Weather
    {
        /// <summary>
        /// Current temperature at the weather station
        /// </summary>
        public double Temperature { get; }

        /// <summary>
        /// Current dewpoint at the weather station
        /// </summary>
        public double Dewpoint { get; }

        /// <summary>
        /// Current wind speed at the weather station
        /// </summary>
        public double WindSpeed { get; }

        /// <summary>
        /// Current wind heading at the weather station
        /// </summary>
        public int WindHeading { get; }

        /// <summary>
        /// Current pressure at the weather station
        /// </summary>
        public double Pressure { get; }

        /// <summary>
        /// Current visibility at the weather station
        /// </summary>
        public double Visibility { get; }

        /// <summary>
        /// COnstructor
        /// </summary>
        /// <param name="temperature">Temperature at the weather station</param>
        /// <param name="dewpoint">Dewpoint at the weather station</param>
        /// <param name="windspeed">Winds peed at the weather station</param>
        /// <param name="windheading">Wind heading at the weather station</param>
        /// <param name="pressure">Pressure at the weather station</param>
        /// <param name="visibility">Visibility at the weather station</param>
        public Weather(double temperature, double dewpoint, double windspeed, int windheading, double pressure,
            double visibility)
        {
            Temperature = temperature;
            Dewpoint = dewpoint;
            WindSpeed = windspeed;
            WindHeading = windheading;
            Pressure = pressure;
            Visibility = visibility;
        }
    }

    /// <summary>
    /// Exception for errors in pulling or interpreting data from NOAA
    /// </summary>
    [Serializable]
    public class WeatherUpdateException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public WeatherUpdateException() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Error message</param>
        public WeatherUpdateException(string message)
            : base(message) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="inner">Inner exception</param>
        public WeatherUpdateException(string message, Exception inner)
            : base(message, inner) { }
    }

#if ANDROID

    /// <summary>
    /// Psudo GeoCoordinate to mimic .NETs GeoCoordinate for Android
    /// </summary>
    public class GeoCoordinate
    {
        /// <summary>
        /// Latitude of coordinate
        /// </summary>
        public double Latitude { get; }

        /// <summary>
        /// Longitude of coordinate
        /// </summary>
        public double Longitude { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="latitude">Latitude of coordinate</param>
        /// <param name="longitude">Longitude of coordinate</param>
        public GeoCoordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }

#endif
}