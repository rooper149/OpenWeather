using System;

namespace OpenWeather.Noaa.Models
{
    public abstract class ForecastValueBase
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
