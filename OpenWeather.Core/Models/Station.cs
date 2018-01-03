using System;

namespace OpenWeather.Models
{
    public enum AviationFlags
    {
        None = 0,
        Airmet_Sigmet = 1,
        ARTCC = 2,
        TAF = 4,
    }

    public enum UpperAirFlags
    {
        None,
        Rawinsonde,
        WindProfiler,
        Unknown
    }

    public enum AutomationFlags
    {
        None,
        Automated_Surface_Observing_Systems,
        Automated_Weather_Observing_Systems,
        MesoNet,
        Human,
        Augmented,
        Unknown
    }

    public enum OfficeTypeFlags
    {
        None,
        Weather_Forecast_Office,
        River_Forcast_Center,
        National_Centers_for_Environmental_Prediction,
        Unknown
    }


    public class Station
    {
        public string StateOrProvince { get; set; }

        public string Name { get; set; }
        public string ICAO { get; set; }
        public string IATA { get; set; }
        public int SYNOP { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Elevation { get; set; }
        public bool IsMetar { get; set; }
        public bool IsNexRad { get; set; }

        public string AviationIndicator { get; set; }
        public AviationFlags AviationFlag
        {
            get
            {
                if (String.IsNullOrWhiteSpace(AviationIndicator)) return AviationFlags.None;
                if (AviationIndicator == "V")
                    return AviationFlags.Airmet_Sigmet;
                else if (AviationIndicator == "A")
                    return AviationFlags.ARTCC;
                else if (AviationIndicator == "T")
                    return AviationFlags.TAF;
                else if (AviationIndicator == "U")
                    return AviationFlags.Airmet_Sigmet | AviationFlags.TAF;
                else
                    return AviationFlags.None;
            }
        }

        public string UpperAiraIndicator { get; set; }
        public UpperAirFlags UpperAirFlag
        {
            get
            {
                if (String.IsNullOrWhiteSpace(UpperAiraIndicator))
                    return UpperAirFlags.None;
                else if (UpperAiraIndicator == "X")
                    return UpperAirFlags.Rawinsonde;
                else if (UpperAiraIndicator == "W")
                    return UpperAirFlags.WindProfiler;
                else
                    return UpperAirFlags.Unknown;
            }
        }

        public string AutomationIndicator { get; set; }
        public AutomationFlags AutomationFlag
        {
            get
            {
                if (String.IsNullOrWhiteSpace(AutomationIndicator))
                    return AutomationFlags.None;
                else if (AutomationIndicator == "A")
                    return AutomationFlags.Automated_Surface_Observing_Systems;
                else if (AutomationIndicator == "W")
                    return AutomationFlags.Automated_Weather_Observing_Systems;
                else if (AutomationIndicator == "M")
                    return AutomationFlags.MesoNet;
                else if (AutomationIndicator == "H")
                    return AutomationFlags.Human;
                else if (AutomationIndicator == "G")
                    return AutomationFlags.Augmented;
                else
                    return AutomationFlags.Unknown;
            }
        }

        public string OfficeTypeIndicator { get; set; }
        public OfficeTypeFlags OfficeTypeFlag
        {
            get
            {
                if (String.IsNullOrWhiteSpace(OfficeTypeIndicator))
                    return OfficeTypeFlags.None;
                else if (OfficeTypeIndicator == "F")
                    return OfficeTypeFlags.Weather_Forecast_Office;
                else if (OfficeTypeIndicator == "R")
                    return OfficeTypeFlags.River_Forcast_Center;
                else if (OfficeTypeIndicator == "C")
                    return OfficeTypeFlags.National_Centers_for_Environmental_Prediction;
                else
                    return OfficeTypeFlags.Unknown;
            }
        }


        public int Priority { get; set; }
        public string CountryCode { get; set; }

    }
}
