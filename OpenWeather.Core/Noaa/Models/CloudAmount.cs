using System.Collections.Generic;

namespace OpenWeather.Noaa.Models
{
    public enum CloudAmountUnits
    {
        Percent
    }

    public class CloudAmount : ForecastElementBase
    {
        public CloudAmountUnits Unit { get; set; }
        public List<CloudAmountValue> Values { get; set; }
    }

    public class CloudAmountValue : ForecastValueBase
    {
        public decimal? Value { get; set; }
    }
}
