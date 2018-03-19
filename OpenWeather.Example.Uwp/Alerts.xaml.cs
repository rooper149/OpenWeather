﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace OpenWeather.Example.Uwp
{
    public sealed partial class Alerts : UserControl
    {
        private Noaa.Alerts.Api _api = null;

        public Alerts()
        {
            this.InitializeComponent();
        }

        private async void GetAlertsButton_Click(object sender, RoutedEventArgs e)
        {
            if (_api != null)
            {
                _api.Dispose();
                _api = null;
            }

            _api = new Noaa.Alerts.Api();

            switch (ConditionsPiviot.SelectedIndex)
            {
                case 0:
                    await QueryByCountyStateAsync();
                    break;
                case 1:
                    await QueryByZipCodeAsync();
                    break;
                case 2:
                    await QueryByZipCodeIntervalAsync();
                    break;
                default:
                    break;
            }
        }

        private void FillAlerts(IEnumerable<Noaa.Alerts.WeatherAlert> alerts, string area)
        {
            if (alerts == null || alerts.Count() == 0)
                StatusTextBlock.Text = $"There are no alerts for {area}.{Environment.NewLine}Last Query: {DateTime.Now}";
            else
            {
                StatusTextBlock.Text = $"Last Query: {DateTime.Now}";
                alerts.ToList().ForEach(i => AlertResultListBox.Items.Add(i));
            }
        }

        private async Task QueryByCountyStateAsync()
        {
            if (String.IsNullOrWhiteSpace(CountyTextBox.Text) | String.IsNullOrWhiteSpace(StateTextBox.Text))
            {
                StatusTextBlock.Text = "Be sure the county and state are filled before you get alerts.";
                return;
            }

            FillAlerts(await _api.GetWeatherAlertByCountyAndStateAsync(CountyTextBox.Text, StateTextBox.Text), $"{CountyTextBox.Text}, {StateTextBox.Text}");
        }

        private async Task QueryByZipCodeAsync()
        {
            if (String.IsNullOrWhiteSpace(ZipCode_ZipCodeTextBox.Text))
            {
                StatusTextBlock.Text = "Be sure the zip code is filled before you get alerts.";
                return;
            }

            FillAlerts(await _api.GetWeatherAlertByZipCodeAsync(ZipCode_ZipCodeTextBox.Text), $"{ZipCode_ZipCodeTextBox.Text}");
        }

        private async Task QueryByZipCodeIntervalAsync()
        {
            int interval = 0;

            if (String.IsNullOrWhiteSpace(ZipCodeInterval_ZipCodeTextBox.Text) || String.IsNullOrWhiteSpace(ZipCodeInterval_SecondsTextBox.Text))
            {
                StatusTextBlock.Text = "Be sure the zip code and the interval is filled before you get alerts.";
                return;
            }
            if (!Int32.TryParse(ZipCodeInterval_SecondsTextBox.Text, out interval))
            {
                StatusTextBlock.Text = "Be sure the interval is a whole number before you get alerts.";
                return;
            }

            FillAlerts(await _api.GetWeatherAlertByZipCodeAndInvervalAsync(ZipCodeInterval_ZipCodeTextBox.Text, interval), $"{ZipCode_ZipCodeTextBox.Text}");
        }

        private async Task QueryByZipCodeAutomaticallyAsync()
        {
            int interval = 0;

            if (String.IsNullOrWhiteSpace(ZipCodeAutomatically_ZipCodeTextBox.Text))
            {
                StatusTextBlock.Text = "Be sure the zip code is filled before you get alerts.";
                return;
            }

            FillAlerts(await _api.GetWeatherAlertByZipCodeAndInvervalAsync(ZipCodeInterval_ZipCodeTextBox.Text, interval), $"{ZipCode_ZipCodeTextBox.Text}");
        }

    }
}
