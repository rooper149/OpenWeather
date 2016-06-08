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
    public class StationDataTable
    {
        public DataTable Stations { get; }

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

        public Station GetStation(string icao)
        {
            var row = Stations.Rows.Cast<DataRow>().ToList().SingleOrDefault(r => (string) r["ICAO"] == icao);

            return new Station((string) row["ICAO"], (double) row["Latitude"], (double) row["Longitude"],
                (double) row["Elevation"],
                (string) row["Country"], (string) row["Region"], (string) row["City"]);
        }

        public Station GetClosestStation(double latitude, double longitude)
        {

            var rows = Stations.Rows.Cast<DataRow>().ToList();
            var closestStation = new Station((string) rows[0]["ICAO"], (double) rows[0]["Latitude"],
                (double) rows[0]["Longitude"], (double) rows[0]["Elevation"],
                (string) rows[0]["Country"], (string) rows[0]["Region"], (string) rows[0]["City"]);

#if !ANDROID
            var location = new GeoCoordinate(latitude, longitude);

            foreach (var row in from row in rows.Skip(1) let dest = new GeoCoordinate((double)row["Latitude"], (double)row["Longitude"])
                                where location.GetDistanceTo(dest) < location.GetDistanceTo(closestStation.Location) select row)
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
                    Latitude = (double) row["Latitude"],
                    Longitude = (double) row["Longitude"]
                }
                let currentEndPoint = new Location("DEST")
                {
                    Latitude = closestStation.Location.Latitude,
                    Longitude = closestStation.Location.Logitude
                }
                where location.DistanceTo(possibleEndPoint) < location.DistanceTo(currentEndPoint)
                select row)
                closestStation = new Station((string) row["ICAO"], (double) row["Latitude"],
                    (double) row["Longitude"], (double) row["Elevation"],
                    (string) row["Country"], (string) row["Region"], (string) row["City"]);
#endif
            return closestStation;
        }
    }
}
