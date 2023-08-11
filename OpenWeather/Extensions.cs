namespace OpenWeather
{
    public static class Extensions
    {
        public static TafStation AsTafStation(this StationInfo stationInfo, bool updateNow = true, bool autoUpdate = false)
        {
            return new TafStation(stationInfo.Latitude, stationInfo.Longitude, updateNow, autoUpdate);
        }

        public static MetarStation AsMetarStation(this StationInfo stationInfo, bool updateNow = true, bool autoUpdate = false)
        {
            return new MetarStation(stationInfo, updateNow, autoUpdate);
        }
    }
}
