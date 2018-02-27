using System;
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
            Global.StationsUpdated += Global_StationsUpdated;
            Global_StationsUpdated(null, null);
        }

        private void Global_StationsUpdated(object sender, EventArgs e)
        {
            if (Global.Stations == null) return;

            cvs.Source = Global.Stations
                .OrderBy(x => x.CountryCode)
                .ThenBy(x => x.Name)
                .GroupBy(x => Bia.Countries.Iso3166.Countries.GetCountryByAlpha2(x.CountryCode));

        }


    }
}
