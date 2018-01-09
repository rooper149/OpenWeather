using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace OpenWeather.Noaa.Models
{
    public class DwmlData
    {
        #region Properties

        public Temperature TemperatureMaximum { get; private set; }
        public Temperature TemperatureMinimum { get; private set; }
        public Temperature TemperatureApparent { get; private set; }
        public Temperature TemperatureDewpoint { get; private set; }
        public Temperature TemperatureHourly { get; private set; }

        public Precipitation PrecipitationLiquid { get; private set; }
        public Precipitation PrecipitationIce { get; private set; }
        public Precipitation PrecipitationSnow { get; private set; }

        public ProbabilityOfPrecipitation PrecipitationProbabilityOf12Hour { get; private set; }

        public WindSpeed WindSpeedGust { get; private set; }
        public WindSpeed WindSpeedSustained { get; private set; }
        public WindSpeed WindSpeedIncremental34 { get; private set; }
        public WindSpeed WindSpeedIncremental50 { get; private set; }
        public WindSpeed WindSpeedIncremental64 { get; private set; }
        public WindSpeed WindSpeedCumulative34 { get; private set; }
        public WindSpeed WindSpeedCumulative50 { get; private set; }
        public WindSpeed WindSpeedCumulative64 { get; private set; }

        public WindDirection WindDirection { get; private set; }
        public CloudAmount CloudAmount { get; private set; }

        public Humidity HumidityReleative { get; private set; }
        public Humidity HumidityMaximumReleative { get; private set; }
        public Humidity HumidityMinimumReleative { get; private set; }

        public ConvectiveHazard ConvectiveHazardOutlook { get; private set; }
        public ConvectiveHazard ConvectiveHazardTornadoes { get; private set; }
        public ConvectiveHazard ConvectiveHazardHail { get; private set; }
        public ConvectiveHazard ConvectiveHazardDamagingThunderstormWinds { get; private set; }
        public ConvectiveHazard ConvectiveHazardExtremeTornadoes { get; private set; }
        public ConvectiveHazard ConvectiveHazardExtremeHail { get; private set; }
        public ConvectiveHazard ConvectiveHazardExtremeThunderstormWinds { get; private set; }
        public ConvectiveHazard ConvectiveHazardSevereThunderstorms { get; private set; }
        public ConvectiveHazard ConvectiveHazardExtremeSevereThunderstorms { get; private set; }

        public FireWeather FireWeatherRiskFromWindAndRelativeHumidity { get; private set; }
        public FireWeather FireWeatherRiskFromDryThunderstorms { get; private set; }

        public ClimateAnomaly ClimateAnomalyWeeklyTemperatureAboveNormal { get; private set; }
        public ClimateAnomaly ClimateAnomalyWeeklyTemperatureBelowNormal { get; private set; }
        public ClimateAnomaly ClimateAnomalyMonthlyTemperatureAboveNormal { get; private set; }
        public ClimateAnomaly ClimateAnomalyMonthlyTemperatureBelowNormal { get; private set; }
        public ClimateAnomaly ClimateAnomalySeasonalTemperatureAboveNormal { get; private set; }
        public ClimateAnomaly ClimateAnomalySeasonalTemperatureBelowNormal { get; private set; }
        public ClimateAnomaly ClimateAnomalyWeeklyPrecipitationAboveNormal { get; private set; }
        public ClimateAnomaly ClimateAnomalyWeeklyPrecipitationBelowNormal { get; private set; }
        public ClimateAnomaly ClimateAnomalyMonthlyPrecipitationAboveNormal { get; private set; }
        public ClimateAnomaly ClimateAnomalyMonthlyPrecipitationBelowNormal { get; private set; }
        public ClimateAnomaly ClimateAnomalySeasonalPrecipitationAboveNormal { get; private set; }
        public ClimateAnomaly ClimateAnomalySeasonalPrecipitationBelowNormal { get; private set; }

        public Weather Weather { get; private set; }
        public ConditionIcons ConditionIcons { get; private set; }

        public DateTime EarliestDateTime { get; private set; }
        public DateTime LatestDateTime { get; private set; }


        #endregion Properties

        #region Methods

        private XElement _parametersElement = null;

        List<TimeLayout> _timeLayouts = new List<TimeLayout>();

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

                foreach (XElement sdtElement in element.Elements("start-valid-time")) startDateTimes.Add(sdtElement.ValueIfExists().ToDateTime());
                foreach (XElement edtElement in element.Elements("end-valid-time")) endDateTimes.Add(edtElement.ValueIfExists().ToDateTime());
                
                for (int i = 0; i < startDateTimes.Count; i++)
                {
                    DateTime startDateTime = startDateTimes[i];
                    DateTime endDateTime = startDateTimes[i];
                    if (endDateTimes.Count - 1 >= i) endDateTime = endDateTimes[i];

                    times.Add(new LayoutTimes() { startDateTime = startDateTime, endDateTime = endDateTime });
                }
            }
        }

        public DwmlData(XElement dataElement)
        {

            if (dataElement == null) return;

            IEnumerable<XElement> timeLayoutElements = dataElement.Elements("time-layout");
            foreach (var item in timeLayoutElements) _timeLayouts.Add(new TimeLayout(item));

            EarliestDateTime = _timeLayouts.SelectMany(x => x.times).OrderBy(x => x.startDateTime).Select(x => x.startDateTime).FirstOrDefault();
            LatestDateTime = _timeLayouts.SelectMany(x => x.times).OrderByDescending(x => x.endDateTime).Select(x => x.endDateTime).FirstOrDefault();

            _parametersElement = dataElement.Element("parameters");

            TemperatureMaximum = GetTemperature("maximum");
            TemperatureMinimum = GetTemperature("minimum");
            TemperatureApparent = GetTemperature("apparent");
            TemperatureDewpoint = GetTemperature("dew point");
            TemperatureHourly = GetTemperature("hourly");

            PrecipitationLiquid = GetPrecipitation("liquid");
            PrecipitationIce = GetPrecipitation("ice");
            PrecipitationSnow = GetPrecipitation("snow");

            PrecipitationProbabilityOf12Hour = GetProbabilityOfPrecipitation("12 hour");

            WindSpeedGust = GetWindSpeed("gust");
            WindSpeedSustained = GetWindSpeed("sustained");
            WindSpeedIncremental34 = GetWindSpeed("incremental34");
            WindSpeedIncremental50 = GetWindSpeed("incremental50");
            WindSpeedIncremental64 = GetWindSpeed("incremental64");
            WindSpeedCumulative34 = GetWindSpeed("cumulative34");
            WindSpeedCumulative50 = GetWindSpeed("cumulative50");
            WindSpeedCumulative64 = GetWindSpeed("cumulative64");

            WindDirection = GetWindDirection();
            CloudAmount = GetCloudAmount();

            HumidityReleative = GetHumidity("relative");
            HumidityMaximumReleative = GetHumidity("maximum relative");
            HumidityMinimumReleative = GetHumidity("minimum relative");

            ConvectiveHazardOutlook = GetConvectiveHazardOutlook();
            ConvectiveHazardTornadoes = GetConvectiveHazard("tornadoes");
            ConvectiveHazardHail = GetConvectiveHazard("hail");
            ConvectiveHazardDamagingThunderstormWinds = GetConvectiveHazard("damaging thunderstorm winds");
            ConvectiveHazardExtremeTornadoes = GetConvectiveHazard("extreme tornadoes");
            ConvectiveHazardExtremeHail = GetConvectiveHazard("extreme hail");
            ConvectiveHazardExtremeThunderstormWinds = GetConvectiveHazard("extreme thunderstorm winds");
            ConvectiveHazardSevereThunderstorms = GetConvectiveHazard("severe thunderstorms");
            ConvectiveHazardExtremeSevereThunderstorms = GetConvectiveHazard("extreme severe thunderstorms");

            FireWeatherRiskFromWindAndRelativeHumidity = GetFireWeather("risk from wind and relative humidity");
            FireWeatherRiskFromDryThunderstorms = GetFireWeather("risk from dry thunderstorms");

            ClimateAnomalyWeeklyTemperatureAboveNormal = GetClimateAnomaly("average temperature above normal", "weekly");
            ClimateAnomalyWeeklyTemperatureBelowNormal = GetClimateAnomaly("average temperature below normal", "weekly");
            ClimateAnomalyMonthlyTemperatureAboveNormal = GetClimateAnomaly("average temperature below normal", "monthly");
            ClimateAnomalyMonthlyTemperatureBelowNormal = GetClimateAnomaly("average temperature below normal", "monthly");
            ClimateAnomalySeasonalTemperatureAboveNormal = GetClimateAnomaly("average temperature below normal", "seasonal");
            ClimateAnomalySeasonalTemperatureBelowNormal = GetClimateAnomaly("average temperature below normal", "seasonal");
            ClimateAnomalyWeeklyPrecipitationAboveNormal = GetClimateAnomaly("average precipitation above normal", "weekly");
            ClimateAnomalyWeeklyPrecipitationBelowNormal = GetClimateAnomaly("average precipitation below normal", "weekly");
            ClimateAnomalyMonthlyPrecipitationAboveNormal = GetClimateAnomaly("average precipitation below normal", "monthly");
            ClimateAnomalyMonthlyPrecipitationBelowNormal = GetClimateAnomaly("average precipitation below normal", "monthly");
            ClimateAnomalySeasonalPrecipitationAboveNormal = GetClimateAnomaly("average precipitation below normal", "seasonal");
            ClimateAnomalySeasonalPrecipitationBelowNormal = GetClimateAnomaly("average precipitation below normal", "seasonal");


            Weather = GetWeather();
            ConditionIcons = GetConditionsIcons("forecast-NWS");
        }


        #endregion

        #region Parsing Methods

        private Temperature GetTemperature(string temperatureType)
        {
            if (String.IsNullOrWhiteSpace(temperatureType)) return null;

            XElement temperatureElement = _parametersElement.Elements("temperature").Where(x => x.Attribute("type").Value == temperatureType).SingleOrDefault();
            if (temperatureElement == null) return null;

            TimeLayout timeLayout = _timeLayouts.SingleOrDefault(x => x.key == temperatureElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = temperatureElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            Temperature temperature = new Temperature()
            {
                Title = temperatureElement.Element("name").Value,
                Unit = (TemperatureUnits)Enum.Parse(typeof(TemperatureUnits), temperatureElement.Attribute("units").Value, true),
                Values = new List<TemperatureValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                temperature.Values.Add(new TemperatureValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return temperature;
        }

        private Precipitation GetPrecipitation(string precipitationType)
        {
            if (String.IsNullOrWhiteSpace(precipitationType)) return null;

            XElement precipitationElement = _parametersElement.Elements("precipitation").Where(x => x.Attribute("type").Value == precipitationType).SingleOrDefault();
            if (precipitationElement == null) return null;

            TimeLayout timeLayout = _timeLayouts.SingleOrDefault(x => x.key == precipitationElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = precipitationElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            Precipitation precipitation = new Precipitation()
            {
                Title = precipitationElement.Element("name").Value,
                Unit = (PrecipitationUnits)Enum.Parse(typeof(PrecipitationUnits), precipitationElement.Attribute("units").Value, true),
                Values = new List<PrecipitationValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                precipitation.Values.Add(new PrecipitationValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return precipitation;
        }

        private WindSpeed GetWindSpeed(string windSpeedType)
        {
            if (String.IsNullOrWhiteSpace(windSpeedType)) return null;

            XElement windSpeedElement = _parametersElement.Elements("wind-speed").Where(x => x.Attribute("type").Value == windSpeedType).SingleOrDefault();
            if (windSpeedElement == null) return null;

            TimeLayout timeLayout = _timeLayouts.SingleOrDefault(x => x.key == windSpeedElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = windSpeedElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            WindSpeed windSpeed = new WindSpeed()
            {
                Title = windSpeedElement.Element("name").Value,
                Unit = (WindSpeedUnits)Enum.Parse(typeof(WindSpeedUnits), windSpeedElement.Attribute("units").Value, true),
                Values = new List<WindSpeedValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                windSpeed.Values.Add(new WindSpeedValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return windSpeed;
        }

        private Humidity GetHumidity(string humidityType)
        {
            if (String.IsNullOrWhiteSpace(humidityType)) return null;

            XElement humidityElement = _parametersElement.Elements("humidity").Where(x => x.Attribute("type").Value == humidityType).SingleOrDefault();
            if (humidityElement == null) return null;

            TimeLayout timeLayout = _timeLayouts.SingleOrDefault(x => x.key == humidityElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = humidityElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            Humidity humidity = new Humidity()
            {
                Title = humidityElement.Element("name").Value,
                Unit = (HumidityUnits)Enum.Parse(typeof(HumidityUnits), humidityElement.Attribute("units").Value, true),
                Values = new List<HumidityValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                humidity.Values.Add(new HumidityValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return humidity;
        }

        private ConvectiveHazard GetConvectiveHazardOutlook()
        {
            XElement convectiveHazardElement = _parametersElement.Elements("convective-hazard").Select(x => x.Element("outlook")).Where(x => x != null).SingleOrDefault();
            if (convectiveHazardElement == null) return null;

            TimeLayout timeLayout = _timeLayouts.SingleOrDefault(x => x.key == convectiveHazardElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = convectiveHazardElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            ConvectiveHazard convectiveHazard = new ConvectiveHazard()
            {
                Title = convectiveHazardElement.Element("name").Value,
                Unit = ConvectiveHazardUnits.None,
                Values = new List<ConvectiveHazardValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                convectiveHazard.Values.Add(new ConvectiveHazardValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return convectiveHazard;

        }

        private ConvectiveHazard GetConvectiveHazard(string convectionHazardType)
        {
            if (String.IsNullOrWhiteSpace(convectionHazardType)) return null;

            IEnumerable<XElement> convectiveHazardElement = convectiveHazardElement = _parametersElement.Elements("convective-hazard").Select(x => x.Element("severe-component"));
            if (convectiveHazardElement == null) return null;

            XElement severeComponentElement = convectiveHazardElement.Where(x => x != null && x.Attribute("type").Value == convectionHazardType).SingleOrDefault();
            if (severeComponentElement == null) return null;

            TimeLayout timeLayout = _timeLayouts.SingleOrDefault(x => x.key == severeComponentElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = severeComponentElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            ConvectiveHazard convectiveHazard = new ConvectiveHazard()
            {
                Title = severeComponentElement.Element("name").Value,
                Unit = (ConvectiveHazardUnits)Enum.Parse(typeof(ConvectiveHazardUnits), severeComponentElement.Attribute("units").Value, true),
                Values = new List<ConvectiveHazardValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                convectiveHazard.Values.Add(new ConvectiveHazardValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return convectiveHazard;

        }

        private WindDirection GetWindDirection()
        {
            XElement windDirectionElement = _parametersElement.Elements("direction").Where(x => x.Attribute("type").Value == "wind").SingleOrDefault();
            if (windDirectionElement == null) return null;

            TimeLayout timeLayout = _timeLayouts.SingleOrDefault(x => x.key == windDirectionElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = windDirectionElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            WindDirection windDirection = new WindDirection()
            {
                Title = windDirectionElement.Element("name").Value,
                Units = WindDirectionUnits.DegreesTrueNorth,
                Values = new List<WindDirectionValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                windDirection.Values.Add(new WindDirectionValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return windDirection;
        }

        private CloudAmount GetCloudAmount()
        {
            XElement cloudAmountElement = _parametersElement.Elements("cloud-amount").Where(x => x.Attribute("type").Value == "total").SingleOrDefault();
            if (cloudAmountElement == null) return null;

            TimeLayout timeLayout = _timeLayouts.SingleOrDefault(x => x.key == cloudAmountElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = cloudAmountElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            CloudAmount cloudAmount = new CloudAmount()
            {
                Title = cloudAmountElement.Element("name").Value,
                Unit = CloudAmountUnits.Percent,
                Values = new List<CloudAmountValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                cloudAmount.Values.Add(new CloudAmountValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return cloudAmount;
        }

        private ProbabilityOfPrecipitation GetProbabilityOfPrecipitation(string probabilityOfPrecipitationType)
        {
            if (String.IsNullOrWhiteSpace(probabilityOfPrecipitationType)) return null;

            XElement probabilityOfPrecipitationElement = _parametersElement.Elements("probability-of-precipitation").Where(x => x.Attribute("type").Value == probabilityOfPrecipitationType).SingleOrDefault();
            if (probabilityOfPrecipitationElement == null) return null;

            TimeLayout timeLayout = _timeLayouts.SingleOrDefault(x => x.key == probabilityOfPrecipitationElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = probabilityOfPrecipitationElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            ProbabilityOfPrecipitation probabilityOfPrecipitation = new ProbabilityOfPrecipitation()
            {
                Title = probabilityOfPrecipitationElement.Element("name").Value,
                Unit = (ProbabilityOfPrecipitationUnits)Enum.Parse(typeof(ProbabilityOfPrecipitationUnits), probabilityOfPrecipitationElement.Attribute("units").Value, true),
                Values = new List<ProbabilityOfPrecipitationValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                probabilityOfPrecipitation.Values.Add(new ProbabilityOfPrecipitationValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return probabilityOfPrecipitation;
        }

        private FireWeather GetFireWeather(string fileWeatherType)
        {
            if (String.IsNullOrWhiteSpace(fileWeatherType)) return null;

            XElement fireWeatherElement = _parametersElement.Elements("fire-weather").Where(x => x.Attribute("type").Value == fileWeatherType).SingleOrDefault();
            if (fireWeatherElement == null) return null;

            TimeLayout timeLayout = _timeLayouts.SingleOrDefault(x => x.key == fireWeatherElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = fireWeatherElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            FireWeather fireWeather = new FireWeather()
            {
                Title = fireWeatherElement.Element("name").Value,
                Unit = FireWeatherUnits.None,
                Values = new List<FireWeatherValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                fireWeather.Values.Add(new FireWeatherValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists()
                });
            }

            return fireWeather;
        }

        private ClimateAnomaly GetClimateAnomaly(string climateAnomalyType, string climateAnomalyTimespan)
        {
            if (String.IsNullOrWhiteSpace(climateAnomalyType)) return null;
            if (String.IsNullOrWhiteSpace(climateAnomalyTimespan)) return null;

            XElement climateAnomalyElement = _parametersElement.Elements("climate-anomaly")
                .Select(x => x.Element(climateAnomalyTimespan))
                .Where(x => x != null)
                .Where(a => a.Attribute("type").Value == climateAnomalyType).SingleOrDefault();
            if (climateAnomalyElement == null) return null;

            TimeLayout timeLayout = _timeLayouts.SingleOrDefault(x => x.key == climateAnomalyElement.Attribute("time-layout").Value);

            IEnumerable<XElement> valueElements = climateAnomalyElement.Elements("value");
            if (valueElements == null || valueElements.Count() == 0) return null;

            ClimateAnomaly climateAnomaly = new ClimateAnomaly()
            {
                Title = climateAnomalyElement.Element("name").Value,
                Unit = (ClimateAnomalyUnits)Enum.Parse(typeof(ClimateAnomalyUnits), climateAnomalyElement.Attribute("units").Value, true),
                Values = new List<ClimateAnomalyValue>()
            };

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                climateAnomaly.Values.Add(new ClimateAnomalyValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToDouble()
                });
            }

            return climateAnomaly;
        }

        private Weather GetWeather()
        {
            XElement weatherElement = _parametersElement.Element("weather");
            if (weatherElement == null) return null;

            TimeLayout timeLayout = _timeLayouts.SingleOrDefault(x => x.key == weatherElement.Attribute("time-layout").Value);

            Weather weather = new Weather()
            {
                Title = weatherElement.Element("name").Value,
                Conditions = new List<WeatherConditions>()
            };

            IEnumerable<XElement> weatherConditionsElements = weatherElement.Elements("weather-conditions");
            if (weatherConditionsElements == null) return null;

            List<XElement> weatherConditionsElementList = weatherConditionsElements.ToList();
            for (int i = 0; i < weatherConditionsElementList.Count; i++)
            {
                List<WeatherConditionsValue> values = new List<WeatherConditionsValue>();
                IEnumerable<XElement> valueElements = weatherConditionsElementList[i].Elements("value");
                if (valueElements == null) continue;
                List<XElement> valueElementList = valueElements.ToList();
                for (int j = 0; j < valueElementList.Count; j++)
                {
                    values.Add(new WeatherConditionsValue()
                    {
                        Coverage = valueElementList[j].Attribute("coverage").ValueIfExists(),
                        Intensity = valueElementList[j].Attribute("intensity").ValueIfExists(),
                        WeatherType = valueElementList[j].Attribute("weather-type").ValueIfExists(),
                        Qualifier = valueElementList[j].Attribute("qualifier").ValueIfExists(),
                        Additive = valueElementList[j].Attribute("additive").ValueIfExists()
                    });
                }

                weather.Conditions.Add(new WeatherConditions()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Values = values
                });

            }

            return weather;

        }

        private ConditionIcons GetConditionsIcons(string iconType)
        {
            XElement conditionsIconElement = _parametersElement.Elements("conditions-icon").Where(x => x.Attribute("type").Value == iconType).SingleOrDefault();
            if (conditionsIconElement == null) return null;

            TimeLayout timeLayout = _timeLayouts.SingleOrDefault(x => x.key == conditionsIconElement.Attribute("time-layout").Value);

            ConditionIcons conditionIcon = new ConditionIcons()
            {
                Title = conditionsIconElement.Element("name").Value,
                Values = new List<ConditionIconValue>()
            };


            IEnumerable<XElement> valueElements = conditionsIconElement.Elements("icon-link");
            if (valueElements == null || valueElements.Count() == 0) return null;

            List<XElement> valueElementsList = valueElements.ToList();
            for (int i = 0; i < valueElementsList.Count; i++)
            {
                conditionIcon.Values.Add(new ConditionIconValue()
                {
                    StartDateTime = timeLayout.times[i].startDateTime,
                    EndDateTime = timeLayout.times[i].endDateTime,
                    Value = valueElementsList[i].ValueIfExists().ToUri()
                });
            }

            return conditionIcon;
        }

        #endregion

    }
}
