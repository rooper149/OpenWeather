using System;
#if !ANDROID
using System.Device.Location;
#endif

namespace OpenWeather
{
    /// <summary>
    /// Singleton to manage a StationDataTable and lookup stations
    /// </summary>
    public sealed class StationLookup
    {
        private static Lazy<StationLookup> _lazy =
            new Lazy<StationLookup>(() => new StationLookup());

        /// <summary>
        /// private constructor
        /// </summary>
        private StationLookup()
        {
            stations = new StationDataTable();
        }

        /// <summary>
        /// Instance of the singleton
        /// </summary>
        public static StationLookup Instance => _lazy.Value;

        /// <summary>
        /// Invokable method to provide the ability to build the StationDataTable for the singleton without having
        /// to create a variable to hold StationLookup.Instance or building it upon first use of the Lookup() functions.
        /// On average, increases first lookup by 5 times.
        /// </summary>
        public static StationLookup ZeroActionInitialize() => Instance;

        /// <summary>
        /// Specifies if the StationDataTable should be stored in memory or collected by GC. See 'SetPersistentLookup(bool)'
        /// </summary>
        private bool isPersistent = true;

        /// <summary>
        /// Reference for the StationDataTable
        /// </summary>
        private StationDataTable stations;

        /// <summary>
        /// Gets the station (if any) matching an ICAO code
        /// </summary>
        /// <param name="icao">Station's ICAO code</param>
        /// <returns>A Station matching the ICAO code</returns>
        public Station Lookup(string icao)
        {
            if (isPersistent) return stations.GetStation(icao);
            using (var dataTable = new StationDataTable())
                return dataTable.GetStation(icao);
        }

        /// <summary>
        /// Gets the nearest station to a given coordinate
        /// </summary>
        /// <param name="coordinate">Coorodinate of location</param>
        /// <returns>The Station closest to the provided coorodinate</returns>
        public Station Lookup(GeoCoordinate coordinate) => Lookup(coordinate.Latitude, coordinate.Longitude);

        /// <summary>
        /// Gets the nearest station to a given latitude and longitude
        /// </summary>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <returns>The Station closest to the provided coorodinate</returns>
        public Station Lookup(double latitude, double longitude)
        {
            if (isPersistent) return stations.GetClosestStation(latitude, longitude);
            using (var dataTable = new StationDataTable())
                return dataTable.GetClosestStation(latitude, longitude);
        }

        /// <summary>
        /// Sets whether the StationDataTable should be persistant in memory or be collected by GC.
        /// isPersistent is default true, so once the StationDataTable is loaded it will remain in memory
        /// due to the singleton nature of this class. This reduces the time to access and search for stations
        /// greatly since we no longer have to build the data table every time we want to search. When isPersistent
        /// is set to false, the StationDataTable is no longer kept in memory and is rather build only when we need it.
        /// This reduces the memory footprint for lower-end devices, however costs extra to initialize. Alternatively
        /// setting isPersistent to false can be useful to get rid of the data table from memory after running a multitude of searches.
        /// </summary>
        /// <param name="val">Boolean to determin if StationLookup should always keep the StationDataTable in memory</param>
        public void SetPersistentLookup(bool val)
        {
            isPersistent = val;

            if (val && (stations == null))
                stations = new StationDataTable();
            else
            {
                stations.Dispose();
                stations = null;
            }

            /*if (!val)
                GC.Collect();*/
        }
    }
}