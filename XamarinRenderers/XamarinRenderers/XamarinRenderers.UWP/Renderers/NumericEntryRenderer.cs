using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
        protected int Min { get; set; }
        protected int Max { get; set; }
        protected int DecimalPoint { get; set; }
        private int ControlOldPosition;


        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null
                && e.NewElement is NumericEntry numericEntry
                && Control != null)
            {
                Control.TextChanging -= Control_TextChanging;
                Control.SelectionChanging -= Control_SelectionChanging;

                Type = numericEntry.NumericType;
                Min = numericEntry.MinValue;
                Max = numericEntry.MaxValue;
                DecimalPoint = numericEntry.DecimalPoint;
                ResetDefaultValue();
                Control.TextChanging += Control_TextChanging;
                Control.SelectionChanging += Control_SelectionChanging;
            }
        }




        private void Control_SelectionChanging(TextBox sender, TextBoxSelectionChangingEventArgs args)
        {
            ControlOldPosition = sender.SelectionStart + sender.SelectionLength;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (sender is NumericEntry numericEntry)
            {
                switch (e.PropertyName)
                {
                    case nameof(NumericEntry.NumericType):
                        Type = numericEntry.NumericType;
                        ResetDefaultValue();
                        break;
                    case nameof(NumericEntry.MaxValue):
                        Max = numericEntry.MaxValue;
                        break;
                    case nameof(NumericEntry.MinValue):
                        Min = numericEntry.MinValue;
                        break;
                    case nameof(NumericEntry.DecimalPoint):
                        DecimalPoint = numericEntry.DecimalPoint;
                        break;
                }
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
                case NumericEntryType.DecimalValue:
                case NumericEntryType.DecimalPositiveValue:
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
                case NumericEntryType.DecimalValue:
                    DecimalTextChanging(sender, args);
                    break;
                case NumericEntryType.DecimalPositiveValue:
                    DecimalPositiveTextChanging(sender, args);
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

            if (long.TryParse(sender.Text, out long value)
                && ValueFromMinMaxRange(value))
            {
                var senderPositionTmp = sender.SelectionStart + sender.SelectionLength;
                var cleanValue = value.ToString();
                sender.Text = cleanValue;
                sender.SelectionStart = cleanValue.Length < senderPositionTmp ? cleanValue.Length : senderPositionTmp;
                return;
            }

            if (string.IsNullOrEmpty(sender.Text))
            {
                sender.Text = "0";
                sender.SelectionStart = 1;
                return;
            }

            ResetToOldValues(sender);
        }


        private void LongPositiveTextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {

            if (long.TryParse(sender.Text, out long value)
                && value >= 0
                && ValueFromMinMaxRange(value))
            {
                var senderPositionTmp = sender.SelectionStart + sender.SelectionLength;
                var cleanValue = value.ToString();
                sender.Text = cleanValue;
                sender.SelectionStart = cleanValue.Length < senderPositionTmp ? cleanValue.Length : senderPositionTmp;
                return;
            }

            if (string.IsNullOrEmpty(sender.Text))
            {
                sender.Text = "0";
                sender.SelectionStart = 1;
                return;
            }

            ResetToOldValues(sender);
        }


        private void DecimalTextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (sender.Text == "-0"
                || sender.Text == "0-"
                || sender.Text == "-")
            {
                sender.Text = "-";
                sender.Select(sender.Text.Length, 0);
                return;
            }

            if (decimal.TryParse(sender.Text, out decimal value)
                && ValueFromMinMaxRange(value))
            {
                var senderPositionTmp = sender.SelectionStart + sender.SelectionLength;
                var cleanValue = value.ToString();

                if (sender.Text.Last() == '.')
                {
                    cleanValue += ".";
                }

                sender.Text = cleanValue;
                sender.SelectionStart = cleanValue.Length < senderPositionTmp ? cleanValue.Length : senderPositionTmp;
                return;
            }

            if (string.IsNullOrEmpty(sender.Text))
            {
                sender.Text = "0";
                sender.SelectionStart = 1;
                return;
            }

            ResetToOldValues(sender);
        }

        private void DecimalPositiveTextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (decimal.TryParse(sender.Text, out decimal value)
                && value >= 0
                && ValueFromMinMaxRange(value))
            {
                var senderPositionTmp = sender.SelectionStart + sender.SelectionLength;
                var cleanValue = value.ToString();

                if (sender.Text.Last() == '.')
                {
                    cleanValue += ".";
                }

                sender.Text = cleanValue;
                sender.SelectionStart = cleanValue.Length < senderPositionTmp ? cleanValue.Length : senderPositionTmp;
                return;
            }

            if (string.IsNullOrEmpty(sender.Text))
            {
                sender.Text = "0";
                sender.SelectionStart = 1;
                return;
            }

            ResetToOldValues(sender);
        }

        private void ResetToOldValues(TextBox sender)
        {
            sender.Text = Control.Text;
            sender.SelectionStart = ControlOldPosition - 1;
        }

        private bool ValueFromMinMaxRange(decimal value)
        {
            if (value < Min || value > Max)
            {
                return false;
            }

            if (value.ToString(CultureInfo.InvariantCulture)
                     .SkipWhile(x => x != '.')
                     .Skip(1)
                     .Count() > DecimalPoint)
            {
                return false;
            }

            return true;
        }
    }
}
