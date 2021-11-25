using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppXam.Models;
using WeatherAppXam.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherAppXam.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPopupPage 
    {
        public NewsPopupPage(string tappedItem, ObservableCollection<NewsDisplayModel> newsObjects)
        {
            InitializeComponent();
            BindingContext = new NewsPopupViewModel(tappedItem, newsObjects);
        }
    }
}