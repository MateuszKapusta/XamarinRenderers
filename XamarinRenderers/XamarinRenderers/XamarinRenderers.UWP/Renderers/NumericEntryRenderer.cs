using System;
using System.Collections.Generic;
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

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (Control != null)
                {
                    Control.Text = "0";
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
