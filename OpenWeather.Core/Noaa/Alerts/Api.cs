using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace OpenWeather.Noaa.Alerts
{
    /// <summary>
    /// A container for retrieving all NOAA alerts
    /// </summary>
    public class Api : IDisposable
    {
        public event EventHandler<NewAlertEventArgs> NewAlerts;

        /// <summary>
        /// Automatically checks for alerts; increasing the interval as the threat increases, and lowering as the threat decreases.
        /// </summary>
        /// <param name="zipCode">The zip code to track.</param>
        public async Task CheckForAlertsByZipCodeAsync(string zipCode)
        {
            int interval = 10000;
            IEnumerable<WeatherAlert> weatherAlerts = null;

            while (true)
            {
                weatherAlerts = await GetWeatherAlertByZipCodeAndInvervalAsync(zipCode, interval);
                if (weatherAlerts == null) continue;

                if (weatherAlerts.Count(x => x.Information.Severity == WeatherAlertSeverities.Minor) > 0) interval = 10000;
                if (weatherAlerts.Count(x => x.Information.Severity == WeatherAlertSeverities.Moderate) > 0) interval = 5000;
                if (weatherAlerts.Count(x => x.Information.Severity == WeatherAlertSeverities.Severe) > 0) interval = 3000;
                if (weatherAlerts.Count(x => x.Information.Severity == WeatherAlertSeverities.Extreme) > 0) interval = 1000;
            }

        }

        /// <summary>
        /// Retrieves an <see cref="IEnumerable{WeatherAlert}"/> checking every interval.
        /// </summary>
        /// <param name="zipCode">The zip code to check</param>
        /// <param name="seconds">The interval (in seconds) to retry.</param>
        /// <returns><see cref="IEnumerable{WeatherAlert}"/> containing the alerts fro the chosen zip code; if any.</returns>
        public async Task<IEnumerable<WeatherAlert>> GetWeatherAlertByZipCodeAndInvervalAsync(string zipCode, int seconds)
        {
            if (String.IsNullOrWhiteSpace(zipCode)) return null;
            if (seconds == 0) return null;

            IEnumerable<WeatherAlert> weatherAlerts = null;

            while (weatherAlerts == null)
            {
                weatherAlerts = await GetWeatherAlertByZipCodeAsync(zipCode);
            }

            return weatherAlerts;
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{WeatherAlert}"/> for a given zip code.
        /// </summary>
        /// <param name="zipCode">The zip code to check for alerts.</param>
        /// <returns>An <see cref="IEnumerable{WeatherAlert}"/> containing the latest alerts, or null if none.</returns>
        public async Task<IEnumerable<WeatherAlert>> GetWeatherAlertByZipCodeAsync(string zipCode)
        {
            if (String.IsNullOrWhiteSpace(zipCode)) return null;
            Common.ZipCodesCityStateCounty? model = null;

            using (Common.ZipCodesCityStateCountiesHelper helper = new Common.ZipCodesCityStateCountiesHelper())
            {
                model = await helper.GetZipCodesCityStateCountyByZipCode(zipCode);
            }

            if (!model.HasValue)
                return null;
            else
                return await GetWeatherAlertByCountyAndStateAsync(model.Value.County, model.Value.State);
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{WeatherAlert}"/> for a given county and state.
        /// </summary>
        /// <param name="zipCode">The zip code to check for alerts.</param>
        /// <param name="stateAbbreviation">The two-character state abbreviation. (e.g. CA)</param>
        /// <returns>An <see cref="IEnumerable{WeatherAlert}"/> containing the latest alerts, or null if none.</returns>
        public async Task<IEnumerable<WeatherAlert>> GetWeatherAlertByCountyAndStateAsync(string county, string stateAbbreviation)
        {
            if (String.IsNullOrWhiteSpace(county)) return null;
            if (String.IsNullOrWhiteSpace(stateAbbreviation)) return null;

            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "OpenWeather.Resources.NOAA_County_CountyCode.csv";
            List<Models.Resources.NOAA_County_CountyCode> list = new List<Models.Resources.NOAA_County_CountyCode>();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (reader.Peek() != -1)
                    {
                        string line = await reader.ReadLineAsync();
                        if (String.IsNullOrWhiteSpace(line)) continue;

                        Models.Resources.NOAA_County_CountyCode nOAA_County_CountyCode = new Models.Resources.NOAA_County_CountyCode();
                        string[] values = line.Split(',');

                        if (values.Length > 0) nOAA_County_CountyCode.CountyCode = values[0];
                        if (values.Length > 1) nOAA_County_CountyCode.County = values[1];
                        if (values.Length > 2) nOAA_County_CountyCode.State_ShortName = values[2];
                        if (values.Length > 3) nOAA_County_CountyCode.State_LongName = values[3];

                        list.Add(nOAA_County_CountyCode);
                    }
                }
            }

            if (list.Count == 0) return null;

            string countyCode = list
                .Where(c => c.County?.ToLower() == county.ToLower())
                .Where(c => c.State_ShortName?.ToLower() == stateAbbreviation.ToLower())
                .Select(c => c.CountyCode).SingleOrDefault();

            if (String.IsNullOrWhiteSpace(countyCode)) return null;

            return await GetWeatherAlertByCountyCodeAsync(countyCode);
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{WeatherAlert}"/> for a given NOAA county code.
        /// </summary>
        /// <param name="countyCode">The county code to check for alerts.</param>
        /// <returns>An <see cref="IEnumerable{WeatherAlert}"/> containing the latest alerts, or null if none.</returns>
        public async Task<IEnumerable<WeatherAlert>> GetWeatherAlertByCountyCodeAsync(string countyCode)
        {
            Uri requestUri = new Uri($"https://alerts.weather.gov/cap/wwaatmget.php?x={countyCode}&y=1");
            string response = null;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("Dynamensions_Weather", "1.0")));
                response = await httpClient.GetStringAsync(requestUri);
            }

            return await GetWeatherAlertByXmlStringAsync(response);
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{WeatherAlert}"/> for a given XML file path.
        /// </summary>
        /// <param name="xmlFilePath">The file path to the NOAA alert XML file.</param>
        /// <returns>An <see cref="IEnumerable{WeatherAlert}"/> containing the latest alerts, or null if none.</returns>
        public async Task<IEnumerable<WeatherAlert>> GetWeatherAlertByXmlFileAsync(string xmlFilePath)
        {
            string xmlString = null;
            if (String.IsNullOrWhiteSpace(xmlFilePath)) return null;

            using (FileStream fs = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    xmlString = await reader.ReadToEndAsync();
                }
            }

            return await GetWeatherAlertByXmlStringAsync(xmlString);
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{WeatherAlert}"/> for a given NOAA atom feed XML string.
        /// </summary>
        /// <param name="xmlString">The string containing the alert information.</param>
        /// <returns></returns>
        public async Task<IEnumerable<WeatherAlert>> GetWeatherAlertByXmlStringAsync(string xmlString)
        {
            if (String.IsNullOrWhiteSpace(xmlString)) return null;
            using (AlertParser parser = new AlertParser())
            {
                return await Task.FromResult(parser.ParseAlerts(xmlString));
            }
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
        // ~Api() {
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
