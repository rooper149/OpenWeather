using gov.weather.graphical;
using OpenWeather.Noaa.Base;
using OpenWeather.Noaa.Models;
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
                            WindDirection = root.Element("wind_dir").ValueIfExists().ToDecimal(),
                            WindDegrees = root.Element("wind_degrees").ValueIfExists().ToDecimal(),
                            Wind_MPH = root.Element("wind_mph").ValueIfExists().ToDecimal(),
                            Wind_Knots = root.Element("wind_kt").ValueIfExists().ToDecimal(),
                            Pressure_In = root.Element("pressure_in").ValueIfExists().ToDecimal(),
                            Pressure_MB = root.Element("pressure_mb").ValueIfExists().ToDecimal(),
                            WindChill_F = root.Element("windchill_f").ValueIfExists().ToDecimal(),
                            WindChill_C = root.Element("windchill_c").ValueIfExists().ToDecimal(),
                            Visibility_Mi = root.Element("visibility_mi").ValueIfExists().ToDecimal(),
                            DewPoint_F = root.Element("dewpoint_f").ValueIfExists().ToDecimal(),
                            DewPoint_C = root.Element("dewpoint_c").ValueIfExists().ToDecimal(),
                            Humidity = root.Element("relative_humidity").ValueIfExists().ToDecimal(),
                            Temperature_F = root.Element("temp_f").ValueIfExists().ToDecimal(),
                            Temperature_C = root.Element("temp_c").ValueIfExists().ToDecimal()
                        };
                    }
                }
            }

            return null;
        }

        #region Alerts




        #endregion Alerts

        public async Task<Forecast> GetForecastByStationAsync(Station station, DateTime startDateTime, DateTime endDateTime, RequestType requestType, Units unit, WeatherParameters weatherParameters)
        {
            ndfdXMLPortTypeClient client = CreateClient();
            Forecast forecast = null;

            try
            {
                productType requestedProduct = requestType == RequestType.Glance ? productType.glance : productType.timeseries;
                unitType requestedUnit = unit == Units.Imperial ? unitType.e : unitType.m;
                weatherParameters.SelectAll();

                string result = await client.NDFDgenAsync((decimal)station.Latitude, (decimal)station.Longitude, requestedProduct, startDateTime, endDateTime, requestedUnit, ConvertToWeatherParametersType(weatherParameters));
                if (String.IsNullOrWhiteSpace(result)) return null;

                ForecastParser forecastParser = new ForecastParser();
                forecast = forecastParser.ParseForecastResult(result);
            }
            catch (Exception ex)
            {
                var tt = ex;
            }

            return forecast;
        }
    }
}
