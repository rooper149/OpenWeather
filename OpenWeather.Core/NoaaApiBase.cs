using OpenWeather.Core.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using System;

namespace OpenWeather.Core
{
    public class NoaaApi
    {

        public string Token { get; }

        public NoaaApi(string token)
        {
            Token = token;
        }

        public async Task<IEnumerable<Station>> GetStationsAsync()
        {
            StationFileParser stationFileParser = new StationFileParser("https://www.aviationweather.gov/static/adds/metars/stations.txt");
            IEnumerable<Station> stations = await stationFileParser.GetStationsAsync();
            return stations;
        }

        public async Task<CurrentObservations> GetCurrentObservationsAsync(Station station)
        {
            string url = $"http://forecast.weather.gov/MapClick.php?lat={station.Latitude}&lon={station.Longitude}&FcstType=dwml";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Open Weather");

                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        XDocument content = XDocument.Parse(await response.Content.ReadAsStringAsync());
                        IEnumerable<XElement> dataElements = content.Element("dwml").Elements("data");
                        XElement currentObservationElement = dataElements.SingleOrDefault(x => x.Attribute("type").Value == "current observations");
                        XElement forcastElement = dataElements.SingleOrDefault(x => x.Attribute("type").Value == "forecast");

                        XElement currentObservationParameters = currentObservationElement.Element("parameters");
                        
                        string temperatureValue = currentObservationParameters.Elements("temperature").Where(x => x.Attribute("type").Value == "apparent").Select(x => x.Value).SingleOrDefault();
                        string dewPointValue = currentObservationParameters.Elements("temperature").Where(x => x.Attribute("type").Value == "dew point").Select(x => x.Value).SingleOrDefault();
                        string humidityValue = currentObservationParameters.Elements("humidity").Where(x => x.Attribute("type").Value == "relative").Select(x => x.Value).SingleOrDefault();

                        XElement weatherElement = currentObservationParameters.Element("weather");
                        XElement weatherconditionsElement = weatherElement.Elements("weather-conditions").Where(x => x.Element("value") != null).SingleOrDefault();
                        XElement weatherValueElement = weatherconditionsElement.Element("value");
                        XElement weatherConditionsVisibilityElement = weatherValueElement.Element("visibility");
                        string visibilityValue = weatherConditionsVisibilityElement.Value;

                        string windHeadingValue = currentObservationParameters.Element("direction").Element("value").Value;
                        string windSpeedValue = currentObservationParameters.Elements("wind-speed").Where(x => x.Attribute("type").Value == "gust").Select(x => x.Element("value").Value).SingleOrDefault();
                        string windSustainedValue = currentObservationParameters.Elements("wind-speed").Where(x => x.Attribute("type").Value == "sustained").Select(x => x.Element("value").Value).SingleOrDefault();
                        string pressureValue = currentObservationParameters.Elements("pressure").Where(x => x.Attribute("type").Value == "barometer").Select(x => x.Element("value").Value).SingleOrDefault();

                        return new CurrentObservations(temperatureValue.ToDouble(), dewPointValue.ToDouble(), humidityValue.ToDouble(), visibilityValue.ToDouble(),
                            windHeadingValue.ToDouble(), windSpeedValue.ToDouble(), windSustainedValue.ToDouble(), pressureValue.ToDouble());
                    }
                }
            }

            return null;
        }
    }
}
