using System.Collections.Generic;

namespace OpenWeather.Models.Noaa
{
    public enum PrecipitationUnits
    {
        Inches,
        Centimeters
    }

    public class Precipitation : ForecastElementBase
    {
        public PrecipitationUnits Unit { get; set; }
        public List<PrecipitationValue> Values { get; set; }
    }

    public class PrecipitationValue : ForecastValueBase
    {
        public double Value { get; set; }
    }
}
