using OpenWeather.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenWeather.Core
{
    internal class StationFileParser
    {
        private readonly Uri _uri;

        public StationFileParser(string uri)
        {
            _uri = new Uri(uri);
        }

        internal async Task<IEnumerable<Station>> GetStationsAsync()
        {

            if (await Task.FromResult(File.Exists("Stations.txt")) == false)
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = await client.GetAsync("https://www.aviationweather.gov/static/adds/metars/stations.txt"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            FileInfo fileIo = new FileInfo("Stations.txt");
                            using (FileStream fs = new FileStream("Stations.txt", FileMode.Create, FileAccess.Write))
                            {
                                using (StreamWriter writer = new StreamWriter(fs))
                                {
                                    using (StreamReader reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                                    {
                                        await writer.WriteAsync(await reader.ReadToEndAsync());
                                    }
                                }
                            }

                        }
                    }
                }
            }

            List<Station> stations = new List<Station>();

            using (FileStream fs = new FileStream("Stations.txt", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    while (reader.Peek() != -1)
                    {
                        string line = await reader.ReadLineAsync();
                        // fileter comments
                        if (line.StartsWith("!")) continue;
                        // fileter short lines
                        if (line.Length < 83) continue;

                        // we have a potientally valid line.
                        Station station = new Station();
                        string synopValue = null;
                        string latitudeValue = null;
                        string longitudeValue = null;
                        string elevationValue = null;
                        string priorityValue = null;

                        if (line.Length >= 2) station.StateOrProvince = line.Substring(0, 2).Trim();
                        if (line.Length >= 19) station.Name = line.Substring(3, 16).Trim();
                        if (line.Length >= 25) station.ICAO = line.Substring(20, 4).Trim();
                        if (line.Length >= 30) station.IATA = line.Substring(26, 3).Trim();
                        if (line.Length >= 38) synopValue = line.Substring(32, 5).Trim();
                        if (line.Length >= 45) latitudeValue = line.Substring(39, 7).Trim();
                        if (line.Length >= 53) longitudeValue = line.Substring(47, 7).Trim();
                        if (line.Length >= 59) elevationValue = line.Substring(55, 4).Trim();
                        if (line.Length >= 64) station.IsMetar = line.Substring(62, 2).Trim() == "X";
                        if (line.Length >= 67) station.IsNexRad = line.Substring(65, 2).Trim() == "X";
                        if (line.Length >= 70) station.AviationIndicator = line.Substring(68, 2).Trim();
                        if (line.Length >= 73) station.UpperAiraIndicator = line.Substring(71, 2).Trim();
                        if (line.Length >= 76) station.AutomationIndicator = line.Substring(74, 2).Trim();
                        if (line.Length >= 79) station.OfficeTypeIndicator = line.Substring(77, 2).Trim();
                        if (line.Length >= 81) priorityValue = line.Substring(79, 2).Trim();
                        if (line.Length >= 83) station.CountryCode = line.Substring(81, 2).Trim();

                        int synop = 0;
                        Int32.TryParse(synopValue, out synop);
                        station.SYNOP = synop;

                        double latitude = 0, longitude = 0, elevation = 0;
                        latitudeValue = latitudeValue.Replace(" ", ".");
                        longitudeValue = longitudeValue.Replace(" ", ".");

                        double.TryParse(latitudeValue.Substring(0, latitudeValue.Length - 1), out latitude);
                        if (latitudeValue.EndsWith("N")) latitude *= -1;
                        station.Latitude = latitude;

                        double.TryParse(longitudeValue.Substring(0, longitudeValue.Length - 1), out longitude);
                        if (longitudeValue.EndsWith("W")) longitude *= -1;
                        station.Longitude = longitude;

                        double.TryParse(elevationValue, out elevation);
                        station.Elevation = elevation;

                        int priority = 0;
                        Int32.TryParse(priorityValue, out priority);
                        station.Priority = priority;

                        stations.Add(station);
                    }
                }
            }

            return stations;
        }

    }
}
