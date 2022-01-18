using System.Xml.Linq;

namespace OpenWeather
{
    /// <summary>
    /// Static set of tools for getting Xml data
    /// </summary>
    internal static class XmlTools
    {
        /// <summary>
        /// Returns the attribute value of an XElement.
        /// </summary>
        /// <param name="attribute">The attribute from which to obtain the value.</param>
        /// <param name="doc"></param>
        /// <param name="elements">Name of elements to navigate.</param>
        /// <returns>The value from the attribute as a string.</returns>
        public static string? GetAttributeValue(string attribute, XDocument doc, params string[] elements)
        {
            var xAttribute = GetNode(doc, elements)?.Attribute(attribute);
            return xAttribute?.Value;
        }

        /// <summary>
        /// Returns the contents of an XElement.
        /// </summary>
        /// <param name="targetElement">The element from which to obtain the value.</param>
        /// <param name="doc"></param>
        /// <param name="elements">Name of elements to navigate.</param>
        /// <returns>The value from the attribute as a string.</returns>
        public static string? GetElementContent(string targetElement, XDocument doc, params string[] elements)
        {
            var content = GetNode(doc, elements)?.Element(targetElement)?.Value;
            return content;
        }

        /// <summary>
        /// Returns the last nodes from a path of XElements.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="elements">Name of elements to navigate.</param>
        /// <returns></returns>
        public static XElement? GetNode(XDocument doc, params string[] elements)
        {
            var node = doc.Root;
            node = elements.Aggregate(node, (current, value) => current?.Element(value));
            return node;
        }

        /// <summary>
        /// Returns the last nodes from a path of XElements.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="element">Name of element to navigate.</param>
        /// <param name="attribute">attribute within the element</param>
        /// <param name="value">value of the attrivute</param>
        /// <returns>XDocument</returns>
        public static XDocument TruncateXDocument(XDocument doc, string element, string attribute, string value)
        {
            var node = doc.Root;

            var address =
                from el in node?.Elements(element)
                where (string?)el.Attribute(attribute) == value
                select el;

            var str = address.Aggregate(string.Empty, (current, xElement) => current + xElement.ToString());

            return XDocument.Parse(str);
        }
    }
}
