using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinRenderers.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private ICommand _testCommand;
        public ICommand TestCommand => _testCommand ?? (_testCommand = new Command(Test));

        private string _numericString;
        public string NumericString
        {
            get
            {
                return _numericString;
            }
            set
            {
                _numericString = value;
                OnPropertyChanged();
            }
        }

        private long _numericLong;
        public long NumericLong
        {
            get
            {
                return _numericLong;
            }
            set
            {
                _numericLong = value;
                OnPropertyChanged();
            }
        }

        private decimal _numericDecimal;
        public decimal NumericDecimal
        {
            get
            {
                return _numericDecimal;
            }
            set
            {
                _numericDecimal = value;
                OnPropertyChanged();
            }
        }


        public AboutViewModel()
        {
            Title = "About";
        }

        private void Test()
        {

        }
    }
}