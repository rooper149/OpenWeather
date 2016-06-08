using UnitsNet;
using UnitsNet.Units;

namespace OpenWeather
{
    /// <summary>
    /// Class to handle unit preferences
    /// </summary>
    public class Units
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Units()
        {
            PressureUnit = PressureUnit.Torr;
            TemperatureUnit = TemperatureUnit.DegreeCelsius;
            WindSpeedUnit = SpeedUnit.Knot;
            VisibilityUnit = LengthUnit.Kilometer;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tempUnit">Unit for temperature, default = celcius</param>
        /// <param name="pUnit">Unit for pressure, default = torr</param>
        /// <param name="speedUnit">Unit for speed (wind), default = knots</param>
        /// <param name="distanceUnit">Unit for distance (visibility), default = kilometer</param>
        public Units(TemperatureUnit tempUnit, PressureUnit pUnit, SpeedUnit speedUnit, LengthUnit distanceUnit)
        {
            PressureUnit = pUnit;
            TemperatureUnit = tempUnit;
            WindSpeedUnit = speedUnit;
            VisibilityUnit = distanceUnit;
        }

        /// <summary>
        /// Pressure unit
        /// </summary>
        public PressureUnit PressureUnit { get; }

        /// <summary>
        /// Temperature unit
        /// </summary>
        public TemperatureUnit TemperatureUnit { get; }

        /// <summary>
        /// Speed unit for wind speed
        /// </summary>
        public SpeedUnit WindSpeedUnit { get; }

        /// <summary>
        /// Distance unit for visibility
        /// </summary>
        public LengthUnit VisibilityUnit { get; }

        /// <summary>
        /// Converts from one unit profile to another unit.
        /// </summary>
        /// <param name="originalUnits">Original unit profile</param>
        /// <param name="toUnit">Unit to convert to</param>
        /// <param name="value">Temperature value to convert</param>
        /// <returns>Converted temperature</returns>
        public static double ConvertTemperature(Units originalUnits, TemperatureUnit toUnit, double value) =>
            Temperature.From(value, originalUnits.TemperatureUnit).As(toUnit);

        /// <summary>
        /// Converts from one unit profile to another unit.
        /// </summary>
        /// <param name="originalUnits">Original unit profile</param>
        /// <param name="toUnit">Unit to convert to</param>
        /// <param name="value">Pressure value to convert</param>
        /// <returns>Converted pressure</returns>
        public static double ConvertPressure(Units originalUnits, PressureUnit toUnit, double value) =>
            Pressure.From(value, originalUnits.PressureUnit).As(toUnit);

        /// <summary>
        /// Converts from one unit profile to another unit.
        /// </summary>
        /// <param name="originalUnits">Original unit profile</param>
        /// <param name="toUnit">Unit to convert to</param>
        /// <param name="value">Speed value to convert</param>
        /// <returns>Converted speed</returns>
        public static double ConvertWindSpeed(Units originalUnits, SpeedUnit toUnit, double value) =>
            Speed.From(value, originalUnits.WindSpeedUnit).As(toUnit);

        /// <summary>
        /// Converts from one unit profile to another unit.
        /// </summary>
        /// <param name="originalUnits">Original unit profile</param>
        /// <param name="toUnit">Unit to convert to</param>
        /// <param name="value">Distance value to convert</param>
        /// <returns>Converted distance</returns>
        public static double ConvertDistance(Units originalUnits, LengthUnit toUnit, double value) =>
            Length.From(value, originalUnits.VisibilityUnit).As(toUnit);
    }
}