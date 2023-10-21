using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XamarinRenderers.ViewModels;
using XamarinRenderers.Views;

namespace XamarinRenderers
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}
