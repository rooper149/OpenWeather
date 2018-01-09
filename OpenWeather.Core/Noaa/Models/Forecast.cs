using System;
using System.Collections.Generic;

namespace OpenWeather.Noaa.Models
{


    public class Forecast
    {
        public ForecastSource Source { get; set; }
        public List<ForecastTimeLine> Timelines { get; set; }

        public Forecast()
        {
            Timelines = new List<ForecastTimeLine>();
        }
    }

    public class ForecastSource
    {
        public string MoreInformation { get; set; }
        public string ProductionCenter { get; set; }
        public string Disclaimer { get; set; }
        public string Credit { get; set; }
        public string CreditLogoUrl { get; set; }
        public string Feedback { get; set; }

    }

    public class ForecastTimeLine
    {
        public DateTime DateTime { get; set; }
        public ForecastValue<double> TemperatureMaximum { get; set; }
        public ForecastValue<double> TemperatureMinimum { get; set; }
        public ForecastValue<double> TemperatureHourly { get; set; }
        public ForecastValue<double> TemperatureDewpoint { get; set; }
        public ForecastValue<double> TemperatureApparent { get; set; }
        public ForecastValue<PrecipitationValue> PrecipitationIce { get; set; }
        public ForecastValue<PrecipitationValue> PrecipitationSnow { get; set; }
        public ForecastValue<PrecipitationValue> PrecipitationLiquid { get; set; }
        public ForecastValue<ProbabilityOfPrecipitationValue> ProbabilityOfPrecipitation { get; set; }
        public ForecastValue<WindSpeedValue> WindSpeedGust { get; set; }
        public ForecastValue<double> WindSpeedSustained { get; set; }
        public ForecastValue<WindSpeedValue> WindSpeedIncremental34 { get; set; }
        public ForecastValue<WindSpeedValue> WindSpeedIncremental50 { get; set; }
        public ForecastValue<WindSpeedValue> WindSpeedIncremental64 { get; set; }
        public ForecastValue<WindSpeedValue> WindSpeedCumulative34 { get; set; }
        public ForecastValue<WindSpeedValue> WindSpeedCumulative50 { get; set; }
        public ForecastValue<WindSpeedValue> WindSpeedCumulative64 { get; set; }
        public ForecastValue<double> WindDirection { get; set; }
        public ForecastValue<double> CloudAmount { get; set; }
        public ForecastValue<double> HumidityReleative { get; set; }
        public ForecastValue<HumidityValue> HumidityMaximumReleative { get; set; }
        public ForecastValue<HumidityValue> HumidityMinimumReleative { get; set; }
        public ForecastValue<ConvectiveHazardValue> ConvectiveHazardOutlook { get; set; }
        public ForecastValue<ConvectiveHazardValue> ConvectiveHazardTornadoes { get; set; }
        public ForecastValue<ConvectiveHazardValue> ConvectiveHazardHail { get; set; }
        public ForecastValue<ConvectiveHazardValue> ConvectiveHazardDamagingThunderstormWinds { get; set; }
        public ForecastValue<ConvectiveHazardValue> ConvectiveHazardExtremeTornadoes { get; set; }
        public ForecastValue<ConvectiveHazardValue> ConvectiveHazardExtremeHail { get; set; }
        public ForecastValue<ConvectiveHazardValue> ConvectiveHazardExtremeThunderstormWinds { get; set; }
        public ForecastValue<ConvectiveHazardValue> ConvectiveHazardSevereThunderstorms { get; set; }
        public ForecastValue<ConvectiveHazardValue> ConvectiveHazardExtremeSevereThunderstorms { get; set; }
        public ForecastValue<FireWeatherValue> FireWeatherRiskFromWindAndRelativeHumidity { get; set; }
        public ForecastValue<FireWeatherValue> FireWeatherRiskFromDryThunderstorms { get; set; }
        public ForecastValue<ClimateAnomalyValue> ClimateAnomalyWeeklyTemperatureAboveNormal { get; set; }
        public ForecastValue<ClimateAnomalyValue> ClimateAnomalyWeeklyTemperatureBelowNormal { get; set; }
        public ForecastValue<ClimateAnomalyValue> ClimateAnomalyMonthlyTemperatureAboveNormal { get; set; }
        public ForecastValue<ClimateAnomalyValue> ClimateAnomalyMonthlyTemperatureBelowNormal { get; set; }
        public ForecastValue<ClimateAnomalyValue> ClimateAnomalySeasonalTemperatureAboveNormal { get; set; }
        public ForecastValue<ClimateAnomalyValue> ClimateAnomalySeasonalTemperatureBelowNormal { get; set; }
        public ForecastValue<ClimateAnomalyValue> ClimateAnomalyWeeklyPrecipitationAboveNormal { get; set; }
        public ForecastValue<ClimateAnomalyValue> ClimateAnomalyWeeklyPrecipitationBelowNormal { get; set; }
        public ForecastValue<ClimateAnomalyValue> ClimateAnomalyMonthlyPrecipitationAboveNormal { get; set; }
        public ForecastValue<ClimateAnomalyValue> ClimateAnomalyMonthlyPrecipitationBelowNormal { get; set; }
        public ForecastValue<ClimateAnomalyValue> ClimateAnomalySeasonalPrecipitationAboveNormal { get; set; }
        public ForecastValue<ClimateAnomalyValue> ClimateAnomalySeasonalPrecipitationBelowNormal { get; set; }
        public ForecastValue<WeatherConditions> WeatherConditions { get; set; }
        public ForecastValue<Uri> ConditionIcons { get; set; }

        public bool IsEmpty
        {
            get
            {
                return TemperatureMaximum.IsNull() & TemperatureMinimum.IsNull() & TemperatureHourly.IsNull() & TemperatureApparent.IsNull() &
                    PrecipitationIce.IsNull() & PrecipitationSnow.IsNull() & PrecipitationLiquid.IsNull() &
                    ProbabilityOfPrecipitation.IsNull() &
                    WindSpeedGust.IsNull() & WindSpeedSustained.IsNull() &
                    WindSpeedIncremental34.IsNull() & WindSpeedIncremental50.IsNull() & WindSpeedIncremental64.IsNull() &
                    WindSpeedCumulative34.IsNull() & WindSpeedCumulative50.IsNull() & WindSpeedCumulative64.IsNull() &
                    WindDirection.IsNull() & CloudAmount.IsNull() &
                    HumidityReleative.IsNull() & HumidityMaximumReleative.IsNull() & HumidityMinimumReleative.IsNull() &
                    ConvectiveHazardOutlook.IsNull() & ConvectiveHazardTornadoes.IsNull() & ConvectiveHazardHail.IsNull() &
                    ConvectiveHazardDamagingThunderstormWinds.IsNull() & ConvectiveHazardExtremeTornadoes.IsNull() &
                    ConvectiveHazardExtremeHail.IsNull() & ConvectiveHazardExtremeThunderstormWinds.IsNull() &
                    ConvectiveHazardSevereThunderstorms.IsNull() & ConvectiveHazardExtremeSevereThunderstorms.IsNull() &
                    FireWeatherRiskFromWindAndRelativeHumidity.IsNull() & FireWeatherRiskFromDryThunderstorms.IsNull() &
                    ClimateAnomalyWeeklyTemperatureAboveNormal.IsNull() & ClimateAnomalyWeeklyTemperatureBelowNormal.IsNull() &
                    ClimateAnomalyMonthlyTemperatureAboveNormal.IsNull() & ClimateAnomalyMonthlyTemperatureBelowNormal.IsNull() &
                    ClimateAnomalySeasonalTemperatureAboveNormal.IsNull() & ClimateAnomalySeasonalTemperatureBelowNormal.IsNull() &
                    ClimateAnomalyWeeklyPrecipitationAboveNormal.IsNull() & ClimateAnomalyWeeklyPrecipitationBelowNormal.IsNull() &
                    ClimateAnomalyMonthlyPrecipitationAboveNormal.IsNull() & ClimateAnomalyMonthlyPrecipitationBelowNormal.IsNull() &
                    ClimateAnomalySeasonalPrecipitationAboveNormal.IsNull() & ClimateAnomalySeasonalPrecipitationBelowNormal.IsNull() &
                    WeatherConditions.IsNull() & ConditionIcons.IsNull();

            }
        }
    }


    public class ForecastValue<T>
    {
        public T Value { get; set; }
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
    }

}
