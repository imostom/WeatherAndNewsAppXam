using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WeatherAppXam.Models;

namespace WeatherAppXam.Services
{
    public class ApiService : BaseService
    {
        public static Task<ForecastWeatherApiModel> GetHourlyLocationWeather2(string endpoint) =>
            GetAsync<ForecastWeatherApiModel>(endpoint, "gethourlyforecast");

        public static Task<ForecastWeatherApiModel> GetSevenDaysForecast2(string endpoint) =>
            GetAsync<ForecastWeatherApiModel>(endpoint, "getsevendaysforecast");

        //public static Task<NewsApiModel> GetNews(string endpoint) =>
        //   GetAsync<NewsApiModel>(endpoint, "getnews");

        public static Task<NewsApiModel> GetNews(string endpoint) =>
           GetAsync<NewsApiModel>(endpoint);

        public static Task<OpenWeatherApiModel> GetForecastOpenWeather(string endpoint) =>
           GetAsync<OpenWeatherApiModel>(endpoint, "forecastopenweather");
        
        public static Task<OpenWeatherApiModel> GetForecastOpenWeather2(string endpoint) =>
           GetAsync<OpenWeatherApiModel>(endpoint);
    }
}
