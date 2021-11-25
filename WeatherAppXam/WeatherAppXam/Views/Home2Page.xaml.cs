using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppXam.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherAppXam.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home2Page : ContentPage
    {
        public Home2Page()
        {
            InitializeComponent();
            BindingContext = new Home2ViewModel();
        }
    }
}