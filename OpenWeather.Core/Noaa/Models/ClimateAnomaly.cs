using System.Collections.Generic;

namespace OpenWeather.Noaa.Models
{

    public enum ClimateAnomalyUnits
    {
        Percent
    }

    public class ClimateAnomaly : ForecastElementBase
    {
        public ClimateAnomalyUnits Unit { get; set; }
        public List<ClimateAnomalyValue> Values { get; set; }
    }

    public class ClimateAnomalyValue : ForecastValueBase
    {
        public double Value { get; set; }
    }
}
