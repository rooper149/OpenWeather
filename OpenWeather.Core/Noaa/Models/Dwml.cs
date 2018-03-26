using System.Xml.Linq;

namespace OpenWeather.Noaa.Models
{
    public class Dwml
    {
        private XDocument _document = null;

        public DwmlHead Head { get; private set; }
        public DwmlData Data { get; private set; }

        public Dwml(string result)
        {
            _document = XDocument.Parse(result);
            PrepareData();
        }

        public Dwml(XDocument document)
        {
            _document = document;
            PrepareData();
        }

        private void PrepareData()
        {
            if (_document == null) return;

            XElement dwmlElement = _document.Element("dwml");
            if (dwmlElement == null) return;

            Head = new DwmlHead(dwmlElement.Element("head"));
            Data = new DwmlData(dwmlElement.Element("data"));
        }

    }
}
