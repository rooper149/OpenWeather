using gov.weather.graphical;
using OpenWeather.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenWeather.Noaa
{
    public class Api : NoaaWebServiceBase
    {

        public async Task<IEnumerable<Station>> GetStationsAsync()
        {
            StationFileParser stationFileParser = new StationFileParser("https://www.aviationweather.gov/static/adds/metars/stations.txt");
            IEnumerable<Station> stations = await stationFileParser.GetStationsAsync();
            return stations;
        }

        public async Task<CurrentObservation> GetCurrentObservationsByStationAsync(Station station)
        {
            string url = $"http://w1.weather.gov/xml/current_obs/display.php?stid={station.ICAO}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Open Weather");

                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        XDocument doc = XDocument.Parse(responseString);
                        if (doc == null) return null;

                        XElement root = doc.Element("current_observation");
                        if (root == null) return null;

                        return new CurrentObservation()
                        {
                            Location = root.Element("location").ValueIfExists(),
                            LastUpdated = root.Element("observation_time_rfc822").ValueIfExists().ToDateTime(),
                            WeatherIconUrl = root.Element("icon_url_base").ValueIfExists() + root.Element("icon_url_name").ValueIfExists(),
                            Weather = root.Element("weather").ValueIfExists(),
                            WindDirection = root.Element("wind_dir").ValueIfExists().ToDouble(),
                            WindDegrees = root.Element("wind_degrees").ValueIfExists().ToDouble(),
                            Wind_MPH = root.Element("wind_mph").ValueIfExists().ToDouble(),
                            Wind_Knots = root.Element("wind_kt").ValueIfExists().ToDouble(),
                            Pressure_In = root.Element("pressure_in").ValueIfExists().ToDouble(),
                            Pressure_MB = root.Element("pressure_mb").ValueIfExists().ToDouble(),
                            WindChill_F = root.Element("windchill_f").ValueIfExists().ToDouble(),
                            WindChill_C = root.Element("windchill_c").ValueIfExists().ToDouble(),
                            Visibility_Mi = root.Element("visibility_mi").ValueIfExists().ToDouble(),
                            DewPoint_F = root.Element("dewpoint_f").ValueIfExists().ToDouble(),
                            DewPoint_C = root.Element("dewpoint_c").ValueIfExists().ToDouble(),
                            Humidity = root.Element("relative_humidity").ValueIfExists().ToDouble(),
                            Temperature_F = root.Element("temp_f").ValueIfExists().ToDouble(),
                            Temperature_C = root.Element("temp_c").ValueIfExists().ToDouble()
                        };
                    }
                }
            }

            return null;
        }

        public async Task GetForecastByStationAsync(Station station, DateTime startDateTime, DateTime endDateTime, RequestType requestType, Units unit, WeatherParameters weatherParameters)
        {
            ndfdXMLPortTypeClient client = CreateClient();
            try
            {
                productType requestedProduct = requestType == RequestType.Glance ? productType.glance : productType.timeseries;
                unitType requestedUnit = unit == Units.Imperial ? unitType.e : unitType.m;

                string result = await client.NDFDgenAsync((decimal)station.Latitude, (decimal)station.Longitude, requestedProduct, startDateTime, endDateTime, requestedUnit, ConvertToWeatherParametersType(weatherParameters));
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
