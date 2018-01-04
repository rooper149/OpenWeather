using OpenWeather.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.UI.Xaml.Controls;


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace OpenWeather.Example.Uwp
{
    public sealed partial class Stations : UserControl
    {
        public Stations()
        {
            this.InitializeComponent();
        }


        private async void UserControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            IEnumerable<Station> stations = null;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Station>));
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

            if (await storageFolder.TryGetItemAsync("Stations.dat") != null)
            {
                stations = (List<Station>)xmlSerializer.Deserialize(await storageFolder.OpenStreamForReadAsync("Stations.dat"));
            }
            else
            {
                NoaaApi api = new NoaaApi(null);
                stations = await api.GetStationsAsync();
                xmlSerializer.Serialize(await storageFolder.OpenStreamForWriteAsync("Stations.dat", CreationCollisionOption.ReplaceExisting), stations);
            }


            cvs.Source = stations
                .OrderBy(x => x.CountryCode)
                .ThenBy(x => x.Name)
                .GroupBy(x => Bia.Countries.Iso3166.Countries.GetCountryByAlpha2(x.CountryCode));
        }
    }
}
