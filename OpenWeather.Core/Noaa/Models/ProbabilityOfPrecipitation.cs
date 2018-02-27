using System.Collections.Generic;

namespace OpenWeather.Noaa.Models
{
    public enum ProbabilityOfPrecipitationUnits
    {
        Percent,
    }

    public class ProbabilityOfPrecipitation : ForecastElementBase
    {
        public ProbabilityOfPrecipitationUnits Unit { get; set; }
        public List<ProbabilityOfPrecipitationValue> Values { get; set; }
    }

    public class ProbabilityOfPrecipitationValue : ForecastValueBase
    {
        public decimal? Value { get; set; }
    }
}
