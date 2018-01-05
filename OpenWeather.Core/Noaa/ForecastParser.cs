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

        private enum WindSpeedTypes
        {
            Gust,
            Sustained,
            Incremental34,
            Incremental50,
            Incremental64,
            Cumulative34,
            Cumulative50,
            Cumulative64
        }

        private enum HumidityTypes
        {
            Relative,
            MaximumRelative,
            MinimumRelative
        }

        private enum ConvectiveHazardTypes
        {
            Tornadoes,
            Hail,
            DamagingThunderstormWinds,
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

            // TODO: Rewrite the get methods to not use enums, but to use strings.
            // TODO: Add cevective-hazord=>outlook from xml.
            // Parameters (forcasts)
            XElement parametersElement = dataElement.Element("parameters");
            Models.Noaa.Temperature maxTemperature = GetTemperature(parametersElement, TemperatureTypes.Maximum);
            Models.Noaa.Temperature minTemperature = GetTemperature(parametersElement, TemperatureTypes.Minimum);
            Models.Noaa.Temperature apparentTemperature = GetTemperature(parametersElement, TemperatureTypes.Apparent);
            Models.Noaa.Temperature dewpointTemperature = GetTemperature(parametersElement, TemperatureTypes.Dewpoint);
            Models.Noaa.Temperature hourlyTemperature = GetTemperature(parametersElement, TemperatureTypes.Hourly);

            Models.Noaa.Precipitation liquidPrecipitation = GetPrecipitation(parametersElement, PrecipitationTypes.Liquid);
            Models.Noaa.Precipitation icePrecipitation = GetPrecipitation(parametersElement, PrecipitationTypes.Ice);
            Models.Noaa.Precipitation snowPrecipitation = GetPrecipitation(parametersElement, PrecipitationTypes.Snow);

            Models.Noaa.WindSpeed gustWindSpeed = GetWindSpeed(parametersElement, WindSpeedTypes.Gust);
            Models.Noaa.WindSpeed sustainedWindSpeed = GetWindSpeed(parametersElement, WindSpeedTypes.Sustained);
            Models.Noaa.WindSpeed incremental34WindSpeed = GetWindSpeed(parametersElement, WindSpeedTypes.Incremental34);
            Models.Noaa.WindSpeed incremental50WindSpeed = GetWindSpeed(parametersElement, WindSpeedTypes.Incremental50);
            Models.Noaa.WindSpeed incremental64WindSpeed = GetWindSpeed(parametersElement, WindSpeedTypes.Incremental64);
            Models.Noaa.WindSpeed cumulative34WindSpeed = GetWindSpeed(parametersElement, WindSpeedTypes.Cumulative34);
            Models.Noaa.WindSpeed cumulative50WindSpeed = GetWindSpeed(parametersElement, WindSpeedTypes.Cumulative50);
            Models.Noaa.WindSpeed cumulative64WindSpeed = GetWindSpeed(parametersElement, WindSpeedTypes.Cumulative64);

            Models.Noaa.Humidity releativeHumidity = GetHumidity(parametersElement, HumidityTypes.Relative);
            Models.Noaa.Humidity maxReleativeHumidity = GetHumidity(parametersElement, HumidityTypes.MaximumRelative);
            Models.Noaa.Humidity minRleativeHumidity = GetHumidity(parametersElement, HumidityTypes.MinimumRelative);

            Models.Noaa.ConvectiveHazard tornadoesConvectiveHazard = GetConvectiveHazard(parametersElement, "tornadoes");
            Models.Noaa.ConvectiveHazard hailConvectiveHazard = GetConvectiveHazard(parametersElement, "hail");
            Models.Noaa.ConvectiveHazard damagingThunderstormWindsConvectiveHazard = GetConvectiveHazard(parametersElement, "damaging thunderstorm winds");
            Models.Noaa.ConvectiveHazard extremeTornadoesConvectiveHazard = GetConvectiveHazard(parametersElement, "extreme tornadoes");
            Models.Noaa.ConvectiveHazard extremeHailConvectiveHazard = GetConvectiveHazard(parametersElement, "extreme hail");
            Models.Noaa.ConvectiveHazard extremeThunderstormWindsConvectiveHazard = GetConvectiveHazard(parametersElement, "extreme thunderstorm winds");
            Models.Noaa.ConvectiveHazard severeThunderstormsConvectiveHazard = GetConvectiveHazard(parametersElement, "severe thunderstorms");
            Models.Noaa.ConvectiveHazard extremeSevereThunderstormsConvectiveHazard = GetConvectiveHazard(parametersElement, "extreme severe thunderstorms");


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

        private Models.Noaa.Precipitation GetPrecipitation(XElement parametersElement, PrecipitationTypes precipitationType)
        {
            if (parametersElement == null) return null;
            string precipitationTypeValue;

            switch (precipitationType)
            {
                default:
                    precipitationTypeValue = precipitationType.ToString().ToLower();
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

        private Models.Noaa.WindSpeed GetWindSpeed(XElement parametersElement, WindSpeedTypes windSpeedType)
        {
            if (parametersElement == null) return null;
            string windSpeedTypeValue;

            switch (windSpeedType)
            {
                default:
                    windSpeedTypeValue = windSpeedType.ToString().ToLower();
                    break;
            }

            XElement windSpeedElement = parametersElement.Elements("wind-speed").Where(x => x.Attribute("type").Value == windSpeedTypeValue).SingleOrDefault();
            if (windSpeedElement == null) return null;

            TimeLayout timeLayout = timeLayouts.SingleOrDefault(x => x.key == windSpeedElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = windSpeedElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            Models.Noaa.WindSpeed windSpeed = new Models.Noaa.WindSpeed()
            {
                Title = windSpeedElement.Element("name").Value,
                Unit = (Models.Noaa.WindSpeedUnits)Enum.Parse(typeof(Models.Noaa.WindSpeedUnits), windSpeedElement.Attribute("units").Value, true),
                Values = new List<Models.Noaa.WindSpeedValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                windSpeed.Values.Add(new Models.Noaa.WindSpeedValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return windSpeed;
        }

        private Models.Noaa.Humidity GetHumidity(XElement parametersElement, HumidityTypes humidityType)
        {
            if (parametersElement == null) return null;
            string humidityTypeTypeValue;

            switch (humidityType)
            {
                case HumidityTypes.MaximumRelative:
                    humidityTypeTypeValue = "maximum relative";
                    break;
                case HumidityTypes.MinimumRelative:
                    humidityTypeTypeValue = "minimum relative";
                    break;
                default:
                    humidityTypeTypeValue = humidityType.ToString().ToLower();
                    break;
            }

            XElement humidityElement = parametersElement.Elements("humidity").Where(x => x.Attribute("type").Value == humidityTypeTypeValue).SingleOrDefault();
            if (humidityElement == null) return null;

            TimeLayout timeLayout = timeLayouts.SingleOrDefault(x => x.key == humidityElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = humidityElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            Models.Noaa.Humidity humidity = new Models.Noaa.Humidity()
            {
                Title = humidityElement.Element("name").Value,
                Unit = (Models.Noaa.HumidityUnits)Enum.Parse(typeof(Models.Noaa.HumidityUnits), humidityElement.Attribute("units").Value, true),
                Values = new List<Models.Noaa.HumidityValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                humidity.Values.Add(new Models.Noaa.HumidityValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return humidity;
        }

        private Models.Noaa.ConvectiveHazard GetConvectiveHazard(XElement parametersElement, string convectionHazardType)
        {
            if (parametersElement == null) return null;
            
            IEnumerable<XElement> convectiveHazardElement = convectiveHazardElement = parametersElement.Elements("convective-hazard").Select(x => x.Element("severe-component"));
            if (convectiveHazardElement == null) return null;
            
            XElement severeComponentElement = convectiveHazardElement.Where(x => x != null && x.Attribute("type").Value == convectionHazardType).SingleOrDefault();
            if (severeComponentElement == null) return null;

            TimeLayout timeLayout = timeLayouts.SingleOrDefault(x => x.key == severeComponentElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = severeComponentElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            Models.Noaa.ConvectiveHazard convectiveHazard = new Models.Noaa.ConvectiveHazard()
            {
                Title = severeComponentElement.Element("name").Value,
                Unit = (Models.Noaa.ConvectiveHazardUnits)Enum.Parse(typeof(Models.Noaa.ConvectiveHazardUnits), severeComponentElement.Attribute("units").Value, true),
                Values = new List<Models.Noaa.ConvectiveHazardValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                convectiveHazard.Values.Add(new Models.Noaa.ConvectiveHazardValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return convectiveHazard;

        }


        #endregion
    }


}
