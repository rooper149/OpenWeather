using System.Collections.Generic;

namespace OpenWeather.Models.Noaa
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
        public double Value { get; set; }
    }
}
