using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace OpenWeather.Example.Uwp
{
    public sealed partial class Alerts : UserControl
    {
        public Alerts()
        {
            this.InitializeComponent();
        }

        private async void GetAlertsButton_Click(object sender, RoutedEventArgs e)
        {

            if (String.IsNullOrWhiteSpace(ZipCodeTextBox.Text) |
                (String.IsNullOrWhiteSpace(CountyTextBox.Text) | String.IsNullOrWhiteSpace(StateTextBox.Text)))
                StatusTextBlock.Text = "Be sure the county and state or zip code are filled before you get alerts.";

            AlertResultListBox.Items.Clear();

            if (!String.IsNullOrWhiteSpace(ZipCodeTextBox.Text))
                StatusTextBlock.Text = $"Grabbing alerts for {ZipCodeTextBox.Text}.";
            else
                StatusTextBlock.Text = $"Grabbing alerts for {CountyTextBox.Text}, {StateTextBox.Text}.";

            Noaa.Api api = new Noaa.Api();
            IEnumerable<Noaa.Models.Alerts.WeatherAlert> alerts = null;

            if (!String.IsNullOrWhiteSpace(ZipCodeTextBox.Text))
                alerts = await api.GetWeatherAlertByZipCodeAsync(ZipCodeTextBox.Text);
            else
                alerts = await api.GetWeatherAlertByCountyAndStateAsync(CountyTextBox.Text, StateTextBox.Text);

            //IEnumerable<Noaa.Models.Alerts.WeatherAlert> alerts = await api.GetWeatherAlertByXmlFile("wwaatmget.xml");
            //IEnumerable<Noaa.Models.Alerts.WeatherAlert> alerts = await api.GetWeatherAlertByCountyCode(CountyCodeTextBox.Text);

            if (alerts == null || alerts.Count() == 0)
                if (!String.IsNullOrWhiteSpace(ZipCodeTextBox.Text))
                    StatusTextBlock.Text = $"There are no alerts for {ZipCodeTextBox.Text}.";
                else
                    StatusTextBlock.Text = $"There are no alerts for {CountyTextBox.Text}, {StateTextBox.Text}.";
            else
                alerts.ToList().ForEach(i => AlertResultListBox.Items.Add(i));

        }
    }
}
