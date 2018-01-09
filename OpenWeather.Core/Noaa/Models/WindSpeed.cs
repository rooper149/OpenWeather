using System.Collections.Generic;

namespace OpenWeather.Noaa.Models
{
    public enum WindSpeedUnits
    {
        Percent,
        MPH,
        Knots
    }

    public class WindSpeed : ForecastElementBase
    {
        public WindSpeedUnits Unit { get; set; }
        public List<WindSpeedValue> Values { get; set; }
    }

    public class WindSpeedValue : ForecastValueBase
    {
        public double Value { get; set; }
    }
}
