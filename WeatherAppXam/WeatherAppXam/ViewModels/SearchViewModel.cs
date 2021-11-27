using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WeatherAppXam.Views;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace WeatherAppXam.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public bool RadioWeatherChecked { get; set; }
        public bool RadioNewsChecked { get; set; }
        //public Command CheckChangedCommand { get; }

        public SearchViewModel()
        {
            try
            {

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            try
            {

                var radWeather = RadioWeatherChecked;
                var radNews = RadioNewsChecked;
                if (!radWeather && !radNews)
                {
                    toast = DoToast("Select a search option.", "error");
                    Application.Current.MainPage.DisplayToastAsync(toast);
                }
                else
                {
                    if (radWeather)
                    {
                        SearchCity = query;
                        GotoPage();
                    }
                    //else
                    //    new ForecastViewModel(query);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        });

        private async void GotoPage()
        {
            //var tabbedPage = this.Parent.Parent as TabbedPage;
            //tabbedPage.CurrentPage = tabbedPage.Children[0];

            //await Shell.Current.GoToAsync($"forecast");
            await new NavigationPage().PushAsync(new ForecastPage());
        }
    }
}
