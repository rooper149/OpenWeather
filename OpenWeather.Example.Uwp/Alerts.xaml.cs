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
            AlertResultListBox.Items.Clear();

            Noaa.Api api = new Noaa.Api();
            IEnumerable<Noaa.Models.Alerts.WeatherAlert> alerts = await api.GetWeatherAlertByXmlFile("wwaatmget.xml");
            //IEnumerable<Noaa.Models.Alerts.WeatherAlert> alerts = await api.GetWeatherAlertByCountyCode(CountyCodeTextBox.Text);

            if (alerts == null || alerts.Count() == 0) return;

            alerts.ToList().ForEach(i => AlertResultListBox.Items.Add(i));

        }
    }
}
