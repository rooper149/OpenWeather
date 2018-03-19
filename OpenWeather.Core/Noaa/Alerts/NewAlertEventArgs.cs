using System.Collections.Generic;

namespace OpenWeather.Noaa.Alerts
{
    public class NewAlertEventArgs
    {
        public NewAlertEventArgs(IEnumerable<WeatherAlert> alerts)
        {
            Alerts = alerts;
        }

        public IEnumerable<WeatherAlert> Alerts { get; }

    }
}
