using System.Linq;
using System.Xml.Linq;

namespace OpenWeather
{
    public static class XmlTools
    {
        /// <summary>
        ///     Returns the attribute value of an XElement.
        /// </summary>
        /// <param name="attribute">The attribute from which to obtain the value.</param>
        /// <param name="doc"></param>
        /// <param name="elements">Name of elements to navigate.</param>
        /// <returns>The value from the attribute as a string.</returns>
        public static string GetAttributeValue(string attribute, XDocument doc, params string[] elements)
        {
            try
            {
                var xAttribute = GetNode(doc, elements).Attribute(attribute);
                if (xAttribute != null)
                    return xAttribute.Value;
            }
            catch
            {
                // ignored
            }

            return null;
        }

        /// <summary>
        ///     Returns the contents of an XElement.
        /// </summary>
        /// <param name="targetElement">The element from which to obtain the value.</param>
        /// <param name="doc"></param>
        /// <param name="elements">Name of elements to navigate.</param>
        /// <returns>The value from the attribute as a string.</returns>
        public static string GetElementContent(string targetElement, XDocument doc, params string[] elements)
        {
            try
            {
                var content = GetNode(doc, elements).Element(targetElement)?.Value;
                if (content != null)
                    return content;
            }
            catch
            {
                // ignored
            }

            return null;
        }

        /// <summary>
        ///     Returns the last nodes from a path of XElements.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="elements">Name of elements to navigate.</param>
        /// <returns></returns>
        public static XElement GetNode(XDocument doc, params string[] elements)
        {
            var node = doc.Root;

            try
            {
                node = elements.Aggregate(node, (current, value) => current.Element(value));
            }
            catch
            {
                // ignored
            }

            return node;
        }
    }
}