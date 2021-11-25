using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using WeatherAppXam.Models;
using WeatherAppXam.Services;

namespace WeatherAppXam.ViewModels
{
    public class ForecastViewModel : BaseViewModel
    {
        private bool _isLoading, _isVisible;
        public ObservableCollection<SevenDaysDataModel> sevenDaysSource { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
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


        public ForecastViewModel()
        {
            ShowLoader();
            LoadForecast();
        }

        private async void LoadForecast()
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

                var forecast = await ApiService.GetForecastOpenWeather(endpoint);
                if (forecast.daily != null && forecast.current != null)
                {
                    SevenDaysSource = new ObservableCollection<SevenDaysDataModel>();
                    IsLoading = false;
                    IndicatorVisibility = false;

                    //FORECAST
                    foreach (var v in forecast.daily)
                    {
                        var date = BaseService.UnixTimeStampToDateTime(Convert.ToDouble(v.dt));
                        //var pub = $"{v.PublishedAt.ToString("MMM dd, yyyy")}, {v.PublishedAt.ToShortTimeString()}";
                        SevenDaysSource.Add(new SevenDaysDataModel
                        {
                            Date = $"{date.ToString("dd MMM, yyyy")}",
                            Day = $"{date.ToString("dddd")}",
                            Humidity = $"{v.humidity}%",
                            Image = "https://openweathermap.org/img/wn/" + $"{v.weather[0].icon}@2x.png",
                            MaxWind = $"{v.wind_speed} m/s",
                            TempHighLow = $"{Convert.ToInt32(v.temp.day)}°C"
                            //TempHighLow = $"{v.day.maxtemp_c}°C / {v.day.mintemp_c}°C"
                        });
                    }
                }

                IsLoading = false;
                IndicatorVisibility = false;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async void ShowLoader()
        {
            IsLoading = true;
            IndicatorVisibility = true;
            await Task.Delay(1000);

        }
    }
}
