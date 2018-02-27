using System.Collections.Generic;

namespace OpenWeather.Noaa.Models
{
    public enum TemperatureUnits
    {
        Fahrenheit,
        Celsius
    }

    public class Temperature : ForecastElementBase
    {
        public TemperatureUnits Unit { get; set; }
        public List<TemperatureValue> Values { get; set; }
    }

    public class TemperatureValue : ForecastValueBase
    {
        public decimal? Value { get; set; }
    }
}
