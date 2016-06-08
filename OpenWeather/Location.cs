using System;
using System.Net;
using System.Threading;
using System.Xml.Linq;
using UnitsNet;
using UnitsNet.Units;

namespace OpenWeather
{
    /// <summary>
    /// Delegate for LocationWeatherUpdated event
    /// </summary>
    /// <param name="source">Source object</param>
    /// <param name="e">EventArgs</param>
    public delegate void LocationWeatherUpdated(object source, LocationUpdateEventArgs e);

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
    /// Class to handle the retrival of METAR data from a weather station
    /// </summary>
    public class LocationWeather
    {
        /// <summary>
        /// Timer for updating weather data from NOAA
        /// </summary>
        private readonly Timer updateIntervalTimer;

        /// <summary>
        /// Default interval that updateIntervalTimer runs at
        /// </summary>
        private const int UPDATE_INTERVAL = 600;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="station">METAR compliant weather station</param>
        /// <param name="units">Units of measurement for data retrieved</param>
        /// <param name="updateNow">Updates the weather data during object creation</param>
        public LocationWeather(Station station, Units units, bool updateNow = true)
        {
            Station = station;
            Units = units;

            if(updateNow)
                Update();

            updateIntervalTimer = new Timer(_timer_Elapsed, null, TimeSpan.FromSeconds(UPDATE_INTERVAL),
                TimeSpan.FromSeconds(UPDATE_INTERVAL));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiprovider">URL of site hosting an OpenICAO api excluding the /Search/?lat=double&lngt=double part.
        /// See https://github.com/rooper149/OpenICAO for information about OpenICAO</param>
        /// <param name="latitude">Latitude of the location requesting METAR data</param>
        /// <param name="longitude">Longitude of the location requesting METAR data</param>
        /// <param name="units">Units of measurement for data retrieved</param>
        /// <param name="updateNow">Updates the weather data during object creation</param>
        public LocationWeather(string apiprovider, double latitude, double longitude, Units units, bool updateNow = true)
        {
            var icao = new WebClient().DownloadString($"{apiprovider}Search/?lat={latitude}&lngt={longitude}");
            Station = new Station(icao, latitude, longitude);
            Units = units;

            if(updateNow)
                Update();

            updateIntervalTimer = new Timer(_timer_Elapsed, null, TimeSpan.FromSeconds(UPDATE_INTERVAL),
                TimeSpan.FromSeconds(UPDATE_INTERVAL));
        }

        /// <summary>
        /// Method to change the interval updateIntervalTimer runs at.
        /// </summary>
        /// <param name="minutes"></param>
        public void SetUpdateInterval(int minutes)
        {
            var seconds = minutes * 60;
            updateIntervalTimer.Change(seconds, seconds);
        }

        /// <summary>
        /// METAR compliant weather station
        /// </summary>
        public Station Station { get; }

        /// <summary>
        /// Holds current weather at the station
        /// </summary>
        public Weather Weather { get; private set; }

        /// <summary>
        /// Holds the unit preferences for the data
        /// </summary>
        public Units Units { get; private set; }

        /// <summary>
        /// Event to notify when weather data has being retrieved
        /// </summary>
        public event LocationWeatherUpdated Updated;

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
                Units.ConvertDistance(Units, units.VisibilityUnit, Weather.Visibility), Weather.SkyConditions);

            Units = units;
            Updated?.Invoke(this,
                new LocationUpdateEventArgs(
                    $"Updated units for {Station.ICAO} at {DateTime.Now.ToString("dd/MM/yy HH:mm:ss")}"));
        }

        /// <summary>
        /// Called when updateIntervalTimer's interval elapses
        /// </summary>
        /// <param name="state">null</param>
        private void _timer_Elapsed(object state) => Update();

        /// <summary>
        /// Gets and parses METAR data from NOAA
        /// </summary>
        public void Update()
        {
            try
            {
                var wc = new WebClient();
                var doc = XDocument.Parse(wc.DownloadString(Station.LookupUrl));
                var temp = Convert.ToDouble(XmlTools.GetElementContent("temp_c", doc, "data", "METAR"));
                var windSpeed = Convert.ToDouble(XmlTools.GetElementContent("wind_speed_kt", doc, "data", "METAR"));
                var pressure =
                    Convert.ToDouble(XmlTools.GetElementContent("altim_in_hg", doc, "data", "METAR")) / 0.0393700732914;
                var windHeading = Convert.ToInt32(XmlTools.GetElementContent("wind_dir_degrees", doc, "data", "METAR"));
                var dewpoint = Convert.ToDouble(XmlTools.GetElementContent("dewpoint_c", doc, "data", "METAR"));
                var visibility =
                    Convert.ToDouble(XmlTools.GetElementContent("visibility_statute_mi", doc, "data", "METAR"));
                var skyconditions = XmlTools.GetAttributeValue("sky_cover", doc, "data", "METAR", "sky_condition");

                temp = Temperature.From(temp, TemperatureUnit.DegreeCelsius).As(Units.TemperatureUnit);
                windSpeed = Speed.From(windSpeed, SpeedUnit.Knot).As(Units.WindSpeedUnit);
                pressure = Pressure.From(pressure, PressureUnit.Torr).As(Units.PressureUnit);
                visibility = Length.From(visibility, LengthUnit.Mile).As(Units.VisibilityUnit);

                Weather = new Weather(temp, dewpoint, windSpeed, windHeading, pressure, visibility, skyconditions);
                Updated?.Invoke(this,
                    new LocationUpdateEventArgs(
                        $"Updated current weather conditions for {Station.ICAO} at {DateTime.Now.ToString("dd/MM/yy HH:mm:ss")}"));
            }
            catch (Exception ex)
            {
                Updated?.Invoke(this,
                    new LocationUpdateEventArgs(
                        $"Unable to update current weather conditions for {Station.ICAO} at {DateTime.Now.ToString("dd/MM/yy HH:mm:ss")}, {ex.Message}"));
                throw new WeatherUpdateException(
                    $"Unable to update current weather conditions for {Station.ICAO} at {DateTime.Now.ToString("dd/MM/yy HH:mm:ss")}",
                    ex);
            }
        }
    }

    /// <summary>
    /// Structure to hold METAR data
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
        /// Current sky conditions at the weather station
        /// </summary>
        public string SkyConditions { get; }

        /// <summary>
        /// COnstructor
        /// </summary>
        /// <param name="temperature">Temperature at the weather station</param>
        /// <param name="dewpoint">Dewpoint at the weather station</param>
        /// <param name="windspeed">Winds peed at the weather station</param>
        /// <param name="windheading">Wind heading at the weather station</param>
        /// <param name="pressure">Pressure at the weather station</param>
        /// <param name="visibility">Visibility at the weather station</param>
        /// <param name="skyconditions">Sky conditions at the weather station</param>
        public Weather(double temperature, double dewpoint, double windspeed, int windheading, double pressure,
            double visibility, string skyconditions)
        {
            Temperature = temperature;
            Dewpoint = dewpoint;
            WindSpeed = windspeed;
            WindHeading = windheading;
            Pressure = pressure;
            Visibility = visibility;
            SkyConditions = skyconditions;
        }
    }

    /// <summary>
    /// Exception for errors in pulling or interpreting METAR from NOAA
    /// </summary>
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
}