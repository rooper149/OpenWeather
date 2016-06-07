#if !ANDROID
using System.Device.Location;
#endif

namespace OpenWeather
{
    public class Station
    {
        public string ICAO { get; }
        public string LookupUrl { get; private set; }

        #region META

        public double Elevation { get; private set; }
        public string Country { get; private set; }
        public string Region { get; private set; }
        public string City { get; private set; }

        #endregion

        #region LOCATION

        public GeoCoordinate Location { get; private set; }

        #endregion

        public Station(string icao, double latitude, double logitude)
        {
            ICAO = icao;
            Location = new GeoCoordinate(latitude, logitude);
            LookupUrl = GenerateLoopupUrl(ICAO);
        }

        public Station(string icao, double latitude, double logitude, double elevation, string country, string region,
            string city)
        {
            ICAO = icao;
            Elevation = elevation;
            Country = country;
            Region = region;
            City = city;
            Location = new GeoCoordinate(latitude, logitude);
            LookupUrl = GenerateLoopupUrl(ICAO);
        }

        private static string GenerateLoopupUrl(string icao)
            =>
                $"https://www.aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&stationString={icao}&hoursBeforeNow=1";

#if ANDROID
         public class GeoCoordinate
        {
            public double Latitude { get; }
            public double Logitude { get; }

            public GeoCoordinate(double latitude, double logitude)
            {
                Latitude = latitude;
                Logitude = logitude;
            }
        }
#endif
    }
}