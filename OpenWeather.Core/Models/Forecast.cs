using System;
using System.Collections.Generic;

namespace OpenWeather.Models
{


    public class Forecast
    {
        public ForcastSource Source { get; set; }
        public List<ForcastTimeLine> Timelines { get; set; }

        public Forecast()
        {
            Timelines = new List<ForcastTimeLine>();
        }
    }

    public class ForcastSource
    {
        public string MoreInformation { get; set; }
        public string ProductionCenter { get; set; }
        public string Disclaimer { get; set; }
        public string Credit { get; set; }
        public string CreditLogoUrl { get; set; }
        public string Feedback { get; set; }

    }

    public class ForcastTimeLine
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public double MaximumTemperature { get; set; }
        public double MinimumTemperature { get; set; }
        public double Snow { get; set; }
        public double Ice { get; set; }
        public double Humidity { get; set; }
    }

    public class ConvectiveHazard
    {
    }
}
