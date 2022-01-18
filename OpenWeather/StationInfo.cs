namespace OpenWeather
{
    /// <summary>
    /// Struct to hold station information
    /// </summary>
    public struct StationInfo
    {
        /// <summary>
        /// Station ICAO code
        /// </summary>
        public readonly string ICAO;

        /// <summary>
        /// Station name
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// The elevation of the weather station in meters
        /// </summary>
        public readonly int Elevation;

        /// <summary>
        /// Two character coutry code
        /// </summary>
        public readonly string Country;

        /// <summary>
        /// Two character state/region code
        /// </summary>
        public readonly string Region;

        /// <summary>
        /// The latitude of the station
        /// </summary>
        public readonly double Latitude;

        /// <summary>
        /// The logitude of the station
        /// </summary>
        public readonly double Longitude;

        public Location Location => new Location(Latitude, Longitude);

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