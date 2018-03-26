using OpenWeather.Noaa.Primitives;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace OpenWeather.Noaa.Alerts
{
    internal class AlertParser : IDisposable
    {

        internal IEnumerable<WeatherAlert> ParseAlerts(string result)
        {
            List<WeatherAlert> weatherAlerts = new List<WeatherAlert>();

            XDocument document = XDocument.Parse(result);
            // define name spaces
            XNamespace atomNameSpace = "http://www.w3.org/2005/Atom";
            XNamespace capNamespace = "urn:oasis:names:tc:emergency:cap:1.1";
            XNamespace haNameSpace = "http://www.alerting.net/namespace/index_1.0";

            IEnumerable<XElement> elements = document.Root.Elements();

            // does the feed element exist?
            XElement feedElement = document.Root;
            if (feedElement == null) return null;

            IEnumerable<XElement> entryElements = feedElement.Elements(atomNameSpace + "entry");

            foreach (XElement element in entryElements)
            {
                WeatherAlert weatherAlert = new WeatherAlert();
                weatherAlert.Information = new WeatherAlertInformation();
                weatherAlert.Area = new WeatherAlertArea();

                weatherAlert.Id = element.Element(atomNameSpace + "id").ValueIfExists();
                weatherAlert.Updated = element.Element(atomNameSpace + "updated").ValueIfExists().ToDateTime();
                weatherAlert.Published = element.Element(atomNameSpace + "published").ValueIfExists().ToDateTime();
                weatherAlert.Title = element.Element(atomNameSpace + "title").ValueIfExists();
                weatherAlert.Link = element.Element(atomNameSpace + "link").Attribute("href").ValueIfExists().ToUri();
                weatherAlert.Summary = element.Element(atomNameSpace + "summary").ValueIfExists();
                weatherAlert.Event = element.Element(capNamespace + "event").ValueIfExists();
                weatherAlert.Effective = element.Element(capNamespace + "effective").ValueIfExists().ToNullableDateTime();
                weatherAlert.Expires = element.Element(capNamespace + "expires").ValueIfExists().ToNullableDateTime();

                weatherAlert.Information.Categories = ParseCategories(element.Elements(capNamespace + "category"));

                weatherAlert.Area.Description = element.Element(capNamespace + "areaDesc").ValueIfExists();
                weatherAlert.Area.Polygon = ParsePolygon(element.Element(capNamespace + "polygon"));


                AlertStatusValues statusValue;
                Enum.TryParse(element.Element(capNamespace + "status").ValueIfExists(), out statusValue);
                weatherAlert.Status = statusValue;

                AlertMessageTypeValues messageTypeValue;
                Enum.TryParse(element.Element(capNamespace + "msgType").ValueIfExists(), out messageTypeValue);
                weatherAlert.MessageType = messageTypeValue;

                WeatherAlertUrgencies alertUrgency;
                Enum.TryParse(element.Element(capNamespace + "urgency").ValueIfExists(), out alertUrgency);
                weatherAlert.Information.Urgency = alertUrgency;

                WeatherAlertSeverities alertSeverity;
                Enum.TryParse(element.Element(capNamespace + "severity").ValueIfExists(), out alertSeverity);
                weatherAlert.Information.Severity = alertSeverity;

                WeatherAlertCertainties alertCertainty;
                Enum.TryParse(element.Element(capNamespace + "certainty").ValueIfExists(), out alertCertainty);
                weatherAlert.Information.Certainty = alertCertainty;

                XElement authorElement = element.Element(atomNameSpace + "author");
                if (authorElement != null)
                {
                    weatherAlert.Sender = authorElement.Element(atomNameSpace + "name").ValueIfExists();
                }

                if (!String.IsNullOrWhiteSpace(weatherAlert.Title) && weatherAlert.Title.Contains("There are no active watches, warnings or advisories")) continue;
                if (!String.IsNullOrWhiteSpace(weatherAlert.Summary) && weatherAlert.Summary.Contains("There are no active watches, warnings or advisories")) continue;

                weatherAlerts.Add(weatherAlert);
            }


            return weatherAlerts;
        }

        private IEnumerable<WeatherAlertCategories> ParseCategories(IEnumerable<XElement> categoryElements)
        {
            List<WeatherAlertCategories> alertCategories = new List<WeatherAlertCategories>();
            foreach (XElement categoryElement in categoryElements)
            {
                WeatherAlertCategories? category = null;
                string categoryValue = categoryElement.ValueIfExists();
                switch (categoryValue)
                {
                    case "GEO":
                        category = WeatherAlertCategories.Geophysical;
                        break;
                    case "MET":
                        category = WeatherAlertCategories.Meteorological;
                        break;
                    case "Safety":
                        category = WeatherAlertCategories.Safety;
                        break;
                    case "Security":
                        category = WeatherAlertCategories.Security;
                        break;
                    case "Rescue":
                        category = WeatherAlertCategories.Rescue;
                        break;
                    case "Fire":
                        category = WeatherAlertCategories.Fire;
                        break;
                    case "Health":
                        category = WeatherAlertCategories.Health;
                        break;
                    case "Env":
                        category = WeatherAlertCategories.Environmental;
                        break;
                    case "Transport":
                        category = WeatherAlertCategories.Transportation;
                        break;
                    case "Infra":
                        category = WeatherAlertCategories.Infrastructure;
                        break;
                    case "CBRNE":
                        category = WeatherAlertCategories.CBRNE;
                        break;
                    case "Other":
                        category = WeatherAlertCategories.Other;
                        break;
                    default:
                        break;
                }

                if (category.HasValue) alertCategories.Add(category.Value);
            }

            if (alertCategories.Count > 0)
                return alertCategories;
            else
                return null;
        }

        private IEnumerable<GeoPoint2D> ParsePolygon(XElement polygonElement)
        {
            // does the element exist and have a value?
            string polygonValue = polygonElement.ValueIfExists();
            if (String.IsNullOrWhiteSpace(polygonValue)) return null;

            // does the value have at least two points?
            string[] points = polygonValue.Split(' ');
            if (points == null || points.Length < 2) return null;

            List<GeoPoint2D> pointList = new List<GeoPoint2D>();
            foreach (string point in points)
            {
                string[] values = point.Split(',');
                if (values == null || values.Length < 2) continue;

                decimal? latitude = values[0].ToDecimal();
                decimal? longitude = values[1].ToDecimal();

                pointList.Add(new GeoPoint2D()
                {
                    Latitude = (double)(latitude.HasValue ? latitude.Value : 0),
                    Longitude = (double)(longitude.HasValue ? longitude.Value : 0)
                });
            }

            return pointList;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AlertParser() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
