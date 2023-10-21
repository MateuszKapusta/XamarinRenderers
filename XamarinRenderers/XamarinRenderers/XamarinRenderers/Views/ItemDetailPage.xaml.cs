using System.ComponentModel;
using Xamarin.Forms;
using XamarinRenderers.ViewModels;

namespace XamarinRenderers.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}