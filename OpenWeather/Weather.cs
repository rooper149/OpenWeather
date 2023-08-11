namespace OpenWeather
{
    public readonly struct Weather
    {
        /// <summary>
        /// The units used for this measurment
        /// </summary>
        public readonly Units Units;

        /// <summary>
        /// Current temperature at the weather station
        /// </summary>
        public readonly double Temperature;

        /// <summary>
        /// Current dewpoint at the weather station
        /// </summary>
        public readonly double Dewpoint;

        /// <summary>
        /// Current wind speed at the weather station
        /// </summary>
        public readonly double WindSpeed;

        /// <summary>
        /// Current wind heading at the weather station
        /// </summary>
        public readonly int WindHeading;

        /// <summary>
        /// Current pressure at the weather station
        /// </summary>
        public readonly double Pressure;

        /// <summary>
        /// Current visibility at the weather station
        /// </summary>
        public readonly double Visibility;

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="temperature">Temperature at the weather station</param>
        /// <param name="dewpoint">Dewpoint at the weather station</param>
        /// <param name="windspeed">Winds peed at the weather station</param>
        /// <param name="windheading">Wind heading at the weather station</param>
        /// <param name="pressure">Pressure at the weather station</param>
        /// <param name="visibility">Visibility at the weather station</param>
        public Weather(Units units, double temperature, double dewpoint, double windspeed, int windheading, double pressure, double visibility)
        {
            Units = units;
            Dewpoint = dewpoint;
            Pressure = pressure;
            WindSpeed = windspeed;
            Visibility = visibility;
            WindHeading = windheading;
            Temperature = temperature;
        }

        public Weather ConvertTo(Units units)
        {
            return new Weather(units,
                Units.ConvertTemperature(units, Temperature),
                Dewpoint, Units.ConvertWindSpeed(units, WindSpeed),
                WindHeading, Units.ConvertPressure(units, Pressure),
                Units.ConvertDistance(units, Visibility));
        }
    }
}
