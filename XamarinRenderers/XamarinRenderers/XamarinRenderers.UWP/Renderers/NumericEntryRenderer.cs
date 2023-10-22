using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using XamarinRenderers.Controls;
using XamarinRenderers.UWP.Renderers;

[assembly: ExportRenderer(typeof(NumericEntry), typeof(NumericEntryRenderer))]
namespace XamarinRenderers.UWP.Renderers
{
    public class NumericEntryRenderer : EntryRenderer
    {
        protected NumericEntryType Type { get; set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null
                && e.NewElement is NumericEntry numericEntry)
            {
                if (Control != null)
                {
                    Type = numericEntry.NumericType;
                    ResetDefaultValue();
                    Control.TextChanging += Control_TextChanging;
                }
            }

            if (e.OldElement != null)
            {
                if (Control != null)
                {
                    Control.TextChanging -= Control_TextChanging;
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (string.Equals(e.PropertyName, nameof(NumericEntry.NumericTypeProperty))
                && sender is NumericEntry numericEntry)
            {
                Type = numericEntry.NumericType;
                ResetDefaultValue();
            }
        }

        private void ResetDefaultValue()
        {
            switch (Type)
            {
                case NumericEntryType.LongValue:
                case NumericEntryType.LongPositiveValue:
                    Control.Text = "0";
                    break;
                case NumericEntryType.DoubleValue:
                case NumericEntryType.DoublePositiveValue:
                    Control.Text = "0.0";
                    break;
            }
        }

        private void Control_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            switch (Type)
            {
                case NumericEntryType.LongValue:
                    LongTextChanging(sender, args);
                    break;
                case NumericEntryType.LongPositiveValue:
                    LongPositiveTextChanging(sender, args);
                    break;
                case NumericEntryType.DoubleValue:
                    DoubleTextChanging(sender, args);
                    break;
                case NumericEntryType.DoublePositiveValue:
                    DoublePositiveTextChanging(sender, args);
                    break;
            }
        }

        private void LongTextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (sender.Text == "-0"
                || sender.Text == "0-"
                || sender.Text == "-")
            {
                sender.Text = "-";
                sender.Select(sender.Text.Length, 0);
                return;
            }

            if (long.TryParse(sender.Text, out long value))
            {
                if (sender.Text.First() == '0'
                    || sender.Text.StartsWith("-0")
                    || sender.Text.Contains(" "))
                {
                    sender.Text = value.ToString();
                    sender.Select(sender.Text.Length, 0);
                    return;
                }

                return;
            }

            if (string.IsNullOrEmpty(sender.Text))
            {
                sender.Text = "0";
                sender.Select(sender.Text.Length, 0);
                return;
            }

            sender.Text = Control.Text;
            sender.Select(Control.Text.Length, 0);
        }


        private void LongPositiveTextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (long.TryParse(sender.Text, out long value)
                && value >= 0)
            {
                if (sender.Text.First() == '0'
                    || sender.Text == "-0"
                    || sender.Text.Contains(" "))
                {
                    sender.Text = value.ToString();
                    sender.Select(sender.Text.Length, 0);
                    return;
                }

                return;
            }

            if (string.IsNullOrEmpty(sender.Text))
            {
                sender.Text = "0";
                sender.Select(sender.Text.Length, 0);
                return;
            }

            sender.Text = Control.Text;
            sender.Select(Control.Text.Length, 0);
        }


        private void DoubleTextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {

        }

        private void DoublePositiveTextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (Double.TryParse(sender.Text, out double value)
                && value >= 0)
            {
                if ((sender.Text.First() == '0' && sender.Text.Length >= 2 && sender.Text[1] != '.')
                    || sender.Text == "-0"
                    || sender.Text.Contains(" "))
                {
                    sender.Text = value.ToString();
                    sender.Select(sender.Text.Length, 0);
                    return;
                }

                return;
            }


            if (string.IsNullOrEmpty(sender.Text))
            {
                sender.Text = "0";
                sender.Select(sender.Text.Length, 0);
                return;
            }

            sender.Text = Control.Text;
            sender.Select(Control.Text.Length, 0);
        }


    }
}
