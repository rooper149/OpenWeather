using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OpenWeather.Common
{
    internal struct ZipCodesCityStateCounty
    {
        internal string ZipCode;
        internal string City;
        internal string State;
        internal string County;
    }

    internal class ZipCodesCityStateCountiesHelper : IDisposable
    {

        internal async Task<IEnumerable<ZipCodesCityStateCounty>> GetZipCodesCityStateCountiesAsync()
        {
            string resourceName = "OpenWeather.Resources.ZipCodes_City_State_County.csv";
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<ZipCodesCityStateCounty> list = new List<ZipCodesCityStateCounty>();

            // read all the zip codes
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (reader.Peek() != -1)
                    {
                        string line = await reader.ReadLineAsync();
                        if (String.IsNullOrWhiteSpace(line)) continue;
                        if (line == "Zip Code,Place Name,State Abbreviation,County") continue;

                        ZipCodesCityStateCounty zipCodesCityStateCounty = new ZipCodesCityStateCounty();
                        string[] values = line.Split(',');

                        if (values.Length > 0) zipCodesCityStateCounty.ZipCode = values[0];
                        if (values.Length > 1) zipCodesCityStateCounty.City = values[1];
                        if (values.Length > 2) zipCodesCityStateCounty.State = values[2];
                        if (values.Length > 3) zipCodesCityStateCounty.County = values[3];

                        list.Add(zipCodesCityStateCounty);
                    }
                }
            }

            return list;
        }

        internal async Task<ZipCodesCityStateCounty?> GetZipCodesCityStateCountyByZipCode(string zipCode)
        {
            IEnumerable<ZipCodesCityStateCounty> zipCodesCityStateCounties = await GetZipCodesCityStateCountiesAsync();
            if (zipCodesCityStateCounties == null)
                return null;
            else
                return zipCodesCityStateCounties
                    .Where(x => x.ZipCode == zipCode)
                    .SingleOrDefault();
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

                disposedValue = true;
            }
        }


        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion

    }
}
