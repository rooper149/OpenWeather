using System;
using System.Net;
using System.Threading;
using System.Xml.Linq;
using UnitsNet;
using UnitsNet.Units;

namespace OpenWeather
{
    public delegate void LocationWeatherUpdated(object source, LocationUpdateEventArgs e);

    public class LocationUpdateEventArgs : EventArgs
    {
        private readonly string eventInfo;

        public LocationUpdateEventArgs(string text)
        {
            eventInfo = text;
        }

        public string GetInfo() => eventInfo;
    }

    public class LocationWeather
    {
        private readonly Timer updateIntervalTimer;
        private const int UPDATE_INTERVAL = 600;

        public LocationWeather(Station station, Units units)
        {
            Station = station;
            Units = units;
            Update();

            updateIntervalTimer = new Timer(_timer_Elapsed, null, TimeSpan.FromSeconds(UPDATE_INTERVAL),
                TimeSpan.FromSeconds(UPDATE_INTERVAL));

            Update();
        }

        public LocationWeather(string apiprovider, double latitude, double longitude, Units units)
        {
            var icao = new WebClient().DownloadString($"{apiprovider}Search/?lat={latitude}&lngt={longitude}");
            Station = new Station(icao, latitude, longitude);
            Units = units;
            Update();

            updateIntervalTimer = new Timer(_timer_Elapsed, null, TimeSpan.FromSeconds(UPDATE_INTERVAL),
                TimeSpan.FromSeconds(UPDATE_INTERVAL));

            Update();
        }

        public void SetUpdateInterval(int minutes)
        {
            var seconds = minutes*60;
            updateIntervalTimer.Change(seconds, seconds);
        }

        public Station Station { get; }
        public Weather Weather { get; private set; }
        public Units Units { get; private set; }
        public event LocationWeatherUpdated Updated;

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

        private void _timer_Elapsed(object state) => Update();

        public void Update()
        {
            try
            {
                var wc = new WebClient();
                var doc = XDocument.Parse(wc.DownloadString(RequestBuilder(Station.ICAO)));
                var temp = Convert.ToDouble(XmlTools.GetElementContent("temp_c", doc, "data", "METAR"));
                var windSpeed = Convert.ToDouble(XmlTools.GetElementContent("wind_speed_kt", doc, "data", "METAR"));
                var pressure =
                    Convert.ToDouble(XmlTools.GetElementContent("sea_level_pressure_mb", doc, "data", "METAR"));
                var windHeading = Convert.ToInt32(XmlTools.GetElementContent("wind_dir_degrees", doc, "data", "METAR"));
                var dewpoint = Convert.ToDouble(XmlTools.GetElementContent("dewpoint_c", doc, "data", "METAR"));
                var visibility =
                    Convert.ToDouble(XmlTools.GetElementContent("visibility_statute_mi", doc, "data", "METAR"));
                var skyconditions = XmlTools.GetAttributeValue("sky_cover", doc, "data", "METAR", "sky_condition");

                temp = Temperature.From(temp, TemperatureUnit.DegreeCelsius).As(Units.TemperatureUnit);
                windSpeed = Speed.From(windSpeed, SpeedUnit.Knot).As(Units.WindSpeedUnit);
                pressure = Pressure.From(pressure, PressureUnit.Millibar).As(Units.PressureUnit);
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
                throw new WeatherUpdateException($"Unable to update current weather conditions for {Station.ICAO} at {DateTime.Now.ToString("dd/MM/yy HH:mm:ss")}", ex);

            }
        }

        private static string RequestBuilder(string icao)
            =>
                $"https://www.aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&stationString={icao}&hoursBeforeNow=1";
    }

    public struct Weather
    {
        public double Temperature { get; }
        public double Dewpoint { get; }
        public double WindSpeed { get; }
        public int WindHeading { get; }
        public double Pressure { get; }
        public double Visibility { get; }
        public string SkyConditions { get; }

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

    public class WeatherUpdateException : Exception
    {
        public WeatherUpdateException() {}

        public WeatherUpdateException(string message)
            : base(message) {}

        public WeatherUpdateException(string message, Exception inner)
            : base(message, inner) {}
    }
}