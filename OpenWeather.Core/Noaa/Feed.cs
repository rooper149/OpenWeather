using System.Xml.Serialization;

namespace OpenWeather.Noaa
{
    [XmlRoot(ElementName = "feed", Namespace = "http://www.w3.org/2005/Atom")]
    public class Feed
    {
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }
    }
}
