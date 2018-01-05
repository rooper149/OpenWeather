using System;

namespace OpenWeather.Models.Noaa
{
    public abstract class ForecastValueBase
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
