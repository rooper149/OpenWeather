using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace OpenWeather.Example.Uwp
{
    public sealed partial class Forecasts : UserControl
    {

        public ObservableCollection<Models.Station> StationCollection { get; }
        public Models.Station SelectedStation { get; set; }

        public Forecasts()
        {
            this.InitializeComponent();
            StationCollection = new ObservableCollection<Models.Station>();
            Global.StationsUpdated += Global_StationsUpdated;
            Global_StationsUpdated(null, null);
        }

        private void Global_StationsUpdated(object sender, EventArgs e)
        {
            StationCollection.Clear();
            if (Global.Stations == null) return;
            Global.Stations.Where(x => x.CountryCode == "US").OrderBy(x => x.Name).ToList().ForEach(x => StationCollection.Add(x));
            SelectedStation = StationCollection.SingleOrDefault(x => x.Name == "GOSHEN");
            MaxTempCheckBox.IsChecked = true;
            MinTempCheckBox.IsChecked = true;
            HumidityCheckBox.IsChecked = true;
            SnowCheckBox.IsChecked = true;
            IceCheckBox.IsChecked = true;
        }

        private async void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Noaa.Api api = new Noaa.Api();
            ResultTextBox.Text = String.Empty;

            ResultTextBox.Text = await api.GetForecastByStationAsync(SelectedStation, DateTime.Now, DateTime.Now.AddDays(25), Noaa.RequestType.Forcast, Noaa.Units.Imperial, new Models.WeatherParameters()
            {
                IsMaximumTempuratureRequested = MaxTempCheckBox.IsChecked.HasValue ? MaxTempCheckBox.IsChecked.Value : false,
                IsMinimumTempuratureRequested = MinTempCheckBox.IsChecked.HasValue ? MinTempCheckBox.IsChecked.Value : false,
                IsRelativeHumidityRequested = HumidityCheckBox.IsChecked.HasValue ? HumidityCheckBox.IsChecked.Value : false,
                IsSnowFallAmountRequested = SnowCheckBox.IsChecked.HasValue ? SnowCheckBox.IsChecked.Value : false,
                IsIceAccumulationRequested = IceCheckBox.IsChecked.HasValue ? IceCheckBox.IsChecked.Value : false
            });
        }
    }

}
