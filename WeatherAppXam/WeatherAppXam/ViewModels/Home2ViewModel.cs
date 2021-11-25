using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WeatherAppXam.Models;
using WeatherAppXam.Services;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace WeatherAppXam.ViewModels
{
    public class Home2ViewModel : BaseViewModel
    {
        private bool _isLoading, _isVisible;
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


        public Home2ViewModel()
        {
            ShowLoader();

            LoadOpenWeatherForecast();
        }

        private async void ShowLoader()
        {
            IsLoading = true;
            IndicatorVisibility = true;
            await Task.Delay(1000);

        }

        public async void LoadOpenWeatherForecast()
        {
            try
            {
                var location = await BaseService.GetLocation("start");
                location = new Xamarin.Essentials.Location
                {
                    Latitude = 37.386051,
                    Longitude = -122.083855
                };
                var placemarkResult = await BaseService.ReverseGeocode(location.Longitude, location.Latitude);

                var resource = Constants.OpenWeatherApiOneCallEndpoint.Replace("{lat}", location.Latitude.ToString())
                    .Replace("{lon}", location.Longitude.ToString()).Replace("{APIkey}", Constants.OpenWeatherApiKey);
                var endpoint = $"{Constants.OpenWeatherApiBaseUrl}{resource}";
                HourlySource = new ObservableCollection<OpenWeatherHourly>();

                var forecastResponse = await ApiService.GetForecastOpenWeather(endpoint);
                if (forecastResponse != null)
                {
                    IsLoading = false;
                    IndicatorVisibility = false;
                    //var i = 0;

                    foreach (var v in forecastResponse.hourly)
                    {
                        var date = BaseService.UnixTimeStampToDateTime(Convert.ToDouble(v.dt));

                        HourlySource.Add(new OpenWeatherHourly
                        {
                            Image = "https://openweathermap.org/img/wn/" + $"{v.weather[0].icon}@2x.png",
                            Temp = $"{Convert.ToInt32(v.temp)}°C",
                            Time = $"{date.ToString("HH:mm")}"
                        });
                        //i++;
                    } 
                }

                CurrentTempC = Convert.ToInt32(forecastResponse.current.temp).ToString();
                CurrentHumidity = $"{forecastResponse.current.humidity.ToString()}%";
                CurrentWindSpeed = forecastResponse.current.wind_speed.ToString();
                CurrentWindDirection = forecastResponse.current.wind_deg.ToString();
                CurrentTempIcon = "https://openweathermap.org/img/wn/" + $"{forecastResponse.current.weather[0].icon}@2x.png";
                CurrentDescription = forecastResponse.current.weather[0].description;
                CurrentLocation = $"{placemarkResult.Locality}, {placemarkResult.CountryName}";
            }
            catch (Exception ex)
            {

                toast = DoToast("We encountered an error processing your request. Please try again.", "error");
                await Application.Current.MainPage.DisplayToastAsync(toast);
            }
        }
    }
}
