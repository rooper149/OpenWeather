using System.Linq;
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
            NoaaApi api = new NoaaApi(null);
            var stations = await api.GetStationsAsync();

            this.cvs.Source = stations.OrderBy(x => x.Name).GroupBy(x => x.Name.Substring(0, 1));
        }
    }
}
