using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeather.Core.Models
{
    public class CurrentObservations
    {
        public double Temperature { get;  }
        public double DewPoint { get;  }
        public double Humidity { get;  }
        public double Visibility { get;  }
        public double WindHeading { get;  }
        public double WindGusts { get;  }
        public double WindSustained { get;  }
        public double Pressure { get;  }

        public CurrentObservations(double temperature = 0, double dewPoint = 0, double humidity = 0, double visibility = 0, double windHeading = 0, double windGusts = 0, 
            double windSustained = 0, double pressure = 0)
        {
            Temperature = temperature;
            DewPoint = dewPoint;
            Humidity = humidity;
            Visibility = visibility;
            WindHeading = windHeading;
            WindGusts = windGusts;
            WindSustained = windSustained;
            Pressure = pressure;
        }
    }

}
