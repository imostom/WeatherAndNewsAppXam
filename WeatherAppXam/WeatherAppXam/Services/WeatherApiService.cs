using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherAppXam.Models;

namespace WeatherAppXam.Services
{
    public class WeatherApiService : BaseService
    {
        //public static Task<ForecastWeatherApiModel> GetHourlyLocationWeather2(string endpoint) =>
        //    GetAsync<ForecastWeatherApiModel>(endpoint, "gethourlyforecast");

        //public static Task<ForecastWeatherApiModel> GetSevenDaysForecast2(string endpoint) =>
        //    GetAsync<ForecastWeatherApiModel>(endpoint, "getsevendaysforecast");


        public static ForecastWeatherApiModel GetHourlyLocationWeather(string location)
        {
            var forecastData = new ForecastWeatherApiModel();
            try
            {
                var client = new HttpClient();
                var response = client.GetAsync(Constants.WeatherApiBaseUrl + Constants.WeatherApiForecastHourly.Replace("{PARAM}", location)
                    .Replace("{KEY}", Constants.WeatherApiKey).Replace("{AQI}", Constants.WeatherApiAQI).Replace("{ALERTS}", Constants.WeatherApiAlerts)).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawResult = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(rawResult))
                    {
                        forecastData = JsonConvert.DeserializeObject<ForecastWeatherApiModel>(rawResult);

                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return forecastData;
        }

        public static ForecastWeatherApiModel GetSevenDaysForecast(string location)
        {
            var forecastData = new ForecastWeatherApiModel();
            try
            {
                var client = new HttpClient();
                var url = Constants.WeatherApiBaseUrl + Constants.WeatherApiForecast7days.Replace("{PARAM}", location).Replace("{DAYS}", "7")
                    .Replace("{KEY}", Constants.WeatherApiKey).Replace("{AQI}", Constants.WeatherApiAQI).Replace("{ALERTS}", Constants.WeatherApiAlerts);
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawResult = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(rawResult))
                    {
                        forecastData = JsonConvert.DeserializeObject<ForecastWeatherApiModel>(rawResult);

                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return forecastData;
        }
    }
}
