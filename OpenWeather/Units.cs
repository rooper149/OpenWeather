using UnitsNet;
using UnitsNet.Units;

namespace OpenWeather
{
    public class Units
    {
        public Units()
        {
            PressureUnit = PressureUnit.Torr;
            TemperatureUnit = TemperatureUnit.DegreeCelsius;
            WindSpeedUnit = SpeedUnit.Knot;
            VisibilityUnit = LengthUnit.Kilometer;
        }

        public Units(TemperatureUnit tempUnit, PressureUnit pUnit, SpeedUnit speedUnit, LengthUnit distanceUnit)
        {
            PressureUnit = pUnit;
            TemperatureUnit = tempUnit;
            WindSpeedUnit = speedUnit;
            VisibilityUnit = distanceUnit;
        }

        public PressureUnit PressureUnit { get; }
        public TemperatureUnit TemperatureUnit { get; }
        public SpeedUnit WindSpeedUnit { get; }
        public LengthUnit VisibilityUnit { get; }

        public static double ConvertTemperature(Units originalUnits, TemperatureUnit toUnit, double value) =>
            Temperature.From(value, originalUnits.TemperatureUnit).As(toUnit);

        public static double ConvertPressure(Units originalUnits, PressureUnit toUnit, double value) =>
            Pressure.From(value, originalUnits.PressureUnit).As(toUnit);

        public static double ConvertWindSpeed(Units originalUnits, SpeedUnit toUnit, double value) =>
            Speed.From(value, originalUnits.WindSpeedUnit).As(toUnit);

        public static double ConvertDistance(Units originalUnits, LengthUnit toUnit, double value) =>
            Length.From(value, originalUnits.VisibilityUnit).As(toUnit);
    }
}