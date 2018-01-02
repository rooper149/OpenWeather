using OpenWeather.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenWeather.Core
{
    public class NoaaApi
    {

        public string Token { get; }

        public NoaaApi(string token)
        {
            Token = token;
        }

        public async Task GetStationsAsync()
        {
            StationFileParser stationFileParser = new StationFileParser("https://www.aviationweather.gov/static/adds/metars/stations.txt");
            IEnumerable<Station> stations = await stationFileParser.GetStationsAsync();


        }
    }
}
