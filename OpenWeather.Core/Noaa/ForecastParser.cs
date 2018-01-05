using OpenWeather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace OpenWeather.Noaa
{
    internal class ForecastParser
    {
        private struct LayoutTimes
        {
            internal DateTime startDateTime, endDateTime;
        }

        private struct TimeLayout
        {
            internal string key;
            internal List<LayoutTimes> times;

            internal TimeLayout(XElement element)
            {
                key = element.Element("layout-key").Value;
                times = new List<LayoutTimes>();

                List<DateTime> startDateTimes = new List<DateTime>();
                List<DateTime> endDateTimes = new List<DateTime>();

                foreach (XElement sdtElement in element.Elements("start-valid-time"))
                {
                    DateTime dateTime;
                    DateTime.TryParse(sdtElement.Value, out dateTime);
                    startDateTimes.Add(dateTime);
                }

                foreach (XElement edtElement in element.Elements("end-valid-time"))
                {
                    DateTime dateTime;
                    DateTime.TryParse(edtElement.Value, out dateTime);
                    endDateTimes.Add(dateTime);
                }

                for (int i = 0; i < startDateTimes.Count; i++)
                {
                    DateTime startDateTime = startDateTimes[i];
                    DateTime endDateTime = startDateTimes[i];
                    if (endDateTimes.Count - 1 >= i) endDateTime = endDateTimes[i];

                    times.Add(new LayoutTimes() { startDateTime = startDateTime, endDateTime = endDateTime });
                }
            }
        }

        private enum TemperatureTypes
        {
            Maximum,
            Minimum,
            Apparent,
            Dewpoint,
            Hourly
        }

        private enum PrecipitationTypes
        {
            Ice,
            Snow,
            Liquid
        }


        List<TimeLayout> timeLayouts = new List<TimeLayout>();
        List<Forecast> forecasts = new List<Forecast>();

        internal IEnumerable<Forecast> ParseForecastResult(string result)
        {
            XDocument doc = XDocument.Parse(result);
            XElement dataElement = doc.Element("dwml").Element("data");

            // Time layouts
            IEnumerable<XElement> timeLayoutElements = dataElement.Elements("time-layout");
            foreach (var item in timeLayoutElements)
            {
                timeLayouts.Add(new TimeLayout(item));
            }

            // Parameters (forcasts)
            XElement parametersElement = dataElement.Element("parameters");
            Models.Noaa.Temperature maxTemperature = GetTemperature(parametersElement, TemperatureTypes.Maximum);
            Models.Noaa.Temperature minTemperature = GetTemperature(parametersElement, TemperatureTypes.Minimum);
            Models.Noaa.Temperature apparentTemperature = GetTemperature(parametersElement, TemperatureTypes.Apparent);
            Models.Noaa.Temperature dewpointTemperature = GetTemperature(parametersElement, TemperatureTypes.Dewpoint);
            Models.Noaa.Temperature hourlyTemperature = GetTemperature(parametersElement, TemperatureTypes.Hourly);

            Models.Noaa.Precipitation liquidPrecipitation = GetPrecipitation(parametersElement,  PrecipitationTypes.Liquid);
            Models.Noaa.Precipitation icePrecipitation = GetPrecipitation(parametersElement,  PrecipitationTypes.Ice);
            Models.Noaa.Precipitation snowPrecipitation = GetPrecipitation(parametersElement,  PrecipitationTypes.Snow);




            //// temperature
            //XElement maxTemperatureElement = parametersElement.Elements("temperature").Where(x => x.Attribute("type").Value == "maximum").SingleOrDefault();
            //XElement minTemperatureElement = parametersElement.Elements("temperature").Where(x => x.Attribute("type").Value == "maximum").SingleOrDefault();
            //XElement hourlyTemperatureElement = parametersElement.Elements("temperature").Where(x => x.Attribute("type").Value == "hourly").SingleOrDefault();
            //XElement dewpointTemperatureElement = parametersElement.Elements("temperature").Where(x => x.Attribute("type").Value == "dewpoint").SingleOrDefault();
            //XElement apparentTemperatureElement = parametersElement.Elements("temperature").Where(x => x.Attribute("type").Value == "apparent").SingleOrDefault();

            //// precipitation
            //XElement liquidPrecipitationElement = parametersElement.Elements("precipitation").Where(x => x.Attribute("type").Value == "liquid").SingleOrDefault();
            //XElement icePrecipitationElement = parametersElement.Elements("precipitation").Where(x => x.Attribute("type").Value == "ice").SingleOrDefault();
            //XElement snowPrecipitationElement = parametersElement.Elements("precipitation").Where(x => x.Attribute("type").Value == "snow").SingleOrDefault();
            //XElement probably12HourPrecipitationElement = parametersElement.Elements("probability-of-precipitation").Where(x => x.Attribute("type").Value == "12 hour").SingleOrDefault();


            //// wind
            //XElement windSpeedSustainedKnotsElement = parametersElement.Elements("wind-speed").Where(x => x.Attribute("type").Value == "sustained").SingleOrDefault();
            //XElement windSpeedDirectionElement = parametersElement.Elements("direction").Where(x => x.Attribute("type").Value == "wind").SingleOrDefault();

            //// cloud
            //XElement cloudCoverElement = parametersElement.Elements("cloud-amount").Where(x => x.Attribute("type").Value == "total").SingleOrDefault();

            //// fire
            //XElement fireWeatherfromWindAndHumidityElement = parametersElement.Elements("fire-weather").Where(x => x.Attribute("type").Value == "risk from wind and relative humidity").SingleOrDefault();
            //XElement fireWeatherfromDryThunderstormsElement = parametersElement.Elements("fire-weather").Where(x => x.Attribute("type").Value == "risk from dry thunderstorms").SingleOrDefault();

            //// hazards
            //XElement connectiveHazordsElement = parametersElement.Elements("convective-hazard").Where(x => x.Attribute("type").Value == "risk from dry thunderstorms").SingleOrDefault();


            return null;

        }

        #region Parsing Methods

        private Models.Noaa.Temperature GetTemperature(XElement parametersElement, TemperatureTypes temperatureType)
        {
            if (parametersElement == null) return null;
            string temperatureTypeValue;

            switch (temperatureType)
            {
                case TemperatureTypes.Dewpoint:
                    temperatureTypeValue = "dew point";
                    break;
                default:
                    temperatureTypeValue = temperatureType.ToString().ToLower();
                    break;
            }

            XElement temperatureElement = parametersElement.Elements("temperature").Where(x => x.Attribute("type").Value == temperatureTypeValue).SingleOrDefault();
            if (temperatureElement == null) return null;

            TimeLayout timeLayout = timeLayouts.SingleOrDefault(x => x.key == temperatureElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = temperatureElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            Models.Noaa.Temperature temperature = new Models.Noaa.Temperature()
            {
                Title = temperatureElement.Element("name").Value,
                Unit = (Models.Noaa.TemperatureUnits)Enum.Parse(typeof(Models.Noaa.TemperatureUnits), temperatureElement.Attribute("units").Value, true),
                Values = new List<Models.Noaa.TemperatureValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                temperature.Values.Add(new Models.Noaa.TemperatureValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return temperature;
        }

        private Models.Noaa.Precipitation GetPrecipitation(XElement parametersElement, PrecipitationTypes precipitationTypes)
        {
            if (parametersElement == null) return null;
            string precipitationTypeValue;

            switch (precipitationTypes)
            {
                default:
                    precipitationTypeValue = precipitationTypes.ToString().ToLower();
                    break;
            }

            XElement precipitationElement = parametersElement.Elements("precipitation").Where(x => x.Attribute("type").Value == precipitationTypeValue).SingleOrDefault();
            if (precipitationElement == null) return null;

            TimeLayout timeLayout = timeLayouts.SingleOrDefault(x => x.key == precipitationElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = precipitationElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            Models.Noaa.Precipitation precipitation = new Models.Noaa.Precipitation()
            {
                Title = precipitationElement.Element("name").Value,
                Unit = (Models.Noaa.PrecipitationUnits)Enum.Parse(typeof(Models.Noaa.PrecipitationUnits), precipitationElement.Attribute("units").Value, true),
                Values = new List<Models.Noaa.PrecipitationValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                precipitation.Values.Add(new Models.Noaa.PrecipitationValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return precipitation;
        }


        #endregion
    }


}
