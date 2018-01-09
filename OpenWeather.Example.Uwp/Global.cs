using OpenWeather.Noaa.Models;
using System;
using System.Collections.Generic;

namespace OpenWeather.Example.Uwp
{
    internal static class Global
    {
        public static event EventHandler StationsUpdated;
        internal static IEnumerable<Station> Stations { get; set; }
        internal static void RaiseStationsUpdated()
        {
            if (StationsUpdated != null) StationsUpdated.Invoke(null, null);
        }
    }
}
