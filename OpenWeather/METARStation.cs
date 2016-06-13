using System;
using System.Net;
using System.Xml.Linq;
using UnitsNet;
using UnitsNet.Units;

#if !ANDROID

using System.Device.Location;

#endif

namespace OpenWeather
{
    /// <summary>
    /// Object to hold information on a valid METAR reporting weather station
    /// </summary>
    public sealed class MetarStation : Station
    {
        /// <summary>
        /// Defines how many hours of observation this station should request.
        /// </summary>
        public int HoursOfData { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MetarStation(StationInfo info, bool autoUpdate, bool updateNow = true) : base(autoUpdate)
        {
            StationInfo = info;
            HoursOfData = 24;
            LookupUrl = GenerateLookupUrl();

            if (updateNow)
                Update();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="icao">ICAO code for the weather station</param>
        /// <param name="latitude">Latitude of the weather station</param>
        /// <param name="logitude">Logitude of the weather station</param>
        /// <param name="autoUpdate">Sets whether to auto update data</param>
        /// <param name="updateNow">Sets whether to update data on object creation</param>
        public MetarStation(string icao, double latitude, double logitude, bool autoUpdate, bool updateNow = true) : base(autoUpdate)
        {
            StationInfo.ICAO = icao;
            HoursOfData = 24;
            StationInfo.Location = new GeoCoordinate(latitude, logitude);
            LookupUrl = GenerateLookupUrl();

            if (updateNow)
                Update();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="icao">ICAO code for the weather station</param>
        /// <param name="latitude">Latitude of the weather station</param>
        /// <param name="logitude">Logitude of the weather station</param>
        /// <param name="elevation">Elevation of the weather station in meters</param>
        /// <param name="country">Coutry identifier (two charater code) for the weather station</param>
        /// <param name="region">Region identifier (two charater code) for the weather station</param>
        /// <param name="name">Name/City identifier for the weather station</param>
        /// <param name="autoUpdate">Sets whether to auto update data</param>
        /// <param name="updateNow">Sets whether to update data on object creation</param>
        public MetarStation(string icao, double latitude, double logitude, double elevation, string country, string region,
            string name, bool autoUpdate, bool updateNow = true) : base(autoUpdate)
        {
            StationInfo.ICAO = icao;
            StationInfo.Name = name;
            StationInfo.City = name;
            StationInfo.Region = region;
            HoursOfData = 24;
            StationInfo.Country = country;
            StationInfo.Elevation = elevation;
            StationInfo.Location = new GeoCoordinate(latitude, logitude);
            LookupUrl = GenerateLookupUrl();

            if (updateNow)
                Update();
        }

        /// <summary>
        /// Sets the number of hours of onservations to collect.
        /// </summary>
        /// <param name="hours">Number of hours</param>
        public void SetHoursOfData(int hours) => HoursOfData = hours;

        /// <summary>
        /// Gets current data from NOAA
        /// </summary>
        protected override void GetWeather()
        {
            var wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.UserAgent, "OpenWeather Client");
            var doc = XDocument.Parse(wc.DownloadString(LookupUrl));
            var temp = Convert.ToDouble(XmlTools.GetElementContent("temp_c", doc, "data", "METAR"));
            var windSpeed = Convert.ToDouble(XmlTools.GetElementContent("wind_speed_kt", doc, "data", "METAR"));
            var pressure =
                Convert.ToDouble(XmlTools.GetElementContent("altim_in_hg", doc, "data", "METAR")) / 0.0393700732914;
            var windHeading = Convert.ToInt32(XmlTools.GetElementContent("wind_dir_degrees", doc, "data", "METAR"));
            var dewpoint = Convert.ToDouble(XmlTools.GetElementContent("dewpoint_c", doc, "data", "METAR"));
            var visibility =
                Convert.ToDouble(XmlTools.GetElementContent("visibility_statute_mi", doc, "data", "METAR"));

            temp = Temperature.From(temp, TemperatureUnit.DegreeCelsius).As(Units.TemperatureUnit);
            windSpeed = Speed.From(windSpeed, SpeedUnit.Knot).As(Units.WindSpeedUnit);
            pressure = Pressure.From(pressure, PressureUnit.Torr).As(Units.PressureUnit);
            visibility = Length.From(visibility, LengthUnit.Mile).As(Units.VisibilityUnit);

            Weather = new Weather(temp, dewpoint, windSpeed, windHeading, pressure, visibility);
            wc.Dispose();
        }

        public override string GenerateLookupUrl() =>
            $"https://www.aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&stationString={StationInfo.ICAO}&hoursBeforeNow={HoursOfData}";

        /// <summary>
        /// Static method to generate the METAR lookup url
        /// </summary>
        /// <param name="icao">ICAO code for the weather station</param>
        /// <param name="hours">Hours of data to collect</param>
        /// <returns>URL too lookup 24 hours of METAR data from NOAA</returns>
        public static string GenerateLookupUrl(string icao, int hours = 24)
        =>
            $"https://www.aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&stationString={icao}&hoursBeforeNow={hours}";
    }
}