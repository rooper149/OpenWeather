using System;
using System.Xml.Linq;

namespace OpenWeather.Noaa.Models
{
    public class DwmlHead
    {
        public DwmlProduct Product { get; internal set; }
        public DwmlSource Source { get; internal set; }


        public DwmlHead(XElement headElement)
        {
            if (headElement == null) return;
            Product = new DwmlProduct(headElement.Element("product"));
            Source = new DwmlSource(headElement.Element("source"));
        }

    }

    public class DwmlProduct
    {
        public string SrsName { get; set; }
        public string ConciseName { get; set; }
        public string OperationalMode { get; set; }
        public string Title { get; set; }
        public string Field { get; set; }
        public string Category { get; set; }
        public DateTime CreationDate { get; set; }
        public TimeSpan RefreshFrequency { get; set; }

        #region Constructors

        public DwmlProduct()
        {

        }

        public DwmlProduct(XElement productElement)
        {
            if (productElement == null) return;

            SrsName = productElement.Attribute("srsName").ValueIfExists();
            ConciseName = productElement.Attribute("concise-name").ValueIfExists();
            OperationalMode = productElement.Attribute("operational-mode").ValueIfExists();
            Title = productElement.Element("title").ValueIfExists();
            Field = productElement.Element("field").ValueIfExists();
            Category = productElement.Element("category").ValueIfExists();

            XElement creationDateElement = productElement.Element("creation-date");
            if (creationDateElement != null)
            {
                CreationDate = creationDateElement.ValueIfExists().ToDateTime();
                RefreshFrequency = creationDateElement.Attribute("refresh-frequency").ValueIfExists().FromXmlDurationToTimeSpan();
            }
        }

        #endregion
    }

    public class DwmlSource
    {
        public Uri MoreInformation { get; set; }
        public string ProductionCenter { get; set; }
        public string SubCenter { get; set; }
        public Uri Disclaimer { get; set; }
        public Uri Credit { get; set; }
        public Uri CreditLogo { get; set; }
        public Uri Feedback { get; set; }

        #region Constructors

        public DwmlSource()
        {

        }

        public DwmlSource(XElement sourceElement)
        {
            if (sourceElement == null) return;
            MoreInformation = sourceElement.Element("more-information").ValueIfExists().ToUri();
            ProductionCenter = sourceElement.Element("production-center").ValueIfExists();
            SubCenter = sourceElement.Element("production-center").Element("sub-center").ValueIfExists();
            Disclaimer = sourceElement.Element("disclaimer").ValueIfExists().ToUri();
            Credit = sourceElement.Element("credit").ValueIfExists().ToUri();
            CreditLogo = sourceElement.Element("credit-logo").ValueIfExists().ToUri();
            Feedback = sourceElement.Element("feedback").ValueIfExists().ToUri();

            if (!String.IsNullOrWhiteSpace(SubCenter)) ProductionCenter = ProductionCenter.Replace(SubCenter, "");
        }

        #endregion  
    }

}
