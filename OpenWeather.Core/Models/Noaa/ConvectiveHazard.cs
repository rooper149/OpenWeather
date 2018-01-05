using System.Collections.Generic;

namespace OpenWeather.Models.Noaa
{
    public enum ConvectiveHazardUnits
    {
        Percent
    }

    public class ConvectiveHazard : ForecastElementBase
    {
        public ConvectiveHazardUnits Unit { get; set; }
        public List<ConvectiveHazardValue> Values { get; set; }
    }

    public class ConvectiveHazardValue : ForecastValueBase
    {
        public double Value { get; set; }
    }
}
