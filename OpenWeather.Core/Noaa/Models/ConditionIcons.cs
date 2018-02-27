using System;
using System.Collections.Generic;

namespace OpenWeather.Noaa.Models
{

    public class ConditionIcons : ForecastElementBase
    {
        public List<ConditionIconValue> Values { get; set; }
    }

    public class ConditionIconValue : ForecastValueBase
    {
        public Uri Value { get; set; }
    }
}
