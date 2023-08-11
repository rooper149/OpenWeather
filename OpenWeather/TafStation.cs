using System.Xml.Linq;
using UnitsNet;
using UnitsNet.Units;

namespace OpenWeather
{
    /// <summary>
    /// Used to get current and forcast TAF weather data from NOAA. United States locations only
    /// </summary>
    public sealed class TafStation : Station
    {
        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="latitude">Location latitude</param>
        /// <param name="longitude">Location longitude</param>
        /// <param name="autoUpdate">Sets whether to auto update data</param>
        /// <param name="updateNow">Sets whether to update data on object creation</param>
        public TafStation(double latitude, double longitude, bool updateNow = true, bool autoUpdate = false) : base(new StationInfo(latitude, longitude), Settings._UpdateIntervalSeconds, autoUpdate)
        {
            if (updateNow) { UpdateNow(); }
        }

        private async void UpdateNow()
        {
            await Update();
        }

        /// <summary>
        /// Gets current and forcast data from NOAA
        /// </summary>
        protected override async Task<Weather?> FetchUpdate()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd(Settings._UserAgent);
            var result = await client.GetStringAsync(LookupUrl);

            if (string.IsNullOrEmpty(result)) { return null; }//we got nothing?

            var doc = XDocument.Parse(result);
            var current = XmlTools.TruncateXDocument(doc, "data", "type", "current observations");

            var temp = GetElementAsDouble(XmlTools.GetElementContent("value", current, "parameters", "temperature"));
            var windHeading = GetElementAsInt32(XmlTools.GetElementContent("value", current, "parameters", "direction"));
            var dewpoint = GetElementAsDouble(((XElement)XmlTools.GetNode(current, "parameters", "temperature")?.NextNode!).Value);
            var windSpeed = GetElementAsDouble(((XElement)XmlTools.GetNode(current, "parameters", "wind-speed")?.NextNode!).Value);
            var pressure = GetElementAsDouble(XmlTools.GetElementContent("value", current, "parameters", "pressure")) / 0.0393700732914;
            var visibility = GetElementAsDouble(((XElement)XmlTools.GetNode(current, "parameters", "weather", "weather-conditions")?.NextNode!).Value);

            if (temp is null || dewpoint is null || 
                pressure is null || windSpeed is null ||
                    windHeading is null || visibility is null)
            {
                return null;
            }

            windSpeed = Speed.From(windSpeed!.Value, SpeedUnit.Knot).As(Units.WindSpeedUnit);
            visibility = Length.From(visibility!.Value, LengthUnit.Mile).As(Units.VisibilityUnit);
            pressure = Pressure.From(pressure!.Value, PressureUnit.InchOfMercury).As(Units.PressureUnit);
            temp =  Temperature.From(temp!.Value, TemperatureUnit.DegreeFahrenheit).As(Units.TemperatureUnit);

            var weather = new Weather(Units, temp.Value, dewpoint!.Value, windSpeed.Value, windHeading!.Value, pressure.Value, visibility.Value);
            client.Dispose();
            return weather;
        }

        protected override string GenerateLookupUrl() => $"http://forecast.weather.gov/MapClick.php?lat={StationInfo.Location.Latitude}&lon={StationInfo.Location.Longitude}&FcstType=dwml";
    }
}
