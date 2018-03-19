using System;

namespace OpenWeather.Noaa.CurrentObservartions
{
    public class CurrentObservation
    {
        public string Location { get; internal set; }
        public DateTime LastUpdated { get; set; }
        public string Weather { get; internal set; }
        public decimal? Temperature_F { get; internal set; }
        public decimal? Temperature_C { get; internal set; }
        public decimal? Humidity { get; internal set; }
        public decimal? WindDirection { get; internal set; }
        public decimal? WindDegrees { get; internal set; }
        public decimal? Wind_MPH { get; internal set; }
        public decimal? Wind_Knots { get; internal set; }
        public decimal? Pressure_MB { get; internal set; }
        public decimal? Pressure_In { get; internal set; }
        public decimal? DewPoint_F { get; internal set; }
        public decimal? DewPoint_C { get; internal set; }
        public decimal? WindChill_F { get; internal set; }
        public decimal? WindChill_C { get; internal set; }
        public decimal? Visibility_Mi { get; internal set; }
        public string WeatherIconUrl { get; internal set; }

    }

}
