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
            EntryTypeButton.Text = NumberString.NumericType.ToString();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var number = NumberString.NumericType;
            number++;

            if(number > Controls.NumericEntryType.DecimalPositiveValue)
            {
                number = 0;
            }

            NumberString.NumericType = number;
            NumberLong.NumericType = number;
            NumberDecimal.NumericType = number;

            EntryTypeButton.Text = number.ToString();
        }
    }
}