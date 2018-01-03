using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace OpenWeather.Example.Uwp
{
    public sealed partial class CurrentObservations : UserControl
    {
        public CurrentObservations()
        {
            this.InitializeComponent();
        }

        private async void GetCurrentObservationsButton_Click(object sender, RoutedEventArgs e)
        {
            WeatherIconImage.Source = null;
            LastUpdatedTextBlock.Text = String.Empty;
            WeatherTextBlock.Text = String.Empty;
            TemperatureTextBlock.Text = String.Empty;
            DewpointTextBlock.Text = String.Empty;
            RelativeHumidityTextBlock.Text = String.Empty;
            WindTextBlock.Text = String.Empty;
            WindChillTextBlock.Text = String.Empty;
            VisibilityTextBlock.Text = String.Empty;
            MSLPressureTextBlock.Text = String.Empty;
            AltimeterTextBlock.Text = String.Empty;
            GetCurrentObservationsButton.IsEnabled = false;


            StatusTextBlock.Text = "Getting Current Observations....";

            NoaaApi api = new NoaaApi(null);
            Windows.Storage.StorageFolder storageFolder =    Windows.Storage.ApplicationData.Current.LocalFolder;

            IEnumerable<Models.Station> stations = await api.GetStationsAsync();
            Models.Station station = stations.SingleOrDefault(x => x.ICAO == "KGSH");
            Models.CurrentObservation currentObservations = await api.GetCurrentObservationsByStationAsync(station);
            if (currentObservations != null)
            {
                StationInformationTextBlock.Text = $"{currentObservations.Location}{Environment.NewLine}({station.ICAO}) {station.Latitude} {station.Longitude}";
                WeatherIconImage.Source = new BitmapImage(new Uri(currentObservations.WeatherIconUrl));
                LastUpdatedTextBlock.Text = currentObservations.LastUpdated.ToString();
                WeatherTextBlock.Text = currentObservations.Weather;
                TemperatureTextBlock.Text = $"{currentObservations.Temperature_F} °F ({currentObservations.Temperature_C} °C)";
                DewpointTextBlock.Text = $"{currentObservations.DewPoint_F} °F ({currentObservations.DewPoint_C} °C)";
                RelativeHumidityTextBlock.Text = $"{currentObservations.Humidity} %";
                WindTextBlock.Text = $"{currentObservations.WindDirection} at {currentObservations.Wind_MPH} MPH";
                WindChillTextBlock.Text = $"{currentObservations.WindChill_F} °F ({currentObservations.WindChill_C} °C)";
                VisibilityTextBlock.Text = $"{currentObservations.Visibility_Mi} miles";
                MSLPressureTextBlock.Text = $"{currentObservations.Pressure_MB} mb";
                AltimeterTextBlock.Text = $"{currentObservations.Pressure_In} in";
            }

            StatusTextBlock.Text = $"Received weather from {station.Name}";
        }
    }
}
