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
    public partial class ForecastPopupPage 
    {
        public ForecastPopupPage()
        {
            InitializeComponent();
            BindingContext = new ForecastPopupViewModel();
        }
    }
}