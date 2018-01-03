using System;

namespace OpenWeather.Models
{
    public class CurrentObservation
    {
        public string Location { get; internal set; }
        public DateTime LastUpdated { get; set; }
        public string Weather { get; internal set; }
        public double Temperature_F { get; internal set; }
        public double Temperature_C { get; internal set; }
        public double Humidity { get; internal set; }
        public double WindDirection { get; internal set; }
        public double WindDegrees { get; internal set; }
        public double Wind_MPH { get; internal set; }
        public double Wind_Knots { get; internal set; }
        public double Pressure_MB { get; internal set; }
        public double Pressure_In { get; internal set; }
        public double DewPoint_F { get; internal set; }
        public double DewPoint_C { get; internal set; }
        public double WindChill_F { get; internal set; }
        public double WindChill_C { get; internal set; }
        public double Visibility_Mi { get; internal set; }
        public string WeatherIconUrl { get; internal set; }

    }

}
