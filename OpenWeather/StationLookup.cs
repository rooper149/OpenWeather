//#define USE_PERSISTANT_LIST

using System;
using System.Collections.Generic;
using System.Linq;
#if !ANDROID
using System.Device.Location;
#else
using Android.Locations;
#endif

namespace OpenWeather
{
    public class StationList
    {
        private static readonly Lazy<StationList> _lazy =
            new Lazy<StationList>(() => new StationList());

        private StationList() {}
        public static StationList Instance => _lazy.Value;

#if USE_PERSISTANT_LIST
        private readonly List<Station> stations = new StationCollection().Stations;
#endif
        public Station Lookup(string icao)
        {

#if !USE_PERSISTANT_LIST
            var stations = new StationCollection().Stations;
#endif

            return stations.FirstOrDefault(s => s.ICAO == icao);
        }

        public Station Lookup(double latitude, double longitude)
        {
#if !USE_PERSISTANT_LIST
            var stations = new StationCollection().Stations;
#endif
#if !ANDROID
            var location = new GeoCoordinate(latitude, longitude);
#else
        var location = new Location("ORGIN");
            location.Latitude = latitude;
            location.Longitude = longitude;
#endif

            var closestStation = stations[0];

#if !ANDROID
            foreach (var station in stations.Where(station => location.GetDistanceTo(station.Location) < location.GetDistanceTo(closestStation.Location)))
                closestStation = station;
#else
            foreach (var station in stations)
            {
                var possibleEndPoint = new Location("DEST_P");
                possibleEndPoint.Latitude = station.Location.Latitude;
                possibleEndPoint.Longitude = station.Location.Logitude;

                var currentEndPoint = new Location("DEST");
                currentEndPoint.Latitude = closestStation.Location.Latitude;
                currentEndPoint.Longitude = closestStation.Location.Logitude;

                if (location.DistanceTo(possibleEndPoint) < location.DistanceTo(currentEndPoint))
                    closestStation = station;
            }
#endif
            return closestStation;
        }
    }
}