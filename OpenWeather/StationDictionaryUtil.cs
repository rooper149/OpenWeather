using System.Diagnostics;

namespace OpenWeather
{
    public partial class StationDictionary
    {
        public static bool Init()
        {
            var count = _Dictionary.Count;//forces the object to be created now
            Debug.WriteLine($@"Dictionary initialized with {count} stations.");
            return count != 0;
        }

        public static bool TryGetStation(string icao, out StationInfo station)
        {
            station = default;
            if (!_Dictionary.ContainsKey(icao)) { return false; }
            station = _Dictionary[icao];
            return true;
        }

        public static bool TryGetClosestStation(double lat, double lon, out StationInfo station)
        {
            station = default;
            if (!_Dictionary.Any()) { return false; }

            var location = new Location(lat, lon);
            station = _Dictionary.OrderBy(x => x.Value.Location.DistanceTo(location)).First().Value;
            return true;
        }
    }
}
