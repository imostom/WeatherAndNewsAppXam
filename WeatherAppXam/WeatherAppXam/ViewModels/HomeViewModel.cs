using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WeatherAppXam.Models;
using WeatherAppXam.Services;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherAppXam.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
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
        public ObservableCollection<SevenDaysDataModel> sevenDaysSource { get; set; }
        //public ObservableCollection<SevenDaysDataModel> SevenDaysSource { get; set; }


        public HomeViewModel()
        {
            //LoadOpenWeatherForecast();

            LoadForecast();
            //LoadLocationDetails(currentCity);

        }

        

        private async void LoadForecast()
        {
            try
            {
                
                //var salt = await CryptographyService.GenerateSalt();
                
                var location = await BaseService.GetLocation("start");
                var placemarkResult = await BaseService.ReverseGeocode(location.Longitude, location.Latitude);
                //var encLocality = CryptographyUtil.EncryptText(placemarkResult.Locality, Constants.EncryptionPassword, Constants.Salt);
                //var city = "Lagos";

                var resource = Constants.WeatherApiForecast7days.Replace("{PARAM}", placemarkResult.Locality).Replace("{DAYS}", "7")
                   .Replace("{KEY}", Constants.WeatherApiKey).Replace("{AQI}", Constants.WeatherApiAQI).Replace("{ALERTS}", Constants.WeatherApiAlerts);
                var endpoint = $"{Constants.WeatherApiBaseUrl}{resource}";

                
                var forecastResponse = await ApiService.GetSevenDaysForecast2(endpoint);

                if (forecastResponse.current == null || forecastResponse.forecast == null || forecastResponse.location == null)
                {
                    toast = DoToast("Entered location is Invalid. Please try again.", "error");
                    await Application.Current.MainPage.DisplayToastAsync(toast);
                }
                else
                {
                    SevenDaysSource = new ObservableCollection<SevenDaysDataModel>();

                    //FORECAST
                    foreach (var v in forecastResponse.forecast.forecastday)
                    {
                        SevenDaysSource.Add(new SevenDaysDataModel
                        {
                            Date = $"{v.date}",
                            Day = Convert.ToDateTime(v.date).ToString("dddd"),
                            Humidity = $"{v.day.avghumidity.ToString()}%",
                            Image = $"https:{v.day.condition.icon}",
                            MaxWind = $"{v.day.maxwind_kph} km/h",
                            TempHighLow = $"{v.day.maxtemp_c}°C"
                            //TempHighLow = $"{v.day.maxtemp_c}°C / {v.day.mintemp_c}°C"
                        });
                    }


                    //CURRENT
                    DateLocation = $"{Convert.ToDateTime(forecastResponse.location.localtime).ToLongDateString()} " +
                        $"{Convert.ToDateTime(forecastResponse.location.localtime).ToShortTimeString()}({forecastResponse.location.name})";
                    CurrentLocation = $"{forecastResponse.location.name}, {forecastResponse.location.country}";
                    CurrentTempC = $"{forecastResponse.current.temp_c}°";
                    CurrentTempF = $"{forecastResponse.current.temp_f}°F";
                    CurrentHumidity = $"{forecastResponse.current.humidity}%";
                    CurrentWindSpeed = $"{forecastResponse.current.wind_kph} Km/h";
                    CurrentWindDirection = $"{forecastResponse.current.wind_dir}";

                    CurrentDescription = $"{forecastResponse.current.condition.text}";


                    var tempIcon = DoTempICon(forecastResponse.current.condition.text.ToLower());
                    if (string.IsNullOrEmpty(tempIcon))
                        CurrentTempIcon = "";
                    else
                        CurrentTempIcon = tempIcon;

                }
            }
            catch (Exception ex)
            {
                toast = DoToast("We encountered an error processing your request. Please try again.", "error");
                await Application.Current.MainPage.DisplayToastAsync(toast);
            }
        }

        private async void LoadLocationDetails(string city)
        {
            try
            {
                var resource= Constants.WeatherApiForecastHourly.Replace("{PARAM}", city)
                     .Replace("{KEY}", Constants.WeatherApiKey).Replace("{AQI}", Constants.WeatherApiAQI).Replace("{ALERTS}", Constants.WeatherApiAlerts);
                var endpoint = $"{Constants.WeatherApiBaseUrl}{resource}";

                //var locationData = await WeatherApiService.GetHourlyLocationWeather2(endpoint);
                var locationData = WeatherApiService.GetHourlyLocationWeather(city);

                
                if (locationData.current == null || locationData.forecast == null || locationData.location == null)
                {
                    toast = DoToast("Entered location is Invalid. Please try again.", "error");
                    await Application.Current.MainPage.DisplayToastAsync(toast);
                }
                else
                {
                    var tempData = locationData.forecast.forecastday[0].hour;

                    DateLocation = $"{Convert.ToDateTime(locationData.location.localtime).ToLongDateString()} " +
                        $"{Convert.ToDateTime(locationData.location.localtime).ToShortTimeString()}({locationData.location.name})";
                    CurrentLocation = $"{locationData.location.name}, {locationData.location.country}";
                    CurrentTempC = $"{locationData.current.temp_c}°";
                    CurrentTempF = $"{locationData.current.temp_f}°F";
                    CurrentHumidity = $"{locationData.current.humidity}%";
                    CurrentWindSpeed = $"{locationData.current.wind_kph} Km/h";
                    CurrentWindDirection = $"{locationData.current.wind_dir}";

                    CurrentDescription = $"{locationData.current.condition.text}";


                    var tempIcon = DoTempICon(locationData.current.condition.text.ToLower());
                    if (string.IsNullOrEmpty(tempIcon))
                        CurrentTempIcon = "";
                    else
                        CurrentTempIcon = tempIcon;


                    //if (locationData.current.is_day == 1)
                    //    BgImage = "bg3";
                    //else
                    //    BgImage = "bg1";
                    
                }
            }
            catch (Exception ex)
            {

                toast = DoToast("We encountered an error processing your request. Please try again.", "error");
                await Application.Current.MainPage.DisplayToastAsync(toast);
            }
        }

        public string DoTempICon(string condition)
        {
            var tempIcon = "";
            if (condition.Contains("partly cloudy") || condition.Contains("cloudy"))
                tempIcon = "Resources.4800-weather-partly-cloudy.json";
            else if (condition.Contains("moderate rain"))// || condition.Contains("light rain"))
                tempIcon = "Resources.35724-weather-day-rain.json";
            else if (condition.Contains("shower"))
                tempIcon = "Resources.4801-weather-partly-shower.json";
            else if (condition.Contains("rain with thunder"))
                tempIcon = "Resources.4803-weather-storm.json";
            else if (condition.Contains("mist"))
                tempIcon = "Resources.4795-weather-mist.json";
            else if (condition.Contains("snow"))
                tempIcon = "Resources.50662-heavy-snow.json";
            else if (condition == "clear")
                tempIcon = "Resources.61372-404-error-not-found.json";
            else
                tempIcon = "Resources.61372-404-error-not-found.json";

            return tempIcon;
        }
    }
}
