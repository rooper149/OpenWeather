using System.Collections.Generic;

namespace OpenWeather.Noaa.Models
{

    public enum FireWeatherUnits
    {
        None
    }

    public class FireWeather : ForecastElementBase
    {
        public FireWeatherUnits Unit { get; set; }
        public List<FireWeatherValue> Values { get; set; }
    }

    public class FireWeatherValue : ForecastValueBase
    {
        public string Value { get; set; }
    }
}
