using OpenWeather.Noaa.Base;
using OpenWeather.Noaa.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace OpenWeather.Example.Uwp
{
    public sealed partial class Forecasts : UserControl
    {

        public ObservableCollection<Station> StationCollection { get; }
        public Station SelectedStation { get; set; }

        public Forecasts()
        {
            this.InitializeComponent();
            StationCollection = new ObservableCollection<Station>();
            Global.StationsUpdated += Global_StationsUpdated;
            Global_StationsUpdated(null, null);
        }

        private void Global_StationsUpdated(object sender, EventArgs e)
        {
            StationCollection.Clear();
            if (Global.Stations == null) return;
            Global.Stations.Where(x => x.CountryCode == "US").OrderBy(x => x.Name).ToList().ForEach(x => StationCollection.Add(x));
            SelectedStation = StationCollection.FirstOrDefault(x => x.Name.Contains("FORT WAYNE"));
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
            DateTime startDateTime = DateTime.Today.AddHours(DateTime.Now.Hour);
            DateTime endDateTime = DateTime.Now.AddDays(25);

            Forecast forecast = await api.GetForecastByStationAsync(SelectedStation, startDateTime, endDateTime, RequestType.Forcast, Units.Imperial, new WeatherParameters()
            {
                IsMaximumTempuratureRequested = MaxTempCheckBox.IsChecked.HasValue ? MaxTempCheckBox.IsChecked.Value : false,
                IsMinimumTempuratureRequested = MinTempCheckBox.IsChecked.HasValue ? MinTempCheckBox.IsChecked.Value : false,
                IsRelativeHumidityRequested = HumidityCheckBox.IsChecked.HasValue ? HumidityCheckBox.IsChecked.Value : false,
                IsSnowFallAmountRequested = SnowCheckBox.IsChecked.HasValue ? SnowCheckBox.IsChecked.Value : false,
                IsIceAccumulationRequested = IceCheckBox.IsChecked.HasValue ? IceCheckBox.IsChecked.Value : false
            });

            if (forecast == null) return;

            StringBuilder builder = new StringBuilder();

            foreach (ForecastTimeLine item in forecast.Timelines.Where(x => x.TemperatureMaximum != null | x.TemperatureMinimum != null | x.ProbabilityOfPrecipitation != null | x.HumidityReleative != null))
            {
                builder.AppendLine(item.DateTime.ToString());
                if (item.TemperatureMaximum != null) builder.AppendLine($"Max: { item.TemperatureMaximum.Value}°F");
                if (item.TemperatureMinimum != null) builder.AppendLine($"Min: { item.TemperatureMinimum.Value}°F");
                if (item.HumidityReleative != null) builder.AppendLine($"Humidity: { item.HumidityReleative.Value}%");
                if (item.ProbabilityOfPrecipitation != null) builder.AppendLine($"Chance Of Precipitation: { item.ProbabilityOfPrecipitation.Value.Value}%");
            }

            ResultTextBox.Text = builder.ToString();
        }
    }

}
