using System;
using System.Collections.Generic;
using WeatherAppXam.ViewModels;
using WeatherAppXam.Views;
using Xamarin.Forms;

namespace WeatherAppXam
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
