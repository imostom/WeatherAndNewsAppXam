using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WeatherAppXam.Models;
using WeatherAppXam.Services;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Location = Xamarin.Essentials.Location;

namespace WeatherAppXam.ViewModels
{
    public class ForecastPopupViewModel : BaseViewModel
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


        public ForecastPopupViewModel()
        {
            LoadOpenWeatherForecast();
        }


        public async void LoadOpenWeatherForecast()
        {
            try
            {
                HourlySource = new ObservableCollection<OpenWeatherHourly>();
                SevenDaysSource = new ObservableCollection<SevenDaysDataModel>();

                if (CurrentReport != null && HourlyReport != null && DailyReport != null)
                {
                    //IsLoading = false;
                    //IndicatorVisibility = false;
                    TempMetric = "°C";


                    //Hourly Forecast
                    foreach (var v in HourlyReport)
                    {
                        var date = BaseService.UnixTimeStampToDateTime(Convert.ToDouble(v.dt));

                        HourlySource.Add(new OpenWeatherHourly
                        {
                            Image = "https://openweathermap.org/img/wn/" + $"{v.weather[0].icon}@2x.png",
                            Temp = $"{Convert.ToInt32(v.temp)}°C",
                            Time = $"{date.ToString("HH:mm")}"
                        });
                    }


                    //8-Days Forecast
                    foreach (var v in DailyReport)
                    {
                        var date = BaseService.UnixTimeStampToDateTime(Convert.ToDouble(v.dt));
                        SevenDaysSource.Add(new SevenDaysDataModel
                        {
                            Date = $"{date.ToString("dd MMM, yyyy")}",
                            Day = $"{date.ToString("dddd")}",
                            Humidity = $"{v.humidity}%",
                            Image = "https://openweathermap.org/img/wn/" + $"{v.weather[0].icon}@2x.png",
                            MaxWind = $"{v.wind_speed} m/s",
                            TempHighLow = $"{Convert.ToInt32(v.temp.day)}°C"
                        });
                    }


                    CurrentTempC = Convert.ToInt32(CurrentReport.temp).ToString();
                    CurrentHumidity = $"{CurrentReport.humidity.ToString()}%";
                    CurrentWindSpeed = CurrentReport.wind_speed.ToString();
                    CurrentWindDirection = CurrentReport.wind_deg.ToString();
                    CurrentTempIcon = "https://openweathermap.org/img/wn/" + $"{CurrentReport.weather[0].icon}@2x.png";
                    CurrentDescription = CurrentReport.weather[0].description;
                    CurrentLocation = $"{SearchCity}, {PlacemarkResult.CountryName}";
                    //CurrentLocation = $"{PlacemarkResult.SubAdminArea}, {PlacemarkResult.CountryName}";
                }

                IsLoading = false;
                IndicatorVisibility = false;
            }
            catch (Exception ex)
            {
                IsLoading = false;
                IndicatorVisibility = false;

                toast = DoToast("We encountered an error processing your request. Please try again.", "error");
                await Application.Current.MainPage.DisplayToastAsync(toast);
            }
        }
    }
}
