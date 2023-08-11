### This project has been quiet for a while, but I am working on udpates now.

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

        static void Main(string[] args)
        {
            // get the closest station to 29.3389, -98.4717 lat/lon
            if(!OpenWeather.StationDictionary.TryGetClosestStation(29.3389, -98.4717, out var stationInfo))
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

![image](https://github.com/rooper149/OpenWeather/assets/2343056/7622c7be-73de-4c14-a935-e567d965f510)

### License
OpenWeather is under the BSD 3-Clause License.
