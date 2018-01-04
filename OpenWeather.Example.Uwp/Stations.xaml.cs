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
            StorageFile storageFile = await storageFolder.CreateFileAsync("Stations.dat", CreationCollisionOption.OpenIfExists);

            if (storageFile != null)
            {
                stations = (List<Station>)xmlSerializer.Deserialize(await storageFile.OpenStreamForReadAsync());
            }
            else
            {
                NoaaApi api = new NoaaApi(null);
                stations = await api.GetStationsAsync();
                xmlSerializer.Serialize(await storageFile.OpenStreamForWriteAsync(), stations);
            }

            cvs.Source = stations.OrderBy(x => x.Name).GroupBy(x => x.Name.Substring(0, 1));
        }
    }
}
