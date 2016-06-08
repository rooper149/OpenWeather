using System;
using OpenWeather.Properties;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;

#if !ANDROID
using System.Device.Location;
#else

using Android.Locations;

#endif

namespace OpenWeather
{
    /// <summary>
    /// Class to hold data table of all METAR compliant weather stations
    /// </summary>
    public class StationDataTable
    {
        /// <summary>
        /// Data table of stations
        /// </summary>
        private DataTable Stations { get; }

        /// <summary>
        /// Constructor, builds the Stations data table from official_stations.csv resource
        /// </summary>
        public StationDataTable()
        {
            Stations = new DataTable();

            Stations.Columns.Add("ICAO", typeof(string));
            Stations.Columns.Add("Latitude", typeof(double));
            Stations.Columns.Add("Longitude", typeof(double));
            Stations.Columns.Add("Elevation", typeof(double));
            Stations.Columns.Add("Country", typeof(string));
            Stations.Columns.Add("Region", typeof(string));
            Stations.Columns.Add("City", typeof(string));

            using (var reader = new StringReader(Resources.official_stations))
            {
                string[] fields;
                while ((fields = reader.ReadLine()?.Split(',')) != null)
                {
                    Debug.Assert(fields != null, "official_stations list has an error");
                    Stations.Rows.Add(fields[0], Convert.ToDouble(fields[1]), Convert.ToDouble(fields[2]),
                        Convert.ToDouble(fields[3]), fields[4], fields[5], fields[6]);
                }
            }
        }

        /// <summary>
        /// Gets the station (if any) matching an ICAO code
        /// </summary>
        /// <param name="icao">Station's ICAO code</param>
        /// <returns>A Station matching the ICAO code</returns>
        public Station GetStation(string icao)
        {
            var row = Stations.Rows.Cast<DataRow>().ToList().SingleOrDefault(r => (string)r["ICAO"] == icao);

            return new Station((string)row["ICAO"], (double)row["Latitude"], (double)row["Longitude"],
                (double)row["Elevation"],
                (string)row["Country"], (string)row["Region"], (string)row["City"]);
        }

        /// <summary>
        /// Gets the nearest station to a given coordinate
        /// </summary>
        /// <param name="coordinate">Coorodinate of location</param>
        /// <returns>The Station closest to the provided coorodinate</returns>
        public Station GetClosestStation(GeoCoordinate coordinate)
            => GetClosestStation(coordinate.Latitude, coordinate.Longitude);

        /// <summary>
        /// Gets the nearest station to a given latitude and longitude
        /// </summary>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <returns>The Station closest to the provided coorodinate</returns>
        public Station GetClosestStation(double latitude, double longitude)
        {
            var rows = Stations.Rows.Cast<DataRow>().ToList();
            var closestStation = new Station((string)rows[0]["ICAO"], (double)rows[0]["Latitude"],
                (double)rows[0]["Longitude"], (double)rows[0]["Elevation"],
                (string)rows[0]["Country"], (string)rows[0]["Region"], (string)rows[0]["City"]);

#if !ANDROID
            var location = new GeoCoordinate(latitude, longitude);

            foreach (var row in from row in rows.Skip(1)
                let dest = new GeoCoordinate((double) row["Latitude"], (double) row["Longitude"])
                where location.GetDistanceTo(dest) < location.GetDistanceTo(closestStation.Location)
                select row)
                closestStation = new Station((string) row["ICAO"], (double) row["Latitude"],
                    (double) row["Longitude"], (double) row["Elevation"],
                    (string) row["Country"], (string) row["Region"], (string) row["City"]);
#else
            var location = new Location("ORGIN")
            {
                Latitude = latitude,
                Longitude = longitude
            };

            foreach (var row in from row in rows
                                let possibleEndPoint = new Location("DEST_P")
                                {
                                    Latitude = (double)row["Latitude"],
                                    Longitude = (double)row["Longitude"]
                                }
                                let currentEndPoint = new Location("DEST")
                                {
                                    Latitude = closestStation.Location.Latitude,
                                    Longitude = closestStation.Location.Longitude
                                }
                                where location.DistanceTo(possibleEndPoint) < location.DistanceTo(currentEndPoint)
                                select row)
                closestStation = new Station((string)row["ICAO"], (double)row["Latitude"],
                    (double)row["Longitude"], (double)row["Elevation"],
                    (string)row["Country"], (string)row["Region"], (string)row["City"]);
#endif
            return closestStation;
        }
    }
}