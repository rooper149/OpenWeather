namespace OpenWeather.Models
{
    /// <summary>
    /// Parameters used by the National Digital Forecast Database XML Web Service
    /// Source: https://graphical.weather.gov/xml/docs/elementInputNames.php
    /// </summary>
    public class WeatherParameters
    {
        public void SelectAll()
        {
            Is3HourTempRequested = true;
            IsApparentTemperatureRequested = true;
            IsCloudCoverAmountRequested = true;
            IsConvectiveHazardOutlookRequested = true;
            IsDewPointTemeratureRequested = true;
            IsFireWeatherFromDryThunderstormsRequested = true;
            IsFireWeatherFromWindAndRelativeHumidityRequested = true;
            IsIceAccumulationRequested = true;
            IsMaximumRelativeHumidityRequested = true;
            IsMaximumTempuratureRequested = true;
            IsMinimumRelativeHumidityRequested = true;
            IsMinimumTempuratureRequested = true;
            IsPrecipitationAmountRequested = true;
            IsProbabilisticTropicalCycloneWindSpeedOver34Knots_CumulativeRequested = true;
            IsProbabilisticTropicalCycloneWindSpeedOver34Knots_IncrementalRequested = true;
            IsProbabilisticTropicalCycloneWindSpeedOver50Knots_CumulativeRequested = true;
            IsProbabilisticTropicalCycloneWindSpeedOver50Knots_IncrementalRequested = true;
            IsProbabilisticTropicalCycloneWindSpeedOver64Knots_CumulativeRequested = true;
            IsProbabilisticTropicalCycloneWindSpeedOver64Knots_IncrementalRequested = true;
            IsProbability8To14DayTotalPrecipitationAboveMedianRequested = true;
            IsProbability8To14DayTotalPrecipitationBelowMedianRequested = true;
            IsProbabilityOf8To14DayAverageTemperatureAboveNormalRequested = true;
            IsProbabilityOf8To14DayAverageTemperatureBelowNormalRequested = true;
            IsProbabilityOfDamagingThunderstormWindsRequested = true;
            IsProbabilityOfExtremeHailRequested = true;
            IsProbabilityOfExtremeSevereThunderstormsRequested = true;
            IsProbabilityOfExtremeThunderstormWindsRequested = true;
            IsProbabilityOfExtremeTornadoesRequested = true;
            IsProbabilityOfHailRequested = true;
            IsProbabilityOfOneMonthAverageTemperatureAboveNormalRequested = true;
            IsProbabilityOfOneMonthAverageTemperatureBelowNormalRequested = true;
            IsProbabilityOfPrecipitationRequested = true;
            IsProbabilityOfSevereThunderstormsRequested = true;
            IsProbabilityOfThreeMonthAverageTemperatureAboveNormalRequested = true;
            IsProbabilityOfThreeMonthAverageTemperatureBelowNormalRequested = true;
            IsProbabilityOfTornadoesRequested = true;
            IsProbabilityOneMonthTotalPrecipitationAboveMedianRequested = true;
            IsProbabilityOneMonthTotalPrecipitationBelowMedianRequested = true;
            IsProbabilityThreeMonthTotalPrecipitationAboveMedianRequested = true;
            IsProbabilityThreeMonthTotalPrecipitationBelowMedianRequested = true;
            IsRealTimeMesoscaleAnalysisDewpointTemperatureRequested = true;
            IsRealTimeMesoscaleAnalysisGOESEffectiveCloudAmountRequested = true;
            IsRealTimeMesoscaleAnalysisPrecipitationRequested = true;
            IsRealTimeMesoscaleAnalysisTemperatureRequested = true;
            IsRealTimeMesoscaleAnalysisWindDirectionRequested = true;
            IsRealTimeMesoscaleAnalysisWindSpeedRequested = true;
            IsRelativeHumidityRequested = true;
            IsSnowFallAmountRequested = true;
            IsWatchesWarningAdvisoriesRequested = true;
            IsWaveHeightRequested = true;
            IsWeatherIconsRequested = true;
            IsWeatherRequested = true;
            IsWindDirectionRequested = true;
            IsWindGustsRequested = true;
            IsWindSpeedRequested = true;
        }

        public void SelectNone()
        {
            Is3HourTempRequested = false;
            IsApparentTemperatureRequested = false;
            IsCloudCoverAmountRequested = false;
            IsConvectiveHazardOutlookRequested = false;
            IsDewPointTemeratureRequested = false;
            IsFireWeatherFromDryThunderstormsRequested = false;
            IsFireWeatherFromWindAndRelativeHumidityRequested = false;
            IsIceAccumulationRequested = false;
            IsMaximumRelativeHumidityRequested = false;
            IsMaximumTempuratureRequested = false;
            IsMinimumRelativeHumidityRequested = false;
            IsMinimumTempuratureRequested = false;
            IsPrecipitationAmountRequested = false;
            IsProbabilisticTropicalCycloneWindSpeedOver34Knots_CumulativeRequested = false;
            IsProbabilisticTropicalCycloneWindSpeedOver34Knots_IncrementalRequested = false;
            IsProbabilisticTropicalCycloneWindSpeedOver50Knots_CumulativeRequested = false;
            IsProbabilisticTropicalCycloneWindSpeedOver50Knots_IncrementalRequested = false;
            IsProbabilisticTropicalCycloneWindSpeedOver64Knots_CumulativeRequested = false;
            IsProbabilisticTropicalCycloneWindSpeedOver64Knots_IncrementalRequested = false;
            IsProbability8To14DayTotalPrecipitationAboveMedianRequested = false;
            IsProbability8To14DayTotalPrecipitationBelowMedianRequested = false;
            IsProbabilityOf8To14DayAverageTemperatureAboveNormalRequested = false;
            IsProbabilityOf8To14DayAverageTemperatureBelowNormalRequested = false;
            IsProbabilityOfDamagingThunderstormWindsRequested = false;
            IsProbabilityOfExtremeHailRequested = false;
            IsProbabilityOfExtremeSevereThunderstormsRequested = false;
            IsProbabilityOfExtremeThunderstormWindsRequested = false;
            IsProbabilityOfExtremeTornadoesRequested = false;
            IsProbabilityOfHailRequested = false;
            IsProbabilityOfOneMonthAverageTemperatureAboveNormalRequested = false;
            IsProbabilityOfOneMonthAverageTemperatureBelowNormalRequested = false;
            IsProbabilityOfPrecipitationRequested = false;
            IsProbabilityOfSevereThunderstormsRequested = false;
            IsProbabilityOfThreeMonthAverageTemperatureAboveNormalRequested = false;
            IsProbabilityOfThreeMonthAverageTemperatureBelowNormalRequested = false;
            IsProbabilityOfTornadoesRequested = false;
            IsProbabilityOneMonthTotalPrecipitationAboveMedianRequested = false;
            IsProbabilityOneMonthTotalPrecipitationBelowMedianRequested = false;
            IsProbabilityThreeMonthTotalPrecipitationAboveMedianRequested = false;
            IsProbabilityThreeMonthTotalPrecipitationBelowMedianRequested = false;
            IsRealTimeMesoscaleAnalysisDewpointTemperatureRequested = false;
            IsRealTimeMesoscaleAnalysisGOESEffectiveCloudAmountRequested = false;
            IsRealTimeMesoscaleAnalysisPrecipitationRequested = false;
            IsRealTimeMesoscaleAnalysisTemperatureRequested = false;
            IsRealTimeMesoscaleAnalysisWindDirectionRequested = false;
            IsRealTimeMesoscaleAnalysisWindSpeedRequested = false;
            IsRelativeHumidityRequested = false;
            IsSnowFallAmountRequested = false;
            IsWatchesWarningAdvisoriesRequested = false;
            IsWaveHeightRequested = false;
            IsWeatherIconsRequested = false;
            IsWeatherRequested = false;
            IsWindDirectionRequested = false;
            IsWindGustsRequested = false;
            IsWindSpeedRequested = false;
        }

        public bool IsMaximumTempuratureRequested { get; set; }
        public bool IsMinimumTempuratureRequested { get; set; }
        public bool Is3HourTempRequested { get; set; }
        public bool IsDewPointTemeratureRequested { get; set; }
        public bool IsApparentTemperatureRequested { get; set; }
        public bool IsProbabilityOfPrecipitationRequested { get; set; }
        public bool IsPrecipitationAmountRequested { get; set; }
        public bool IsSnowFallAmountRequested { get; set; }
        public bool IsCloudCoverAmountRequested { get; set; }
        public bool IsRelativeHumidityRequested { get; set; }
        public bool IsWindSpeedRequested { get; set; }
        public bool IsWindDirectionRequested { get; set; }
        public bool IsWeatherRequested { get; set; }
        public bool IsWeatherIconsRequested { get; set; }
        public bool IsWaveHeightRequested { get; set; }
        public bool IsProbabilisticTropicalCycloneWindSpeedOver34Knots_IncrementalRequested { get; set; }
        public bool IsProbabilisticTropicalCycloneWindSpeedOver50Knots_IncrementalRequested { get; set; }
        public bool IsProbabilisticTropicalCycloneWindSpeedOver64Knots_IncrementalRequested { get; set; }
        public bool IsProbabilisticTropicalCycloneWindSpeedOver34Knots_CumulativeRequested { get; set; }
        public bool IsProbabilisticTropicalCycloneWindSpeedOver50Knots_CumulativeRequested { get; set; }
        public bool IsProbabilisticTropicalCycloneWindSpeedOver64Knots_CumulativeRequested { get; set; }
        public bool IsWindGustsRequested { get; set; }
        public bool IsFireWeatherFromWindAndRelativeHumidityRequested { get; set; }
        public bool IsFireWeatherFromDryThunderstormsRequested { get; set; }
        public bool IsConvectiveHazardOutlookRequested { get; set; }
        public bool IsProbabilityOfTornadoesRequested { get; set; }
        public bool IsProbabilityOfHailRequested { get; set; }
        public bool IsProbabilityOfDamagingThunderstormWindsRequested { get; set; }
        public bool IsProbabilityOfExtremeTornadoesRequested { get; set; }
        public bool IsProbabilityOfExtremeHailRequested { get; set; }
        public bool IsProbabilityOfExtremeThunderstormWindsRequested { get; set; }
        public bool IsProbabilityOfSevereThunderstormsRequested { get; set; }
        public bool IsProbabilityOfExtremeSevereThunderstormsRequested { get; set; }
        public bool IsProbabilityOf8To14DayAverageTemperatureAboveNormalRequested { get; set; }
        public bool IsProbabilityOf8To14DayAverageTemperatureBelowNormalRequested { get; set; }
        public bool IsProbabilityOfOneMonthAverageTemperatureAboveNormalRequested { get; set; }
        public bool IsProbabilityOfOneMonthAverageTemperatureBelowNormalRequested { get; set; }
        public bool IsProbabilityOfThreeMonthAverageTemperatureAboveNormalRequested { get; set; }
        public bool IsProbabilityOfThreeMonthAverageTemperatureBelowNormalRequested { get; set; }
        public bool IsProbability8To14DayTotalPrecipitationAboveMedianRequested { get; set; }
        public bool IsProbability8To14DayTotalPrecipitationBelowMedianRequested { get; set; }
        public bool IsProbabilityOneMonthTotalPrecipitationAboveMedianRequested { get; set; }
        public bool IsProbabilityOneMonthTotalPrecipitationBelowMedianRequested { get; set; }
        public bool IsProbabilityThreeMonthTotalPrecipitationAboveMedianRequested { get; set; }
        public bool IsProbabilityThreeMonthTotalPrecipitationBelowMedianRequested { get; set; }
        public bool IsRealTimeMesoscaleAnalysisPrecipitationRequested { get; set; }
        public bool IsRealTimeMesoscaleAnalysisGOESEffectiveCloudAmountRequested { get; set; }
        public bool IsRealTimeMesoscaleAnalysisDewpointTemperatureRequested { get; set; }
        public bool IsRealTimeMesoscaleAnalysisTemperatureRequested { get; set; }
        public bool IsRealTimeMesoscaleAnalysisWindDirectionRequested { get; set; }
        public bool IsRealTimeMesoscaleAnalysisWindSpeedRequested { get; set; }
        public bool IsWatchesWarningAdvisoriesRequested { get; set; }
        public bool IsIceAccumulationRequested { get; set; }
        public bool IsMaximumRelativeHumidityRequested { get; set; }
        public bool IsMinimumRelativeHumidityRequested { get; set; }

    }


}
