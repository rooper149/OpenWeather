using System;
using System.Xml;
using System.Xml.Linq;

namespace OpenWeather
{
    internal static class Extensions
    {
        //public static double? ToDouble(this string s, double? defaultValue = null)
        //{
        //    double value = 0;
        //    if (!String.IsNullOrWhiteSpace(s) && Double.TryParse(s, out value))
        //        return value;
        //    else
        //        return defaultValue;
        //}

        public static decimal? ToDecimal(this string s, decimal? defaultValue = null)
        {
            decimal value = 0;
            if (!String.IsNullOrWhiteSpace(s) && decimal.TryParse(s, out value))
                return value;
            else
                return defaultValue;
        }

        public static DateTime ToDateTime(this string s, DateTime? defaultValue = null)
        {
            if (!defaultValue.HasValue) defaultValue = new DateTime();
            DateTime? value = (DateTime?)Convert.ToDateTime(s);
            //DateTime value = XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.Local);
            if (value == null)
                return defaultValue.Value;
            else
                return value.Value;
        }

        public static DateTime? ToNullableDateTime(this string s, DateTime? defaultValue = null)
        {
            if (!defaultValue.HasValue) defaultValue = new DateTime();
            DateTime? value = (DateTime?)Convert.ToDateTime(s);
            //DateTime value = XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.Local);
            if (value == null)
                return defaultValue.Value;
            else
                return value.Value;
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
