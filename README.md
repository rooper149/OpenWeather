# This project has been quiet for a while, but I am working on udpates now.

### Welcome to OpenWeather.
Open Weather is a simple library designed to take a coordinate (latitude and longitude) and find the closest weather station to that coordinate while also getting the current METAR or TAF weather data and parsing it. It has other features like being cross-platform (Windows/Linux/Android), searching by ICAO codes, automatic update intervals, and unit conversions. We hope to be adding more and more features as development continues!

### Pre-Compiled Packages
You can get a pre-compiled nuget package by searching OpenWeather in NuGet or going directly to our NuGet page:
[https://www.nuget.org/packages/OpenWeather/](https://www.nuget.org/packages/OpenWeather/)

### The Data
All the METAR data is obtained through [NOAA](http://www.noaa.gov/) and the [Aviation Weather Center](https://www.aviationweather.gov/).
Our weather station lookup list is adapted from Greg Thompson's station list which can be found [here](https://www.aviationweather.gov/docs/metar/stations.txt).

### Getting Started
It's easy to search for a station and start getting weather data!

     private static void Main(string[] args)
        {
            //Optional, build the StationDataTable without any actions, otherwise it will be built upon first loopkup like below.
            //On average, increases the first lookup time by 5 times. Obviously it's of no use in this application, but for an
            //application that runs lookups at a later time, you could build the table at the start and have it ready for later 
            (assuming you even want persistant lookup).
            //StationLookup.ZeroActionInitialize();

            var station = MetarStationLookup.Instance.Lookup(-90, -180);

            Console.WriteLine($"Station: {station.GetStationInfo.Name}\n" +
                              $"ICAO: {station.GetStationInfo.ICAO}\n" +
                              $"Temperature: {station.Weather.Temperature} {station.Units.TemperatureUnit}\n" +
                              $"Pressure: {station.Weather.Pressure} {station.Units.PressureUnit}\n" +
                              $"Wind Speed: {station.Weather.WindSpeed} {station.Units.WindSpeedUnit}");

            Console.ReadLine();
        }

### License
OpenWeather is under the BSD 3-Clause License.
