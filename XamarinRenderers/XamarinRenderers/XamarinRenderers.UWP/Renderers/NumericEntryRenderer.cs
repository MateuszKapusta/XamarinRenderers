using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                    Control.BeforeTextChanging += Control_BeforeTextChanging;
                    Control.TextChanging += Control_TextChanging;
                }
            }

            if (e.OldElement != null)
            {
                Control.BeforeTextChanging -= Control_BeforeTextChanging;
                Control.TextChanging -= Control_TextChanging;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (string.Equals(e.PropertyName, nameof(NumericEntry.NumericTypeProperty))
                && sender is NumericEntry numericEntry)
            {
                Type = numericEntry.NumericType;
            }
        }

        private void ResetDefaultValue()
        {
            switch (Type)
            {
                case NumericEntryType.Natural:
                case NumericEntryType.Integers:
                    Control.Text = "0";
                    break;
                case NumericEntryType.Fractional:
                    Control.Text = "0.0";
                    break;
            }
        }


        private void Control_TextChanging(Windows.UI.Xaml.Controls.TextBox sender, Windows.UI.Xaml.Controls.TextBoxTextChangingEventArgs args)
        {
            if (string.IsNullOrEmpty(sender.Text))
            {
                Control.Text = "0";
            }

            if (sender.Text.Length == 2 && sender.Text.First() == '0')
            {
                Control.Text = sender.Text.Substring(1);
            }
        }

        private void Control_BeforeTextChanging(Windows.UI.Xaml.Controls.TextBox sender, Windows.UI.Xaml.Controls.TextBoxBeforeTextChangingEventArgs args)
        {
            if (int.TryParse(args.NewText, out int value)
                && (args.NewText.First() != '0' || Control.Text == "0"))
            {
                return;
            }

            if (string.IsNullOrEmpty(args.NewText) && !string.Equals(sender.Text, "0"))
            {
                return;
            }

            args.Cancel = true;
        }
    }
}
