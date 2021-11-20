using System.ComponentModel;
using WeatherAppXam.ViewModels;
using Xamarin.Forms;

namespace WeatherAppXam.Views
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