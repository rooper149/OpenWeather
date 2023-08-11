namespace OpenWeather
{
    public struct Location
    {
        public readonly double Latitude;
        public readonly double Longitude;

        public Location(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        /// <summary>
        /// Gets aprox distance in km from one location to another.
        /// </summary>
        /// <returns>Distance in km</returns>
        public double DistanceTo(Location location)
        {
            double rad(double angle) => angle * 0.017453292519943295769236907684886127d; // = angle * Math.Pi / 180.0d
            double havf(double diff) => Math.Pow(Math.Sin(rad(diff) / 2d), 2); // = sin²(diff / 2)
            return 12745.6 * Math.Asin(Math.Sqrt(havf(location.Latitude - Latitude) + Math.Cos(rad(Latitude)) * Math.Cos(rad(location.Latitude)) * havf(location.Longitude - Longitude)));
        }
    }
}
