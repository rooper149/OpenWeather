using System;
using OpenWeather;
using UnitsNet.Units;

namespace Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
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
