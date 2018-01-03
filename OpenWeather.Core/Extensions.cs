using System;

namespace OpenWeather.Core
{
    public static class Extensions
    {
        public static double ToDouble(this string s, double defaultValue = 0)
        {
            double value = defaultValue;
            Double.TryParse(s, out value);

            return value;
        }
    }
}
