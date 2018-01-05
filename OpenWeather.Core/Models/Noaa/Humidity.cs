using System.Collections.Generic;

namespace OpenWeather.Models.Noaa
{
    public enum HumidityUnits
    {
        Percent
    }

    public class Humidity : ForecastElementBase
    {
        public HumidityUnits Unit { get; set; }
        public List<HumidityValue> Values { get; set; }
    }

    public class HumidityValue : ForecastValueBase
    {
        public double Value { get; set; }
    }
}
