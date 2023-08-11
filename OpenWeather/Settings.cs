using System.Reflection;

namespace OpenWeather
{
    public static class Settings
    {
        /// <summary>
        /// The time interval in seconds that all stations use for updates
        /// </summary>
        public static int _UpdateIntervalSeconds { get; set; } = 1800;

        /// <summary>
        /// The useragent used for all Http requests
        /// </summary>
        public static string? _UserAgent { get; set; } = $@"{nameof(OpenWeather)}/{Assembly.GetExecutingAssembly().GetName().Version}";
    }
}
