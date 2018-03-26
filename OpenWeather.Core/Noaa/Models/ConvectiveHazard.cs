using System.Collections.Generic;

namespace OpenWeather.Noaa.Models
{
    public enum ConvectiveHazardUnits
    {
        None,
        Percent
    }

    public class ConvectiveHazard : ForecastElementBase
    {
        public ConvectiveHazardUnits Unit { get; set; }
        public List<ConvectiveHazardValue> Values { get; set; }
    }

    public class ConvectiveHazardValue : ForecastValueBase
    {
        public decimal? Value { get; set; }
    }
}
