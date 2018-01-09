using OpenWeather.Noaa.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenWeather.Noaa
{
    internal class ForecastParser
    {
        internal Forecast ParseForecastResult(string result)
        {
            Dwml dwml = new Dwml(result);
            Forecast forecast = new Forecast();

            IEnumerable<TemperatureValue> apparentTemperatureValues = dwml.Data.TemperatureApparent?.Values;
            IEnumerable<TemperatureValue> dewpointTemperatureValues = dwml.Data.TemperatureDewpoint?.Values;
            IEnumerable<TemperatureValue> hourlyTemperatureValues = dwml.Data.TemperatureHourly?.Values;
            IEnumerable<TemperatureValue> maximumTemperatureValues = dwml.Data.TemperatureMaximum?.Values;
            IEnumerable<TemperatureValue> minimumTemperatureValues = dwml.Data.TemperatureMinimum?.Values;

            IEnumerable<PrecipitationValue> precipitationIceValues = dwml.Data.PrecipitationIce?.Values;
            IEnumerable<PrecipitationValue> precipitationLiquidValues = dwml.Data.PrecipitationLiquid?.Values;
            IEnumerable<PrecipitationValue> precipitationSnowValues = dwml.Data.PrecipitationSnow?.Values;
            IEnumerable<ProbabilityOfPrecipitationValue> precipitation12HourProbabilityValues = dwml.Data.PrecipitationProbabilityOf12Hour?.Values;

            IEnumerable<WindSpeedValue> windSpeedGustValues = dwml.Data.WindSpeedGust?.Values;
            IEnumerable<WindSpeedValue> windSpeedSustainedValues = dwml.Data.WindSpeedGust?.Values;
            IEnumerable<WindSpeedValue> windSpeedIncremental34Values = dwml.Data.WindSpeedIncremental34?.Values;
            IEnumerable<WindSpeedValue> windSpeedIncremental50Values = dwml.Data.WindSpeedIncremental50?.Values;
            IEnumerable<WindSpeedValue> windSpeedIncremental64Values = dwml.Data.WindSpeedIncremental64?.Values;
            IEnumerable<WindSpeedValue> windSpeedCumulative34Values = dwml.Data.WindSpeedCumulative34?.Values;
            IEnumerable<WindSpeedValue> windSpeedCumulative50Values = dwml.Data.WindSpeedCumulative50?.Values;
            IEnumerable<WindSpeedValue> windSpeedCumulative64Values = dwml.Data.WindSpeedCumulative64?.Values;

            IEnumerable<HumidityValue> humidityReleativeValues = dwml.Data.HumidityReleative?.Values;
            IEnumerable<HumidityValue> humidityMaximumReleativeValues = dwml.Data.HumidityMaximumReleative?.Values;
            IEnumerable<HumidityValue> humidityMinimumReleativeValues = dwml.Data.HumidityMinimumReleative?.Values;

            IEnumerable<ConvectiveHazardValue> convectiveHazardOutlookValues = dwml.Data.ConvectiveHazardOutlook?.Values;
            IEnumerable<ConvectiveHazardValue> convectiveHazardTornadoesValues = dwml.Data.ConvectiveHazardTornadoes?.Values;
            IEnumerable<ConvectiveHazardValue> convectiveHazardHailValues = dwml.Data.ConvectiveHazardHail?.Values;
            IEnumerable<ConvectiveHazardValue> convectiveHazardDamagingThunderstormWindsValues = dwml.Data.ConvectiveHazardDamagingThunderstormWinds?.Values;
            IEnumerable<ConvectiveHazardValue> convectiveHazardExtremeTornadoesValues = dwml.Data.ConvectiveHazardExtremeTornadoes?.Values;
            IEnumerable<ConvectiveHazardValue> convectiveHazardExtremeHailValues = dwml.Data.ConvectiveHazardExtremeHail?.Values;
            IEnumerable<ConvectiveHazardValue> convectiveHazardExtremeThunderstormWindsValues = dwml.Data.ConvectiveHazardExtremeThunderstormWinds?.Values;
            IEnumerable<ConvectiveHazardValue> convectiveHazardSevereThunderstormValues = dwml.Data.ConvectiveHazardSevereThunderstorms?.Values;
            IEnumerable<ConvectiveHazardValue> convectiveHazardExtremeSevereThunderstormsValues = dwml.Data.ConvectiveHazardExtremeSevereThunderstorms?.Values;

            IEnumerable<FireWeatherValue> fireWeatherRiskFromWindAndRelativeHumidityValues = dwml.Data.FireWeatherRiskFromWindAndRelativeHumidity?.Values;
            IEnumerable<FireWeatherValue> fireWeatherRiskFromDryThunderstormValues = dwml.Data.FireWeatherRiskFromDryThunderstorms?.Values;

            IEnumerable<ClimateAnomalyValue> climateAnomalyWeeklyTemperatureAboveNormalValues = dwml.Data.ClimateAnomalyWeeklyTemperatureAboveNormal?.Values;
            IEnumerable<ClimateAnomalyValue> climateAnomalyWeeklyTemperatureBelowNormalValues = dwml.Data.ClimateAnomalyWeeklyTemperatureBelowNormal?.Values;
            IEnumerable<ClimateAnomalyValue> climateAnomalyMonthlyTemperatureAboveNormalValues = dwml.Data.ClimateAnomalyMonthlyTemperatureAboveNormal?.Values;
            IEnumerable<ClimateAnomalyValue> climateAnomalyMonthlyTemperatureBelowNormalValues = dwml.Data.ClimateAnomalyMonthlyTemperatureBelowNormal?.Values;
            IEnumerable<ClimateAnomalyValue> climateAnomalySeasonalTemperatureAboveNormalValues = dwml.Data.ClimateAnomalySeasonalTemperatureAboveNormal?.Values;
            IEnumerable<ClimateAnomalyValue> climateAnomalySeasonalTemperatureBelowNormalValues = dwml.Data.ClimateAnomalySeasonalTemperatureBelowNormal?.Values;
            IEnumerable<ClimateAnomalyValue> climateAnomalyWeeklyPrecipitationAboveNormalValues = dwml.Data.ClimateAnomalyWeeklyPrecipitationAboveNormal?.Values;
            IEnumerable<ClimateAnomalyValue> climateAnomalyWeeklyPrecipitationBelowNormalValues = dwml.Data.ClimateAnomalyWeeklyPrecipitationBelowNormal?.Values;
            IEnumerable<ClimateAnomalyValue> climateAnomalyMonthlyPrecipitationAboveNormalValues = dwml.Data.ClimateAnomalyMonthlyPrecipitationAboveNormal?.Values;
            IEnumerable<ClimateAnomalyValue> climateAnomalyMonthlyPrecipitationBelowNormalValues = dwml.Data.ClimateAnomalyMonthlyPrecipitationBelowNormal?.Values;
            IEnumerable<ClimateAnomalyValue> climateAnomalySeasonalPrecipitationAboveNormalValues = dwml.Data.ClimateAnomalySeasonalPrecipitationAboveNormal?.Values;
            IEnumerable<ClimateAnomalyValue> climateAnomalySeasonalPrecipitationBelowNormalValues = dwml.Data.ClimateAnomalySeasonalPrecipitationBelowNormal?.Values;


            IEnumerable<WindDirectionValue> windDirectionValues = dwml.Data.WindDirection?.Values;
            IEnumerable<WeatherConditions> weatherConditions = dwml.Data.Weather?.Conditions;
            IEnumerable<ConditionIconValue> conditionIconValues = dwml.Data.ConditionIcons?.Values;
            IEnumerable<CloudAmountValue> cloudAmountValues = dwml.Data.CloudAmount?.Values;


            DateTime currentDateTime = dwml.Data.EarliestDateTime;
            while (currentDateTime <= dwml.Data.LatestDateTime)
            {

                ForecastTimeLine forecastTimeLine = new ForecastTimeLine();
                forecastTimeLine.DateTime = currentDateTime;

                // maximum temperature
                TemperatureValue maximumTemperatureValue = maximumTemperatureValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime >= currentDateTime);
                if (maximumTemperatureValue != null)
                {

                    ForecastValue<double> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.TemperatureMaximum)
                        .Where(x => x?.Value == maximumTemperatureValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<double>() { Value = maximumTemperatureValue.Value, IsStart = maximumTemperatureValue.StartDateTime == currentDateTime };
                        forecastTimeLine.TemperatureMaximum = forecastElement;
                    }

                    forecastElement.IsEnd = maximumTemperatureValue.EndDateTime == currentDateTime;
                }

                // minimum temperature
                TemperatureValue minimumTemperatureValue = minimumTemperatureValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime >= currentDateTime);
                if (minimumTemperatureValue != null)
                {

                    ForecastValue<double> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.TemperatureMinimum)
                        .Where(x => x?.Value == minimumTemperatureValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<double>() { Value = minimumTemperatureValue.Value, IsStart = minimumTemperatureValue.StartDateTime == currentDateTime };
                        forecastTimeLine.TemperatureMinimum = forecastElement;
                    }

                    forecastElement.IsEnd = minimumTemperatureValue.EndDateTime == currentDateTime;
                }

                // hourly temperature
                TemperatureValue hourlyTemperatureValue = hourlyTemperatureValues?.SingleOrDefault(x => x.StartDateTime == currentDateTime);
                if (hourlyTemperatureValue != null)
                {
                    forecastTimeLine.TemperatureHourly = new ForecastValue<double>()
                    {
                        Value = hourlyTemperatureValue.Value,
                        IsStart = true,
                        IsEnd = true
                    };
                }

                // dewpoint temperature
                TemperatureValue dewpointTemperatureValue = dewpointTemperatureValues?.SingleOrDefault(x => x.StartDateTime == currentDateTime);
                if (dewpointTemperatureValue != null)
                {
                    forecastTimeLine.TemperatureDewpoint = new ForecastValue<double>()
                    {
                        Value = dewpointTemperatureValue.Value,
                        IsStart = true,
                        IsEnd = true
                    };
                }

                // apparent temperature
                TemperatureValue apparentTemperatureValue = apparentTemperatureValues?.SingleOrDefault(x => x.StartDateTime == currentDateTime);
                if (apparentTemperatureValue != null)
                {
                    forecastTimeLine.TemperatureApparent = new ForecastValue<double>() { Value = apparentTemperatureValue.Value, IsStart = true, IsEnd = true };
                }

                // ice precipitation
                PrecipitationValue precipitationIceValue = precipitationIceValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (precipitationIceValue != null)
                {
                    ForecastValue<PrecipitationValue> forecastElement = forecast.Timelines
                       .Where(x => x.DateTime <= currentDateTime)
                       .Select(x => x.PrecipitationIce)
                       .Where(x => x?.Value.Value == precipitationIceValue.Value)
                       .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<PrecipitationValue>() { Value = precipitationIceValue, IsStart = precipitationIceValue.StartDateTime == currentDateTime };
                        forecastTimeLine.PrecipitationIce = forecastElement;
                    }

                    forecastElement.IsEnd = precipitationIceValue.EndDateTime == currentDateTime;
                }

                // snow precipitation
                PrecipitationValue precipitationSnowValue = precipitationSnowValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (precipitationSnowValue != null)
                {
                    ForecastValue<PrecipitationValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.PrecipitationSnow)
                        .Where(x => x?.Value.Value == precipitationSnowValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<PrecipitationValue>() { Value = precipitationSnowValue, IsStart = precipitationSnowValue.StartDateTime == currentDateTime };
                        forecastTimeLine.PrecipitationSnow = forecastElement;
                    }

                    forecastElement.IsEnd = precipitationSnowValue.EndDateTime == currentDateTime;
                }

                // liquid precipitation
                PrecipitationValue precipitationLiquidValue = precipitationLiquidValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (precipitationLiquidValue != null)
                {
                    ForecastValue<PrecipitationValue> forecastElement = forecast.Timelines
                       .Where(x => x.DateTime <= currentDateTime)
                       .Select(x => x.PrecipitationLiquid)
                       .Where(x => x?.Value.Value == precipitationLiquidValue.Value)
                       .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<PrecipitationValue>() { Value = precipitationLiquidValue, IsStart = precipitationLiquidValue.StartDateTime == currentDateTime };
                        forecastTimeLine.PrecipitationLiquid = forecastElement;
                    }

                    forecastElement.IsEnd = precipitationLiquidValue.EndDateTime == currentDateTime;
                }

                // probaility of precipitation
                ProbabilityOfPrecipitationValue probabilityOfPrecipitationValue = precipitation12HourProbabilityValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (probabilityOfPrecipitationValue != null)
                {
                    ForecastValue<ProbabilityOfPrecipitationValue> forecastElement = forecast.Timelines
                       .Where(x => x.DateTime <= currentDateTime)
                       .Select(x => x.ProbabilityOfPrecipitation)
                       .Where(x => x?.Value.Value == probabilityOfPrecipitationValue.Value)
                       .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ProbabilityOfPrecipitationValue>() { Value = probabilityOfPrecipitationValue, IsStart = probabilityOfPrecipitationValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ProbabilityOfPrecipitation = forecastElement;
                    }

                    forecastElement.IsEnd = probabilityOfPrecipitationValue.EndDateTime == currentDateTime;
                }

                // wind speed - gusts
                WindSpeedValue windSpeedGustValue = windSpeedGustValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (windSpeedGustValue != null)
                {
                    ForecastValue<WindSpeedValue> forecastElement = forecast.Timelines
                      .Where(x => x.DateTime <= currentDateTime)
                      .Select(x => x.WindSpeedGust)
                      .Where(x => x?.Value.Value == windSpeedGustValue.Value)
                      .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<WindSpeedValue>() { Value = windSpeedGustValue, IsStart = windSpeedGustValue.StartDateTime == currentDateTime };
                        forecastTimeLine.WindSpeedGust = forecastElement;
                    }

                    forecastElement.IsEnd = windSpeedGustValue.EndDateTime == currentDateTime;
                }

                // wind speed - sustained
                WindSpeedValue windSpeedSustainedValue = windSpeedSustainedValues?.SingleOrDefault(x => x.StartDateTime == currentDateTime);
                if (windSpeedSustainedValue != null)
                {
                    forecastTimeLine.WindSpeedSustained = new ForecastValue<double>() { Value = windSpeedSustainedValue.Value, IsStart = true, IsEnd = true };
                }

                // wind speed - incremental34
                WindSpeedValue windSpeedIncremental34Value = windSpeedIncremental34Values?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (windSpeedIncremental34Value != null)
                {
                    ForecastValue<WindSpeedValue> forecastElement = forecast.Timelines
                      .Where(x => x.DateTime <= currentDateTime)
                      .Select(x => x.WindSpeedCumulative34)
                      .Where(x => x?.Value.Value == windSpeedIncremental34Value.Value)
                      .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<WindSpeedValue>() { Value = windSpeedIncremental34Value, IsStart = windSpeedIncremental34Value.StartDateTime == currentDateTime };
                        forecastTimeLine.WindSpeedCumulative34 = forecastElement;
                    }

                    forecastElement.IsEnd = windSpeedIncremental34Value.EndDateTime == currentDateTime;
                }

                // wind speed - incremental50
                WindSpeedValue windSpeedIncremental50Value = windSpeedIncremental50Values?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (windSpeedIncremental50Value != null)
                {
                    ForecastValue<WindSpeedValue> forecastElement = forecast.Timelines
                      .Where(x => x.DateTime <= currentDateTime)
                      .Select(x => x.WindSpeedIncremental50)
                      .Where(x => x?.Value.Value == windSpeedIncremental50Value.Value)
                      .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<WindSpeedValue>() { Value = windSpeedIncremental50Value, IsStart = windSpeedIncremental50Value.StartDateTime == currentDateTime };
                        forecastTimeLine.WindSpeedIncremental50 = forecastElement;
                    }

                    forecastElement.IsEnd = windSpeedIncremental50Value.EndDateTime == currentDateTime;
                }

                // wind speed - incremental64
                WindSpeedValue windSpeedIncremental64Value = windSpeedIncremental64Values?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (windSpeedIncremental64Value != null)
                {
                    ForecastValue<WindSpeedValue> forecastElement = forecast.Timelines
                      .Where(x => x.DateTime <= currentDateTime)
                      .Select(x => x.WindSpeedIncremental64)
                      .Where(x => x?.Value.Value == windSpeedIncremental64Value.Value)
                      .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<WindSpeedValue>() { Value = windSpeedIncremental64Value, IsStart = windSpeedIncremental64Value.StartDateTime == currentDateTime };
                        forecastTimeLine.WindSpeedIncremental64 = forecastElement;
                    }

                    forecastElement.IsEnd = windSpeedIncremental64Value.EndDateTime == currentDateTime;
                }

                // wind speed - cumulative34
                WindSpeedValue windSpeedCumulative34Value = windSpeedIncremental34Values?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (windSpeedIncremental34Value != null)
                {
                    ForecastValue<WindSpeedValue> forecastElement = forecast.Timelines
                      .Where(x => x.DateTime <= currentDateTime)
                      .Select(x => x.WindSpeedCumulative34)
                      .Where(x => x?.Value.Value == windSpeedCumulative34Value.Value)
                      .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<WindSpeedValue>() { Value = windSpeedCumulative34Value, IsStart = windSpeedCumulative34Value.StartDateTime == currentDateTime };
                        forecastTimeLine.WindSpeedCumulative34 = forecastElement;
                    }

                    forecastElement.IsEnd = windSpeedCumulative34Value.EndDateTime == currentDateTime;
                }

                // wind speed - cumulative50
                WindSpeedValue windSpeedCumulative50Value = windSpeedIncremental50Values?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (windSpeedIncremental50Value != null)
                {
                    ForecastValue<WindSpeedValue> forecastElement = forecast.Timelines
                      .Where(x => x.DateTime <= currentDateTime)
                      .Select(x => x.WindSpeedCumulative50)
                      .Where(x => x?.Value.Value == windSpeedCumulative50Value.Value)
                      .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<WindSpeedValue>() { Value = windSpeedCumulative50Value, IsStart = windSpeedCumulative50Value.StartDateTime == currentDateTime };
                        forecastTimeLine.WindSpeedCumulative50 = forecastElement;
                    }

                    forecastElement.IsEnd = windSpeedCumulative50Value.EndDateTime == currentDateTime;
                }

                // wind speed - cumulative64
                WindSpeedValue windSpeedCumulative64Value = windSpeedIncremental64Values?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (windSpeedCumulative64Value != null)
                {
                    ForecastValue<WindSpeedValue> forecastElement = forecast.Timelines
                      .Where(x => x.DateTime <= currentDateTime)
                      .Select(x => x.WindSpeedCumulative64)
                      .Where(x => x?.Value.Value == windSpeedCumulative64Value.Value)
                      .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<WindSpeedValue>() { Value = windSpeedCumulative64Value, IsStart = windSpeedCumulative64Value.StartDateTime == currentDateTime };
                        forecastTimeLine.WindSpeedCumulative64 = forecastElement;
                    }

                    forecastElement.IsEnd = windSpeedCumulative64Value.EndDateTime == currentDateTime;
                }

                // wind direction
                WindDirectionValue windDirectionValue = windDirectionValues?.SingleOrDefault(x => x.StartDateTime == currentDateTime);
                if (windDirectionValue != null)
                {
                    forecastTimeLine.WindDirection = new ForecastValue<double>() { Value = windDirectionValue.Value, IsStart = true, IsEnd = true };
                }

                // cloud amount
                CloudAmountValue cloudAmountValue = cloudAmountValues?.SingleOrDefault(x => x.StartDateTime == currentDateTime);
                if (cloudAmountValue != null)
                {
                    forecastTimeLine.CloudAmount = new ForecastValue<double>() { Value = cloudAmountValue.Value, IsStart = true, IsEnd = true };
                }

                // humidity - releative 
                HumidityValue humidityReleaveValue = humidityReleativeValues?.SingleOrDefault(x => x.StartDateTime == currentDateTime);
                if (humidityReleaveValue != null)
                {
                    forecastTimeLine.HumidityReleative = new ForecastValue<double>() { Value = humidityReleaveValue.Value, IsStart = true, IsEnd = true };
                }

                // humidity - maximum releative 
                HumidityValue humidityMaximumReleaveValue = humidityMaximumReleativeValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (humidityMaximumReleaveValue != null)
                {
                    ForecastValue<HumidityValue> forecastElement = forecast.Timelines
                     .Where(x => x.DateTime <= currentDateTime)
                     .Select(x => x.HumidityMaximumReleative)
                     .Where(x => x?.Value.Value == humidityMaximumReleaveValue.Value)
                     .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<HumidityValue>() { Value = humidityMaximumReleaveValue, IsStart = humidityMaximumReleaveValue.StartDateTime == currentDateTime };
                        forecastTimeLine.HumidityMaximumReleative = forecastElement;
                    }

                    forecastElement.IsEnd = humidityMaximumReleaveValue.EndDateTime == currentDateTime;
                }

                // humidity - minimum releative 
                HumidityValue humidityMinimumReleaveValue = humidityMinimumReleativeValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (humidityMinimumReleaveValue != null)
                {
                    ForecastValue<HumidityValue> forecastElement = forecast.Timelines
                     .Where(x => x.DateTime <= currentDateTime)
                     .Select(x => x.HumidityMinimumReleative)
                     .Where(x => x?.Value.Value == humidityMinimumReleaveValue.Value)
                     .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<HumidityValue>() { Value = humidityMinimumReleaveValue, IsStart = humidityMinimumReleaveValue.StartDateTime == currentDateTime };
                        forecastTimeLine.HumidityMinimumReleative = forecastElement;
                    }

                    forecastElement.IsEnd = humidityMinimumReleaveValue.EndDateTime == currentDateTime;
                }

                // convective hazard - outlook
                ConvectiveHazardValue convectiveHazardOutlookValue = convectiveHazardOutlookValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (convectiveHazardOutlookValue != null)
                {
                    ForecastValue<ConvectiveHazardValue> forecastElement = forecast.Timelines
                    .Where(x => x.DateTime <= currentDateTime)
                    .Select(x => x.ConvectiveHazardOutlook)
                    .Where(x => x?.Value.Value == convectiveHazardOutlookValue.Value)
                    .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ConvectiveHazardValue>() { Value = convectiveHazardOutlookValue, IsStart = convectiveHazardOutlookValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ConvectiveHazardOutlook = forecastElement;
                    }

                    forecastElement.IsEnd = convectiveHazardOutlookValue.EndDateTime == currentDateTime;
                }

                // convective hazard - tornadoes
                ConvectiveHazardValue convectiveHazardTornadoesValue = convectiveHazardTornadoesValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (convectiveHazardTornadoesValue != null)
                {
                    ForecastValue<ConvectiveHazardValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.ConvectiveHazardTornadoes)
                        .Where(x => x?.Value.Value == convectiveHazardTornadoesValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ConvectiveHazardValue>() { Value = convectiveHazardTornadoesValue, IsStart = convectiveHazardTornadoesValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ConvectiveHazardTornadoes = forecastElement;
                    }

                    forecastElement.IsEnd = convectiveHazardTornadoesValue.EndDateTime == currentDateTime;
                }

                // convective hazard - hail
                ConvectiveHazardValue convectiveHazardHailValue = convectiveHazardHailValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (convectiveHazardHailValue != null)
                {
                    ForecastValue<ConvectiveHazardValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.ConvectiveHazardHail)
                        .Where(x => x?.Value.Value == convectiveHazardHailValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ConvectiveHazardValue>() { Value = convectiveHazardHailValue, IsStart = convectiveHazardHailValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ConvectiveHazardHail = forecastElement;
                    }

                    forecastElement.IsEnd = convectiveHazardHailValue.EndDateTime == currentDateTime;
                }

                // convective hazard - damaging thunderstorm winds
                ConvectiveHazardValue convectiveHazardDamagingThunderstormWindsValue = convectiveHazardDamagingThunderstormWindsValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (convectiveHazardDamagingThunderstormWindsValue != null)
                {
                    ForecastValue<ConvectiveHazardValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.ConvectiveHazardDamagingThunderstormWinds)
                        .Where(x => x?.Value.Value == convectiveHazardDamagingThunderstormWindsValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ConvectiveHazardValue>() { Value = convectiveHazardDamagingThunderstormWindsValue, IsStart = convectiveHazardDamagingThunderstormWindsValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ConvectiveHazardDamagingThunderstormWinds = forecastElement;
                    }

                    forecastElement.IsEnd = convectiveHazardDamagingThunderstormWindsValue.EndDateTime == currentDateTime;
                }

                // convective hazard - extreme tornadoes
                ConvectiveHazardValue convectiveHazardExtremeTornadoesValue = convectiveHazardTornadoesValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (convectiveHazardExtremeTornadoesValue != null)
                {
                    ForecastValue<ConvectiveHazardValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.ConvectiveHazardExtremeTornadoes)
                        .Where(x => x?.Value.Value == convectiveHazardExtremeTornadoesValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ConvectiveHazardValue>() { Value = convectiveHazardExtremeTornadoesValue, IsStart = convectiveHazardExtremeTornadoesValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ConvectiveHazardExtremeTornadoes = forecastElement;
                    }

                    forecastElement.IsEnd = convectiveHazardExtremeTornadoesValue.EndDateTime == currentDateTime;
                }

                // convective hazard - extreme hail
                ConvectiveHazardValue convectiveHazardExtremeHailValue = convectiveHazardExtremeHailValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (convectiveHazardExtremeHailValue != null)
                {
                    ForecastValue<ConvectiveHazardValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.ConvectiveHazardExtremeHail)
                        .Where(x => x?.Value.Value == convectiveHazardExtremeHailValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ConvectiveHazardValue>() { Value = convectiveHazardExtremeHailValue, IsStart = convectiveHazardExtremeHailValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ConvectiveHazardExtremeHail = forecastElement;
                    }

                    forecastElement.IsEnd = convectiveHazardExtremeHailValue.EndDateTime == currentDateTime;
                }

                // convective hazard - extreme thunderstorm winds
                ConvectiveHazardValue convectiveHazardExtremeThunderstormWindsValue = convectiveHazardExtremeThunderstormWindsValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (convectiveHazardExtremeThunderstormWindsValue != null)
                {
                    ForecastValue<ConvectiveHazardValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.ConvectiveHazardExtremeThunderstormWinds)
                        .Where(x => x?.Value.Value == convectiveHazardExtremeThunderstormWindsValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ConvectiveHazardValue>() { Value = convectiveHazardExtremeThunderstormWindsValue, IsStart = convectiveHazardExtremeThunderstormWindsValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ConvectiveHazardExtremeThunderstormWinds = forecastElement;
                    }

                    forecastElement.IsEnd = convectiveHazardExtremeThunderstormWindsValue.EndDateTime == currentDateTime;
                }

                // convective hazard - severe thunderstorm
                ConvectiveHazardValue convectiveHazardSevereThunderstormsValue = convectiveHazardSevereThunderstormValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (convectiveHazardSevereThunderstormsValue != null)
                {
                    ForecastValue<ConvectiveHazardValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.ConvectiveHazardSevereThunderstorms)
                        .Where(x => x?.Value.Value == convectiveHazardSevereThunderstormsValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ConvectiveHazardValue>() { Value = convectiveHazardSevereThunderstormsValue, IsStart = convectiveHazardSevereThunderstormsValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ConvectiveHazardSevereThunderstorms = forecastElement;
                    }

                    forecastElement.IsEnd = convectiveHazardSevereThunderstormsValue.EndDateTime == currentDateTime;
                }

                // convective hazard - extreme severe thunderstorm (is this redundant?)
                ConvectiveHazardValue convectiveHazardExtremeSevereThunderstormsValue = convectiveHazardExtremeSevereThunderstormsValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (convectiveHazardExtremeSevereThunderstormsValue != null)
                {
                    ForecastValue<ConvectiveHazardValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.ConvectiveHazardExtremeSevereThunderstorms)
                        .Where(x => x?.Value.Value == convectiveHazardExtremeSevereThunderstormsValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ConvectiveHazardValue>() { Value = convectiveHazardExtremeSevereThunderstormsValue, IsStart = convectiveHazardExtremeSevereThunderstormsValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ConvectiveHazardExtremeSevereThunderstorms = forecastElement;
                    }

                    forecastElement.IsEnd = convectiveHazardExtremeSevereThunderstormsValue.EndDateTime == currentDateTime;
                }

                // fire weather - risk from wind and relative humidity
                FireWeatherValue fireWeatherRiskFromWindAndRelativeHumidityValue = fireWeatherRiskFromWindAndRelativeHumidityValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (fireWeatherRiskFromWindAndRelativeHumidityValue != null)
                {
                    ForecastValue<FireWeatherValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.FireWeatherRiskFromWindAndRelativeHumidity)
                        .Where(x => x?.Value.Value == fireWeatherRiskFromWindAndRelativeHumidityValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<FireWeatherValue>() { Value = fireWeatherRiskFromWindAndRelativeHumidityValue, IsStart = fireWeatherRiskFromWindAndRelativeHumidityValue.StartDateTime == currentDateTime };
                        forecastTimeLine.FireWeatherRiskFromWindAndRelativeHumidity = forecastElement;
                    }

                    forecastElement.IsEnd = fireWeatherRiskFromWindAndRelativeHumidityValue.EndDateTime == currentDateTime;
                }

                // fire weather - risk from dry thunderstorms
                FireWeatherValue fireWeatherRiskFromDryThunderstormsValue = fireWeatherRiskFromDryThunderstormValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (fireWeatherRiskFromDryThunderstormsValue != null)
                {
                    ForecastValue<FireWeatherValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.FireWeatherRiskFromDryThunderstorms)
                        .Where(x => x?.Value.Value == fireWeatherRiskFromDryThunderstormsValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<FireWeatherValue>() { Value = fireWeatherRiskFromDryThunderstormsValue, IsStart = fireWeatherRiskFromDryThunderstormsValue.StartDateTime == currentDateTime };
                        forecastTimeLine.FireWeatherRiskFromDryThunderstorms = forecastElement;
                    }

                    forecastElement.IsEnd = fireWeatherRiskFromDryThunderstormsValue.EndDateTime == currentDateTime;
                }

                // climate anomaly - weekly temperature above normal
                ClimateAnomalyValue climateAnomalyWeeklyTemperatureAboveNormalValue = climateAnomalyWeeklyTemperatureAboveNormalValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (climateAnomalyWeeklyTemperatureAboveNormalValue != null)
                {
                    ForecastValue<ClimateAnomalyValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.ClimateAnomalyWeeklyTemperatureAboveNormal)
                        .Where(x => x?.Value.Value == climateAnomalyWeeklyTemperatureAboveNormalValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ClimateAnomalyValue>() { Value = climateAnomalyWeeklyTemperatureAboveNormalValue, IsStart = climateAnomalyWeeklyTemperatureAboveNormalValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ClimateAnomalyWeeklyTemperatureAboveNormal = forecastElement;
                    }

                    forecastElement.IsEnd = climateAnomalyWeeklyTemperatureAboveNormalValue.EndDateTime == currentDateTime;
                }

                // climate anomaly - weekly temperature below normal
                ClimateAnomalyValue climateAnomalyWeeklyTemperatureBelowNormalValue = climateAnomalyWeeklyTemperatureBelowNormalValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (climateAnomalyWeeklyTemperatureBelowNormalValue != null)
                {
                    ForecastValue<ClimateAnomalyValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.ClimateAnomalyWeeklyTemperatureBelowNormal)
                        .Where(x => x?.Value.Value == climateAnomalyWeeklyTemperatureBelowNormalValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ClimateAnomalyValue>() { Value = climateAnomalyWeeklyTemperatureBelowNormalValue, IsStart = climateAnomalyWeeklyTemperatureBelowNormalValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ClimateAnomalyWeeklyTemperatureBelowNormal = forecastElement;
                    }

                    forecastElement.IsEnd = climateAnomalyWeeklyTemperatureBelowNormalValue.EndDateTime == currentDateTime;
                }

                // climate anomaly - monthly temperature above normal
                ClimateAnomalyValue climateAnomalyMonthlyTemperatureAboveNormalValue = climateAnomalyMonthlyTemperatureAboveNormalValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (climateAnomalyMonthlyTemperatureAboveNormalValue != null)
                {
                    ForecastValue<ClimateAnomalyValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.ClimateAnomalyMonthlyTemperatureAboveNormal)
                        .Where(x => x?.Value.Value == climateAnomalyMonthlyTemperatureAboveNormalValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ClimateAnomalyValue>() { Value = climateAnomalyMonthlyTemperatureAboveNormalValue, IsStart = climateAnomalyMonthlyTemperatureAboveNormalValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ClimateAnomalyMonthlyTemperatureAboveNormal = forecastElement;
                    }

                    forecastElement.IsEnd = climateAnomalyMonthlyTemperatureAboveNormalValue.EndDateTime == currentDateTime;
                }

                // climate anomaly - monthly temperature below normal
                ClimateAnomalyValue climateAnomalyMonthlyTemperatureBelowNormalValue = climateAnomalyMonthlyTemperatureBelowNormalValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (climateAnomalyMonthlyTemperatureBelowNormalValue != null)
                {
                    ForecastValue<ClimateAnomalyValue> forecastElement = forecast.Timelines
                        .Where(x => x.DateTime <= currentDateTime)
                        .Select(x => x.ClimateAnomalyMonthlyTemperatureBelowNormal)
                        .Where(x => x?.Value.Value == climateAnomalyMonthlyTemperatureBelowNormalValue.Value)
                        .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ClimateAnomalyValue>() { Value = climateAnomalyMonthlyTemperatureBelowNormalValue, IsStart = climateAnomalyMonthlyTemperatureBelowNormalValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ClimateAnomalyMonthlyTemperatureBelowNormal = forecastElement;
                    }

                    forecastElement.IsEnd = climateAnomalyMonthlyTemperatureBelowNormalValue.EndDateTime == currentDateTime;
                }

                // climate anomaly - seasonal temperature above normal
                ClimateAnomalyValue climateAnomalySeasonalTemperatureAboveNormalValue = climateAnomalySeasonalTemperatureAboveNormalValues?.FirstOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (climateAnomalyMonthlyTemperatureAboveNormalValue != null)
                {
                    ForecastValue<ClimateAnomalyValue> forecastElement = forecast.Timelines
                       .Where(x => x.DateTime <= currentDateTime)
                       .Select(x => x.ClimateAnomalySeasonalTemperatureAboveNormal)
                       .Where(x => x?.Value.Value == climateAnomalyMonthlyTemperatureAboveNormalValue.Value)
                       .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ClimateAnomalyValue>() { Value = climateAnomalyMonthlyTemperatureAboveNormalValue, IsStart = climateAnomalyMonthlyTemperatureAboveNormalValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ClimateAnomalySeasonalTemperatureAboveNormal = forecastElement;
                    }

                    forecastElement.IsEnd = climateAnomalyMonthlyTemperatureAboveNormalValue.EndDateTime == currentDateTime;
                }

                // climate anomaly - seasonal temperature below normal
                ClimateAnomalyValue climateAnomalySeasonalTemperatureBelowNormalValue = climateAnomalySeasonalTemperatureBelowNormalValues?.FirstOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (climateAnomalySeasonalTemperatureBelowNormalValue != null)
                {
                    ForecastValue<ClimateAnomalyValue> forecastElement = forecast.Timelines
                       .Where(x => x.DateTime <= currentDateTime)
                       .Select(x => x.ClimateAnomalySeasonalTemperatureBelowNormal)
                       .Where(x => x?.Value.Value == climateAnomalySeasonalTemperatureBelowNormalValue.Value)
                       .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ClimateAnomalyValue>() { Value = climateAnomalySeasonalTemperatureBelowNormalValue, IsStart = climateAnomalySeasonalTemperatureBelowNormalValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ClimateAnomalySeasonalTemperatureBelowNormal = forecastElement;
                    }

                    forecastElement.IsEnd = climateAnomalySeasonalTemperatureBelowNormalValue.EndDateTime == currentDateTime;
                }

                // climate anomaly - weekly precipitation above normal
                ClimateAnomalyValue climateAnomalyWeeklyPrecipitationAboveNormalValue = climateAnomalyWeeklyPrecipitationAboveNormalValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (climateAnomalyWeeklyPrecipitationAboveNormalValue != null)
                {
                    ForecastValue<ClimateAnomalyValue> forecastElement = forecast.Timelines
                       .Where(x => x.DateTime <= currentDateTime)
                       .Select(x => x.ClimateAnomalyWeeklyPrecipitationAboveNormal)
                       .Where(x => x?.Value.Value == climateAnomalyWeeklyPrecipitationAboveNormalValue.Value)
                       .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ClimateAnomalyValue>() { Value = climateAnomalyWeeklyPrecipitationAboveNormalValue, IsStart = climateAnomalyWeeklyPrecipitationAboveNormalValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ClimateAnomalyWeeklyPrecipitationAboveNormal = forecastElement;
                    }

                    forecastElement.IsEnd = climateAnomalyWeeklyPrecipitationAboveNormalValue.EndDateTime == currentDateTime;
                }

                // climate anomaly - weekly precipitation below normal
                ClimateAnomalyValue climateAnomalyWeeklyPrecipitationBelowNormalValue = climateAnomalyWeeklyPrecipitationBelowNormalValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (climateAnomalyWeeklyPrecipitationBelowNormalValue != null)
                {
                    ForecastValue<ClimateAnomalyValue> forecastElement = forecast.Timelines
                       .Where(x => x.DateTime <= currentDateTime)
                       .Select(x => x.ClimateAnomalyWeeklyPrecipitationBelowNormal)
                       .Where(x => x?.Value.Value == climateAnomalyWeeklyPrecipitationBelowNormalValue.Value)
                       .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ClimateAnomalyValue>() { Value = climateAnomalyWeeklyPrecipitationBelowNormalValue, IsStart = climateAnomalyWeeklyPrecipitationBelowNormalValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ClimateAnomalyWeeklyPrecipitationBelowNormal = forecastElement;
                    }

                    forecastElement.IsEnd = climateAnomalyWeeklyPrecipitationBelowNormalValue.EndDateTime == currentDateTime;
                }

                // climate anomaly - monthly precipitation above normal
                ClimateAnomalyValue climateAnomalyMonthlyPrecipitationAboveNormalValue = climateAnomalyMonthlyPrecipitationAboveNormalValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (climateAnomalyMonthlyPrecipitationAboveNormalValue != null)
                {
                    ForecastValue<ClimateAnomalyValue> forecastElement = forecast.Timelines
                       .Where(x => x.DateTime <= currentDateTime)
                       .Select(x => x.ClimateAnomalyMonthlyPrecipitationAboveNormal)
                       .Where(x => x?.Value.Value == climateAnomalyMonthlyPrecipitationAboveNormalValue.Value)
                       .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ClimateAnomalyValue>() { Value = climateAnomalyMonthlyPrecipitationAboveNormalValue, IsStart = climateAnomalyMonthlyPrecipitationAboveNormalValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ClimateAnomalyMonthlyPrecipitationAboveNormal = forecastElement;
                    }

                    forecastElement.IsEnd = climateAnomalyMonthlyPrecipitationAboveNormalValue.EndDateTime == currentDateTime;
                }

                // climate anomaly - monthly precipitation below normal
                ClimateAnomalyValue climateAnomalyMonthlyPrecipitationBelowNormalValue = climateAnomalyMonthlyPrecipitationBelowNormalValues?.SingleOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (climateAnomalyMonthlyPrecipitationBelowNormalValue != null)
                {
                    ForecastValue<ClimateAnomalyValue> forecastElement = forecast.Timelines
                       .Where(x => x.DateTime <= currentDateTime)
                       .Select(x => x.ClimateAnomalyMonthlyPrecipitationBelowNormal)
                       .Where(x => x?.Value.Value == climateAnomalyMonthlyPrecipitationBelowNormalValue.Value)
                       .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ClimateAnomalyValue>() { Value = climateAnomalyMonthlyPrecipitationBelowNormalValue, IsStart = climateAnomalyMonthlyPrecipitationBelowNormalValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ClimateAnomalyMonthlyPrecipitationBelowNormal = forecastElement;
                    }

                    forecastElement.IsEnd = climateAnomalyMonthlyPrecipitationBelowNormalValue.EndDateTime == currentDateTime;
                }

                // climate anomaly - seasonal precipitation above normal
                ClimateAnomalyValue climateAnomalySeasonalPrecipitationAboveNormalValue = climateAnomalySeasonalPrecipitationAboveNormalValues?.FirstOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (climateAnomalySeasonalPrecipitationAboveNormalValue != null)
                {
                    ForecastValue<ClimateAnomalyValue> forecastElement = forecast.Timelines
                       .Where(x => x.DateTime <= currentDateTime)
                       .Select(x => x.ClimateAnomalySeasonalPrecipitationAboveNormal)
                       .Where(x => x?.Value.Value == climateAnomalySeasonalPrecipitationAboveNormalValue.Value)
                       .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ClimateAnomalyValue>() { Value = climateAnomalySeasonalPrecipitationAboveNormalValue, IsStart = climateAnomalySeasonalPrecipitationAboveNormalValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ClimateAnomalySeasonalPrecipitationAboveNormal = forecastElement;
                    }

                    forecastElement.IsEnd = climateAnomalySeasonalPrecipitationAboveNormalValue.EndDateTime == currentDateTime;
                }

                // climate anomaly - seasonal precipitation below normal
                ClimateAnomalyValue climateAnomalySeasonalPrecipitationBelowNormalValue = climateAnomalySeasonalPrecipitationBelowNormalValues?.FirstOrDefault(x => x.StartDateTime <= currentDateTime & x.EndDateTime > currentDateTime);
                if (climateAnomalySeasonalPrecipitationBelowNormalValue != null)
                {
                    ForecastValue<ClimateAnomalyValue> forecastElement = forecast.Timelines
                       .Where(x => x.DateTime <= currentDateTime)
                       .Select(x => x.ClimateAnomalySeasonalPrecipitationBelowNormal)
                       .Where(x => x?.Value.Value == climateAnomalySeasonalPrecipitationBelowNormalValue.Value)
                       .SingleOrDefault();

                    if (forecastElement == null)
                    {
                        forecastElement = new ForecastValue<ClimateAnomalyValue>() { Value = climateAnomalySeasonalPrecipitationBelowNormalValue, IsStart = climateAnomalySeasonalPrecipitationBelowNormalValue.StartDateTime == currentDateTime };
                        forecastTimeLine.ClimateAnomalySeasonalPrecipitationBelowNormal = forecastElement;
                    }

                    forecastElement.IsEnd = climateAnomalySeasonalPrecipitationBelowNormalValue.EndDateTime == currentDateTime;
                }

                // weather conditions
                WeatherConditions weatherCondition = weatherConditions?.SingleOrDefault(x => x.StartDateTime == currentDateTime);
                if (weatherCondition != null && weatherCondition.Values.Count > 0)
                {
                    forecastTimeLine.WeatherConditions = new ForecastValue<WeatherConditions>() { Value = weatherCondition, IsStart = true, IsEnd = true };
                }

                // condition icons
                ConditionIconValue conditionIconValue = conditionIconValues?.SingleOrDefault(x => x.StartDateTime == currentDateTime);
                if (conditionIconValue != null)
                {
                    forecastTimeLine.ConditionIcons = new ForecastValue<Uri>() { Value = conditionIconValue.Value, IsStart = true, IsEnd = true };
                }

                // if there is data; add it to the list.
                if (!forecastTimeLine.IsEmpty) forecast.Timelines.Add(forecastTimeLine);

                currentDateTime = currentDateTime.AddMinutes(30);
            }

            return forecast;

        }

    }


}
