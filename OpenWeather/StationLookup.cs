using System;

namespace OpenWeather
{
    public class StationLookup
    {
        private static readonly Lazy<StationLookup> _lazy =
            new Lazy<StationLookup>(() => new StationLookup());

        private StationLookup() { stations = new StationDataTable(); }
        public static StationLookup Instance => _lazy.Value;

        private bool isPersistent = true;
        private StationDataTable stations;

        public Station Lookup(string icao) => !isPersistent ? new StationDataTable().GetStation(icao) : stations.GetStation(icao);

        public Station Lookup(double latitude, double longitude) => !isPersistent ? new StationDataTable().GetClosestStation(latitude, longitude)
            : stations.GetClosestStation(latitude, longitude);

        public void SetPersistentLookup(bool val)
        {
            isPersistent = val;
            stations = val ? new StationDataTable() : null;

            if(!val)
                GC.Collect();
        }
    }
}