using OpenWeather;
using System;

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

            var station = MetarStationLookup.Instance.Lookup(-90, -180);

            Console.WriteLine($"Station: {station.GetStationInfo.Name}\n" +
                              $"ICAO: {station.GetStationInfo.ICAO}\n" +
                              $"Temperature: {station.Weather.Temperature} {station.Units.TemperatureUnit}\n" +
                              $"Pressure: {station.Weather.Pressure} {station.Units.PressureUnit}\n" +
                              $"Wind Speed: {station.Weather.WindSpeed} {station.Units.WindSpeedUnit}");

            Console.ReadLine();
        }
    }
}