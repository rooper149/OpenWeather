using System.Collections.Generic;

namespace OpenWeather.Noaa.Models
{
    public class Weather : ForecastElementBase
    {
        public List<WeatherConditions> Conditions { get; set; }
    }

    public class WeatherConditions : ForecastValueBase
    {
        public List<WeatherConditionsValue> Values { get; set; }
    }


    public class WeatherConditionsValue
    {
        public string Coverage { get; set; }
        public string Intensity { get; set; }
        public string WeatherType { get; set; }
        public string Additive { get; set; }
        public string Qualifier { get; set; }
    }
}
