## [![nuget](https://img.shields.io/nuget/v/OpenWeather.Core.svg)](https://www.nuget.org/packages/OpenWeather.Core/)
   
# OpenWeather .NET Standard Fork
This is a fork for the original open weather - using NOAA data.
- Utilized Task Parallel Library (TPL) 
- Uses raw xml feeds.
- Uses Web Services

## Features ##
### Stations ###
Downloaded from NOAA; It contains all available stations worldwide. It is recommmended you cache this file to avoid recursive calls to the NOAA web site.

### Current Observations ###
Downloaded through the NOAA xml service. This module compiles a url to download information in xml format. This module then compiles the information into easy-to-use CLR object.

### Forecast and Forecast History ###
Downloaded through NOAA's NDFD php based web service. This module allows you to query for 98% of the information available as well as past data. It will then product a time line containing with the results. The timeline will skip times if there are no informaiton available.

[Example]
- 05:00
   - Apparent Temp
   - Releative Humidity
- 06:00
   - Apparent Temp
   - Convection Hazard
- 20:00
   - Weather Condition
   - Min Temp
   - Min Humiditiy
   
**Note:** That the data available is up to the feeds publisher. For example, Elkhart, IN (KEKM) can hold 2 days of history.

# Original Project
## OpenWeather
Open Weather is a simple library designed to take a coordinate (latitude and longitude) and find the closest weather station to that coordinate while also getting the current METAR and TAF weather data and parsing it.
