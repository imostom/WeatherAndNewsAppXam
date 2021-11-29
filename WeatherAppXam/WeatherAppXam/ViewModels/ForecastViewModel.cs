using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherAppXam.Models;
using WeatherAppXam.Services;
using WeatherAppXam.Views;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Location = Xamarin.Essentials.Location;

namespace WeatherAppXam.ViewModels
{
    public class ForecastViewModel : BaseViewModel
    {
        private bool _isLoading, _isVisible, _searchStackVisible;
        public string TempMetric
        {
            get
            {
                return tempMetric;
            }
            set
            {
                tempMetric = value;
                OnPropertyChanged();
            }
        }
        public string CurrentLocation
        {
            get
            {
                return currentLocation;
            }
            set
            {
                currentLocation = value;
                OnPropertyChanged();
            }
        }
        public string DateLocation
        {
            get
            {
                return dateLocation;
            }
            set
            {
                dateLocation = value;
                OnPropertyChanged();
            }
        }
        public string CurrentTempIcon
        {
            get
            {
                return currentTempIcon;
            }
            set
            {
                currentTempIcon = value;
                OnPropertyChanged();
            }
        }
        public string CurrentTempF
        {
            get
            {
                return currentTempF;
            }
            set
            {
                currentTempF = value;
                OnPropertyChanged();
            }
        }
        public string CurrentTempC
        {
            get
            {
                return currentTempC;
            }
            set
            {
                currentTempC = value;
                OnPropertyChanged();
            }
        }
        public string CurrentHumidity
        {
            get
            {
                return currentHumidity;
            }
            set
            {
                currentHumidity = value;
                OnPropertyChanged();
            }
        }
        public string CurrentDescription
        {
            get
            {
                return currentDescription;
            }
            set
            {
                currentDescription = value;
                OnPropertyChanged();
            }
        }
        public string CurrentWindSpeed
        {
            get
            {
                return currentWindSpeed;
            }
            set
            {
                currentWindSpeed = value;
                OnPropertyChanged();
            }
        }
        public string CurrentWindDirection
        {
            get
            {
                return currentWindDirection;
            }
            set
            {
                currentWindDirection = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<OpenWeatherHourly> HourlySource
        {
            get
            {
                return hourlySource;
            }
            set
            {
                hourlySource = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<SevenDaysDataModel> SevenDaysSource
        {
            get
            {
                return sevenDaysSource;
            }
            set
            {
                sevenDaysSource = value;
                OnPropertyChanged();
            }
        }
        public bool IndicatorVisibility
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged("IndicatorVisibility");
            }
        }
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
        public bool SearchStackVisible
        {
            get => _searchStackVisible;
            set
            {
                _searchStackVisible = value;
                OnPropertyChanged("SearchStackVisible");
            }
        }

        public string tempMetric { get; set; }
        public string dateLocation { get; set; }
        public string currentLocation;
        public string currentTempIcon { get; set; }
        public string currentTempF { get; set; }
        public string currentHumidity { get; set; }
        public string currentTempC { get; set; }
        public string currentDescription { get; set; }
        public string currentWindSpeed { get; set; }
        public string currentWindDirection { get; set; }
        public string bgImage { get; set; }
        public ObservableCollection<OpenWeatherHourly> hourlySource { get; set; }
        public ObservableCollection<SevenDaysDataModel> sevenDaysSource { get; set; }


        public ForecastViewModel()
        {

            //ShowLoader();

            //LoadOpenWeatherForecast(SearchCity);
        }

        private async void ShowLoader()
        {
            IsLoading = true;
            IndicatorVisibility = true;
            await Task.Delay(1000);

        }

        public async void LoadOpenWeatherForecast(string address, double longitude, double latitude)
        {
            try
            {

                var placemarkResult = await BaseService.ReverseGeocode(longitude, latitude);

                var resource = Constants.OpenWeatherApiOneCallEndpoint.Replace("{lat}", latitude.ToString())
                    .Replace("{lon}", longitude.ToString()).Replace("{APIkey}", Constants.OpenWeatherApiKey);
                var endpoint = $"{Constants.OpenWeatherApiBaseUrl}{resource}";

                var forecastResponse = await ApiService.GetForecastOpenWeather2(endpoint);
                //var forecastResponse = await ApiService.GetForecastOpenWeather(endpoint);
                if (forecastResponse != null)
                {
                    IsLoading = false;
                    IndicatorVisibility = false;
                    TempMetric = "°C";

                    ///TODO    Cache the forecast response using monkey cache
                    CurrentReport = forecastResponse.current;
                    DailyReport = forecastResponse.daily;
                    HourlyReport = forecastResponse.hourly;

                    await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new ForecastPopupPage());
                }
                else
                {
                    IsLoading = false;
                    IndicatorVisibility = false;

                    toast = DoToast("We encountered an error while processing your request. Please try again", "error");
                    await Application.Current.MainPage.DisplayToastAsync(toast);
                }
            }
            catch (Exception ex)
            {
                IsLoading = false;
                IndicatorVisibility = false;

                toast = DoToast("We encountered an error processing your request. Please try again.", "error");
                await Application.Current.MainPage.DisplayToastAsync(toast);
            }
        }

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            try
            {
                

                if (string.IsNullOrEmpty(query))
                {
                    toast = DoToast("Please enter a location.", "error");
                    Application.Current.MainPage.DisplayToastAsync(toast);
                }
                else
                {
                    ShowLoader();

                    GotoPage(query);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        });

        private async void GotoPage(string address)
        {
            try
            {
                var location = new Location();
                var locations = await Geocoding .GetLocationsAsync(address);
                if (locations == null)
                {
                    location = new Xamarin.Essentials.Location
                    {
                        Latitude = 37.386051,
                        Longitude = -122.083855
                    };
                }
                else
                {
                    var searchLocations = new List<Location>();
                    foreach (var l in locations)
                    {
                        searchLocations.Add(l);
                    }

                    if (searchLocations.Count == 0)
                    {
                        IsLoading = false;
                        IndicatorVisibility = false;

                        toast = DoToast("Entered location not found. Please try again.", "error");
                        await Application.Current.MainPage.DisplayToastAsync(toast);
                    }
                    else
                    {
                        location.Latitude = searchLocations[0].Latitude;
                        location.Longitude = searchLocations[0].Longitude;

                        PlacemarkResult = await BaseService.ReverseGeocode(location.Longitude, location.Latitude);

                        if (PlacemarkResult != null && !string.IsNullOrEmpty(PlacemarkResult.CountryName))
                        {
                            SearchCity = address;

                            LoadOpenWeatherForecast(address, location.Longitude, location.Latitude);

                            //await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new ForecastPopupPage());
                        }
                        else
                        {
                            toast = DoToast("Entered location not found. Please try again.", "error");
                            await Application.Current.MainPage.DisplayToastAsync(toast);
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                IsLoading = false;
                IndicatorVisibility = false;

                toast = DoToast("We encountered an error processing your request. Please try again.", "error");
                await Application.Current.MainPage.DisplayToastAsync(toast);
            }
        }

        public async void TapGestureRecognizer_Tapped(EventArgs tappedEventArgs)
        {
            var tappedItem = ((TappedEventArgs)tappedEventArgs).Parameter;

            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new ForecastPopupPage());
        }
    }
}
