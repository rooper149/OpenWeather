#if !ANDROID
using System.Device.Location;
#endif

namespace OpenWeather
{
    /// <summary>
    /// Object to hold information on a valid METAR reporting weather station
    /// </summary>
    public class Station
    {
        /// <summary>
        /// The weather station's ICAO code
        /// </summary>
        public string ICAO { get; }

        /// <summary>
        /// A generated url to lookup the past 24 hours of data from NOAA from the weatehr station.
        /// </summary>
        public string LookupUrl => GenerateLoopupUrl(ICAO);

        /// <summary>
        /// Defines how many hours of observation this station should request.
        /// </summary>
        public int HoursOfData { get; private set; }

        #region META

        /// <summary>
        /// Station name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The elevation of the weather station in meters
        /// </summary>
        public double Elevation { get; }

        /// <summary>
        /// Two character coutry code
        /// </summary>
        public string Country { get; }

        /// <summary>
        /// Two character state/region code
        /// </summary>
        public string Region { get; }

        /// <summary>
        /// Station location (city)/name
        /// </summary>
        public string City => Name;

        #endregion META

        #region LOCATION

        /// <summary>
        /// Contains the latitude and logitude of the station with in a GeoCoordinate object.
        /// </summary>
        public GeoCoordinate Location { get; }

        #endregion LOCATION

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="icao">ICAO code for the weather station</param>
        /// <param name="latitude">Latitude of the weather station</param>
        /// <param name="logitude">Logitude of the weather station</param>
        public Station(string icao, double latitude, double logitude)
        {
            ICAO = icao;
            HoursOfData = 24;
            Location = new GeoCoordinate(latitude, logitude);
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
        public Station(string icao, double latitude, double logitude, double elevation, string country, string region,
            string name)
        {
            ICAO = icao;
            Name = name;
            Region = region;
            HoursOfData = 24;
            Country = country;
            Elevation = elevation;
            Location = new GeoCoordinate(latitude, logitude);
        }

        /// <summary>
        /// Sets the number of hours of onservations to collect.
        /// </summary>
        /// <param name="hours">Number of hours</param>
        public void SetHoursOfData(int hours) => HoursOfData = hours;

        /// <summary>
        /// Method to generate the METAR lookup url
        /// </summary>
        /// <param name="icao">ICAO code for the weather station</param>
        /// <returns>URL too lookup 24 hours of METAR data from NOAA</returns>
        private string GenerateLoopupUrl(string icao)
            =>
                $"https://www.aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&stationString={icao}&hoursBeforeNow={HoursOfData}";

        /// <summary>
        /// Static method to generate the METAR lookup url
        /// </summary>
        /// <param name="icao">ICAO code for the weather station</param>
        /// <param name="hours">Hours of data to collect</param>
        /// <returns>URL too lookup 24 hours of METAR data from NOAA</returns>
        public static string GenerateLoopupUrl(string icao, int hours = 24)
        =>
            $"https://www.aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&stationString={icao}&hoursBeforeNow={hours}";
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