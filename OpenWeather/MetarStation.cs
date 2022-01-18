﻿using System.Xml.Linq;
using UnitsNet;
using UnitsNet.Units;

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
        public MetarStation(StationInfo info, bool autoUpdate = false, bool updateNow = true) : base(info, autoUpdate)
        {
            HoursOfData = 24;
            if (updateNow) { UpdateNow(); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="icao">ICAO code for the weather station</param>
        /// <param name="latitude">Latitude of the weather station</param>
        /// <param name="logitude">Logitude of the weather station</param>
        /// <param name="autoUpdate">Sets whether to auto update data</param>
        /// <param name="updateNow">Sets whether to update data on object creation</param>
        public MetarStation(string icao, double latitude, double logitude, bool autoUpdate = false, bool updateNow = true) : base(new StationInfo(icao, latitude, logitude), autoUpdate)
        {
            HoursOfData = 24;
            if (updateNow) { UpdateNow(); }
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
        public MetarStation(string icao, double latitude, double logitude, int elevation, string country, string region,
            string name, bool autoUpdate = false, bool updateNow = true) : base(new StationInfo(icao, name, elevation, country, region, latitude, logitude), autoUpdate)
        {   
            HoursOfData = 24;
            if (updateNow) { UpdateNow(); }
        }

        private async void UpdateNow()
        {
            await Update();
        }

        /// <summary>
        /// Sets the number of hours of onservations to collect.
        /// </summary>
        /// <param name="hours">Number of hours</param>
        public void SetHoursOfData(int hours) => HoursOfData = hours;

        /// <summary>
        /// Gets current data from NOAA
        /// </summary>
        protected override async Task<Weather?> FetchUpdate()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.ParseAdd($@"{nameof(OpenWeather)}/2.0");
            var result = await client.GetStringAsync(LookupUrl);

            if (string.IsNullOrEmpty(result)) { return null; }//we got nothing?

            var doc = XDocument.Parse(result);

            var temp = GetElementAsDouble(XmlTools.GetElementContent("temp_c", doc, "data", "METAR"));
            var windSpeed = GetElementAsDouble(XmlTools.GetElementContent("wind_speed_kt", doc, "data", "METAR"));
            var pressure = GetElementAsDouble(XmlTools.GetElementContent("altim_in_hg", doc, "data", "METAR"));
            var windHeading = GetElementAsInt32(XmlTools.GetElementContent("wind_dir_degrees", doc, "data", "METAR"));
            var dewpoint = GetElementAsDouble(XmlTools.GetElementContent("dewpoint_c", doc, "data", "METAR"));
            var visibility = GetElementAsDouble(XmlTools.GetElementContent("visibility_statute_mi", doc, "data", "METAR"));

            temp = Temperature.From(temp!.Value, TemperatureUnit.DegreeCelsius).As(Units.TemperatureUnit);
            windSpeed = Speed.From(windSpeed!.Value, SpeedUnit.Knot).As(Units.WindSpeedUnit);
            pressure = Pressure.From(pressure!.Value, PressureUnit.InchOfMercury).As(Units.PressureUnit);
            visibility = Length.From(visibility!.Value, LengthUnit.Mile).As(Units.VisibilityUnit);

            Weather = new Weather(Units, temp.Value, dewpoint!.Value, windSpeed.Value, windHeading!.Value, pressure.Value, visibility.Value);
            client.Dispose();
            return Weather;
        }

        protected override string GenerateLookupUrl() => $"https://www.aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&stationString={StationInfo.ICAO}&hoursBeforeNow={HoursOfData}";
    }
}
