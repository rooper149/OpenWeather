using System.Collections.Generic;

namespace OpenWeather.Noaa.Models
{
    public enum WindDirectionUnits
    {
        DegreesTrueNorth
    }


    public class WindDirection : ForecastElementBase
    {
        public WindDirectionUnits Units { get; set; }
        public List<WindDirectionValue> Values { get; set; }
    }

    public class WindDirectionValue : ForecastValueBase
    {
        public double Value { get; set; }
    }
}
