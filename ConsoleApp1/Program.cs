using OpenWeather;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // get the closest station to 29.3389, -98.4717 lat/lon
            if (!OpenWeather.StationDictionary.TryGetClosestStation(29.3389, -98.4717, out var stationInfo))
            {
                Console.WriteLine($@"Could not find a station.");
                return;
            }

            Console.WriteLine($@"Name: {stationInfo.Name}");
            Console.WriteLine($@"ICAO: {stationInfo.ICAO}");
            Console.WriteLine($@"Lat/Lon: {stationInfo.Latitude}, {stationInfo.Longitude}");
            Console.WriteLine($@"Elevation: {stationInfo.Elevation}m");
            Console.WriteLine($@"Country: {stationInfo.Country}");
            Console.WriteLine($@"Region: {stationInfo.Region}");

            // get a MetarStation and autoupdate every 30 minutes
            // you can change this via Settings._UpdateIntervalSeconds = yourValue
            var metarStation = stationInfo.AsMetarStation(true, true);

            // subscribe to updates on the station
            _ = metarStation.Subscribe(x =>
            {
                Console.WriteLine("\n\nCurrent METAR Report:");
                Console.WriteLine($@"Temperature: {x.Temperature}C");
                Console.WriteLine($@"Wind Heading: {x.WindHeading}");
                Console.WriteLine($@"Wind Speed: {x.WindSpeed}Kts");
                Console.WriteLine($@"Dewpoint: {x.Dewpoint}");
                Console.WriteLine($@"Visibility: {x.Visibility}Km");
                Console.WriteLine($@"Presure: {x.Pressure}Pa");
            });

            Console.ReadLine();
        }
    }
}