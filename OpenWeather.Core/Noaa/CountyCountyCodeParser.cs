using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace OpenWeather.Noaa
{
    internal struct NOAA_County_CountyCode
    {
        internal string CountyCode;
        internal string County;
        internal string State_ShortName;
        internal string State_LongName;
    }

    internal class CountyCountyCodeParser : IDisposable
    {
        internal async Task<IEnumerable<NOAA_County_CountyCode>> GetZipCodesCityStateCountiesAsync()
        {
            string resourceName = "OpenWeather.Resources.ZipCodes_City_State_County.csv";
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<NOAA_County_CountyCode> list = new List<NOAA_County_CountyCode>();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (reader.Peek() != -1)
                    {
                        string line = await reader.ReadLineAsync();
                        if (String.IsNullOrWhiteSpace(line)) continue;

                        NOAA_County_CountyCode nOAA_County_CountyCode = new NOAA_County_CountyCode();
                        string[] values = line.Split(',');

                        if (values.Length > 0) nOAA_County_CountyCode.CountyCode = values[0];
                        if (values.Length > 1) nOAA_County_CountyCode.County = values[1];
                        if (values.Length > 2) nOAA_County_CountyCode.State_ShortName = values[2];
                        if (values.Length > 3) nOAA_County_CountyCode.State_LongName = values[3];

                        list.Add(nOAA_County_CountyCode);
                    }
                }
            }

            return list;
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
        // ~CountyCountyCodeParser() {
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
