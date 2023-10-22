using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinRenderers.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NumericEntry : Entry
    {
        public static readonly BindableProperty NumericTypeProperty = BindableProperty.Create(nameof(NumericTypeProperty), typeof(NumericEntryType), typeof(NumericEntry), NumericEntryType.Integer);

        public NumericEntryType NumericType
        {
            get { return (NumericEntryType)GetValue(NumericTypeProperty);}
            set { SetValue(NumericTypeProperty, value);}
        }

        public NumericEntry()
        {
            InitializeComponent();
        }
    }
}