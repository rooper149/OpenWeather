using System;
using System.Xml;
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
            DateTime value = XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.Local);
            if (value == null)
                return defaultValue.Value;
            else
                return value;
        }

        public static TimeSpan FromXmlDurationToTimeSpan(this string s, TimeSpan? defaultValue = null)
        {
            if (!defaultValue.HasValue) defaultValue = new TimeSpan();
            TimeSpan value = XmlConvert.ToTimeSpan(s);
            if (value == null) value = defaultValue.Value;

            return value;
        }

        public static Uri ToUri(this string s, Uri defaultValue = null)
        {
            Uri value = defaultValue;
            Uri.TryCreate(s, UriKind.Absolute, out value);

            return value;
        }

        public static bool IsNull(this object o)
        {
            return o == null;
        }

        public static string ValueIfExists(this XElement element)
        {
            if (element != null)
                return element.Value;
            else
                return null;
        }

        public static string ValueIfExists(this XAttribute attribute)
        {
            if (attribute != null)
                return attribute.Value;
            else
                return null;
        }
    }
}
