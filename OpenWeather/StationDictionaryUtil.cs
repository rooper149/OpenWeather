using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeather
{
    public partial class StationDictionary
    {
        public static StationInfo? GetStation(string icao)
        {
            if (!_Dictionary.ContainsKey(icao)) { return null; }
            return _Dictionary[icao];
        }

        public static StationInfo GetClosestStation(double lat, double lon)
        {
            var location = new Location(lat, lon);
            return _Dictionary.OrderBy(x => x.Value.Location.DistanceTo(location)).First().Value;
        }
    }
}
