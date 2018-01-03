using OpenWeather.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OpenWeather
{
    public class NoaaApi : NoaaWebServiceBase
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

        public async Task<CurrentObservation> GetCurrentObservationsAsync(Station station)
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

                        //return new CurrentObservation(temperatureValue.ToDouble(), dewPointValue.ToDouble(), humidityValue.ToDouble(), visibilityValue.ToDouble(),
                        //    windHeadingValue.ToDouble(), windSpeedValue.ToDouble(), windSustainedValue.ToDouble(), pressureValue.ToDouble());
                    }
                }
            }

            return null;
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
    }
}
