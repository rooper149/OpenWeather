using OpenWeather.Noaa.Primitives;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OpenWeather.Noaa.Alerts
{
    public class WeatherAlertArea
    {
        /// <summary>
        /// Required. A user-friendly name for the area affected.
        /// </summary>
        [Required]
        public string Description { get; internal set; }

        /// <summary>
        /// Where Lat,Lon is a latitude and longitude coordinate pair. 
        /// A minimum of 4 coordinate pairs is present. 
        /// The first and last pair will always be the same.
        /// </summary>
        public IEnumerable<GeoPoint2D> Polygon { get; internal set; }

       
    }
}
