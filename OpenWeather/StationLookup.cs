#define USE_PERSISTANT_LIST
using System;

namespace OpenWeather
{
    public class StationLookup
    {
        private static readonly Lazy<StationLookup> _lazy =
            new Lazy<StationLookup>(() => new StationLookup());

        private StationLookup() {}
        public static StationLookup Instance => _lazy.Value;

#if USE_PERSISTANT_LIST
        private readonly StationDataTable stations = new StationDataTable();
#endif
        public Station Lookup(string icao)
        {
#if !USE_PERSISTANT_LIST
            var stations = new StationDataTable();
#endif
            return stations.GetStation(icao);
        }

        public Station Lookup(double latitude, double longitude)
        {
#if !USE_PERSISTANT_LIST
            var stations = new StationDataTable();
#endif
            return stations.GetClosestStation(latitude, longitude);
        }
    }
}