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
        public static readonly BindableProperty NumericTypeProperty
            = BindableProperty.Create(nameof(NumericTypeProperty), typeof(NumericEntryType), typeof(NumericEntry), NumericEntryType.LongValue);

        public static readonly BindableProperty MaxValueProperty
            = BindableProperty.Create(nameof(MaxValue), typeof(int), typeof(NumericEntry), int.MaxValue);

        public static readonly BindableProperty MinValueProperty
            = BindableProperty.Create(nameof(MinValue), typeof(int), typeof(NumericEntry), int.MinValue);

        public static readonly BindableProperty DecimalPointProperty
            = BindableProperty.Create(nameof(DecimalPoint), typeof(int), typeof(NumericEntry), 5);

        public NumericEntryType NumericType
        {
            get { return (NumericEntryType)GetValue(NumericTypeProperty); }
            set { SetValue(NumericTypeProperty, value); }
        }
        public int MaxValue
        {
            get => (int)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public int MinValue
        {
            get => (int)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        public int DecimalPoint
        {
            get => (int)GetValue(DecimalPointProperty);
            set => SetValue(DecimalPointProperty, value);
        }

        public NumericEntry()
        {
            InitializeComponent();
        }
    }
}