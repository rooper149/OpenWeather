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
    /// Used to get current and forcast TAF weather data from NOAA. United States locations only
    /// </summary>
    public sealed class TafStation : Station
    {
        /// <summary>
        /// Cunstructor
        /// </summary>
        /// <param name="latitude">Location latitude</param>
        /// <param name="longitude">Location longitude</param>
        /// <param name="autoUpdate">Sets whether to auto update data</param>
        /// <param name="updateNow">Sets whether to update data on object creation</param>
        public TafStation(double latitude, double longitude, bool autoUpdate, bool updateNow = true) : base(autoUpdate)
        {
            StationInfo.Location = new GeoCoordinate(latitude, longitude);
            LookupUrl = GenerateLookupUrl();

            if (updateNow)
                Update();
        }

        /// <summary>
        /// Gets current and forcast data from NOAA
        /// </summary>
        protected override void GetWeather()
        {
            var wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.UserAgent, "OpenWeather Client");
            var doc = XDocument.Parse(wc.DownloadString(LookupUrl));

            #region CURRENT_CONDITIONS

            var current = XmlTools.TruncateXDocument(doc, "data", "type", "current observations");
            var temp = (Convert.ToDouble(XmlTools.GetElementContent("value", current, "parameters", "temperature")) - 32) * .555555;
            var dewpoint =
                Convert.ToDouble(((XElement)XmlTools.GetNode(current, "parameters", "temperature").NextNode).Value);
            var visibility =
                Convert.ToDouble(
                    ((XElement)XmlTools.GetNode(current, "parameters", "weather", "weather-conditions").NextNode).Value);
            var windHeading = Convert.ToInt32(XmlTools.GetElementContent("value", current, "parameters", "direction"));
            var windSpeed =
                Convert.ToDouble(((XElement)XmlTools.GetNode(current, "parameters", "wind-speed").NextNode).Value);
            var pressure = Convert.ToDouble(XmlTools.GetElementContent("value", current, "parameters", "pressure")) /
                           0.0393700732914;

            temp = Temperature.From(temp, TemperatureUnit.DegreeCelsius).As(Units.TemperatureUnit);
            windSpeed = Speed.From(windSpeed, SpeedUnit.Knot).As(Units.WindSpeedUnit);
            pressure = Pressure.From(pressure, PressureUnit.Torr).As(Units.PressureUnit);
            visibility = Length.From(visibility, LengthUnit.Mile).As(Units.VisibilityUnit);

            Weather = new Weather(temp, dewpoint, windSpeed, windHeading, pressure, visibility);

            #endregion CURRENT_CONDITIONS

            wc.Dispose();
        }

        public override string GenerateLookupUrl() => $"http://forecast.weather.gov/MapClick.php?lat={GetStationInfo.Location.Latitude}&lon={GetStationInfo.Location.Longitude}&FcstType=dwml";
    }
}