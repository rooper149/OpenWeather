using System;
using System.Xml.Linq;

namespace OpenWeather
{
    public static class Extensions
    {
        public static double ToDouble(this string s, double defaultValue = 0)
        {
            double value = defaultValue;
            Double.TryParse(s, out value);

            return value;
        }

        public static DateTime ToDateTime(this string s, DateTime? defaultValue = null)
        {
            if (!defaultValue.HasValue) defaultValue = new DateTime();
            DateTime value = defaultValue.Value;
            DateTime.TryParse(s, out value);

            return value;
        }

        public static string ValueIfExists(this XElement element)
        {
            if (element != null)
                return element.Value;
            else
                return null;
        }
    }
}
