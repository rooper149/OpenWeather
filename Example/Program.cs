using OpenWeather;
using System;
using System.Linq;

namespace Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Optional, build the StationDataTable without any actions, otherwise it will be built upon first loopkup like below.
            //On average, increases the first lookup time by 5 times. Obviously it's of no use in this application, but for an
            //application that runs lookups at a later time, you could build the table at the start and have it ready for later (assuming you even want persistant lookup).
            //StationLookup.ZeroActionInitialize();

            //var station = MetarStationLookup.Instance.Lookup(-90, -180);
            //station.Updated += Station_Updated;
            //station.Update();

            NoaaApi noaaApiBase = new NoaaApi("aJchcTLIvuFMNWvzKUgQHRyzMgsedRmX");
            var stations = noaaApiBase.GetStationsAsync().GetAwaiter().GetResult();
            var station = stations.SingleOrDefault(x => x.Name.ToLower().StartsWith("goshen") & x.StateOrProvince.ToLower() == "in");
            var currentObservations = noaaApiBase.GetCurrentObservationsByStationAsync(station).GetAwaiter().GetResult();

            Console.WriteLine($"Station: {station.Name}\n" +
                             $"ICAO: {station.ICAO}\n" +
                             $"Temperature: {currentObservations.Temperature_F}\n" +
                             $"Pressure: {currentObservations.Pressure_In} \n" +
                             $"Wind Speed: {currentObservations.Wind_MPH}");

            Console.ReadLine();
        }

        private static void Station_Updated(object source, LocationUpdateEventArgs e)
        {
            //var station = source as MetarStation;
            //Console.WriteLine($"Station: {station.GetStationInfo.Name}\n" +
            //                 $"ICAO: {station.GetStationInfo.ICAO}\n" +
            //                 $"Temperature: {station.Weather.Temperature} {station.Units.TemperatureUnit}\n" +
            //                 $"Pressure: {station.Weather.Pressure} {station.Units.PressureUnit}\n" +
            //                 $"Wind Speed: {station.Weather.WindSpeed} {station.Units.WindSpeedUnit}");
        }
    }
}