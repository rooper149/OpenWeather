namespace OpenWeather.Data
{
    /// <summary>
    /// Struct to hold station information
    /// </summary>
    public struct StationInfo
    {
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
        public int Elevation { get; set; }

        /// <summary>
        /// Two character coutry code
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Two character state/region code
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// The latitude of the station
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// The logitude of the station
        /// </summary>
        public double Longitude { get; set; }

        public StationInfo(string icao, string name, int elevation, string country, string region, double lat, double lon)
        {
            ICAO = icao;
            Name = name;
            Elevation = elevation;
            Country = country;
            Region = region;
            Latitude = lat;
            Longitude = lon;
        }
    }
}