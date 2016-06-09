using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OpenWeather;
using UnitsNet.Units;

namespace Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Optional, build the StationDataTable without any actions, otherwise it will be built upon first loopkup like below.
            //On average, increases the first lookup time by 5 times. Obviously it's of no use in this application, but for an
            //application that runs lookups at a later time, you could build the table at the start and have it ready for later (assuming you even want persistant lookup).
            //StationLookup.Instance.ZeroActionInitialize();

            var locationWeather = new LocationWeather(StationLookup.Instance.Lookup(-90, -180), new Units(TemperatureUnit.DegreeCelsius,
                PressureUnit.Millibar, SpeedUnit.KilometerPerHour, LengthUnit.Kilometer));

            Console.WriteLine($"Station: {locationWeather.Station.Name}\n" +
                              $"ICAO: {locationWeather.Station.ICAO}\n" +
                              $"Temperature: {locationWeather.Weather.Temperature} {locationWeather.Units.TemperatureUnit}\n" +
                              $"Pressure: {locationWeather.Weather.Pressure} {locationWeather.Units.PressureUnit}\n" +
                              $"Wind Speed: {locationWeather.Weather.WindSpeed} {locationWeather.Units.WindSpeedUnit}");
            Console.ReadLine();
        }
    }
}
