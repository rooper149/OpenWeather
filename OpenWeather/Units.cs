using UnitsNet;
using UnitsNet.Units;

namespace OpenWeather
{
    /// <summary>
    /// Class to handle unit preferences
    /// </summary>
    public struct Units
    {
        /// <summary>
        /// Pressure unit
        /// </summary>
        public readonly PressureUnit PressureUnit;

        /// <summary>
        /// Temperature unit
        /// </summary>
        public readonly TemperatureUnit TemperatureUnit;

        /// <summary>
        /// Speed unit for wind speed
        /// </summary>
        public readonly SpeedUnit WindSpeedUnit;

        /// <summary>
        /// Distance unit for visibility
        /// </summary>
        public readonly LengthUnit VisibilityUnit;


        /// <summary>
        /// Default .ctor with default units (knots, pascal, km, centigrade)
        /// </summary>
        public Units()
        {
            WindSpeedUnit = SpeedUnit.Knot;
            PressureUnit = PressureUnit.Pascal;
            VisibilityUnit = LengthUnit.Kilometer;
            TemperatureUnit = TemperatureUnit.DegreeCelsius;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tempUnit">Unit for temperature, default = celcius</param>
        /// <param name="pUnit">Unit for pressure, default = pascal</param>
        /// <param name="speedUnit">Unit for speed (wind), default = knots</param>
        /// <param name="distanceUnit">Unit for distance (visibility), default = kilometer</param>
        public Units(TemperatureUnit tempUnit = TemperatureUnit.DegreeCelsius, PressureUnit pUnit = PressureUnit.Pascal, SpeedUnit speedUnit = SpeedUnit.Knot, LengthUnit distanceUnit = LengthUnit.Kilometer)
        {
            PressureUnit = pUnit;
            TemperatureUnit = tempUnit;
            WindSpeedUnit = speedUnit;
            VisibilityUnit = distanceUnit;
        }

        /// <summary>
        /// Converts from one unit profile to another unit.
        /// </summary>
        /// <param name="originalUnits">Original unit profile</param>
        /// <param name="toUnit">Unit to convert to</param>
        /// <param name="value">Temperature value to convert</param>
        /// <returns>Converted temperature</returns>
        public double ConvertTemperature(Units toUnit, double value) =>
            Temperature.From(value, TemperatureUnit).As(toUnit.TemperatureUnit);

        /// <summary>
        /// Converts from one unit profile to another unit.
        /// </summary>
        /// <param name="originalUnits">Original unit profile</param>
        /// <param name="toUnit">Unit to convert to</param>
        /// <param name="value">Pressure value to convert</param>
        /// <returns>Converted pressure</returns>
        public double ConvertPressure(Units toUnit, double value) =>
            Pressure.From(value, PressureUnit).As(toUnit.PressureUnit);

        /// <summary>
        /// Converts from one unit profile to another unit.
        /// </summary>
        /// <param name="originalUnits">Original unit profile</param>
        /// <param name="toUnit">Unit to convert to</param>
        /// <param name="value">Speed value to convert</param>
        /// <returns>Converted speed</returns>
        public double ConvertWindSpeed(Units toUnit, double value) =>
            Speed.From(value, WindSpeedUnit).As(toUnit.WindSpeedUnit);

        /// <summary>
        /// Converts from one unit profile to another unit.
        /// </summary>
        /// <param name="originalUnits">Original unit profile</param>
        /// <param name="toUnit">Unit to convert to</param>
        /// <param name="value">Distance value to convert</param>
        /// <returns>Converted distance</returns>
        public double ConvertDistance(Units toUnit, double value) =>
            Length.From(value, VisibilityUnit).As(toUnit.VisibilityUnit);

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
