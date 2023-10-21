using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinRenderers.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            NumberString.NumericType = Controls.NumericEntryType.Fractional;
            NumberInt.NumericType = Controls.NumericEntryType.Fractional;
        }
    }
}