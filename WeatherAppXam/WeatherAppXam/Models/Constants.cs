using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherAppXam.Models
{
    public class Constants
    {
        public static string WeatherApiBaseUrl = "https://api.weatherapi.com/v1";
        public static string WeatherApiCurrent = "/current.json?key={KEY}&q={PARAM}";
        public static string WeatherApiForecastHourly = "/forecast.json?key={KEY}&q={PARAM}&days={DAYS}&aqi={AQI}&alerts={ALERTS}";
        public static string WeatherApiForecast7days = "/forecast.json?key={KEY}&q={PARAM}&days={DAYS}&aqi={AQI}&alerts={ALERTS}";
        public static string WeatherApiAQI = "no";
        public static string WeatherApiAlerts = "no";
        public static string WeatherApiKey = "69e0911c49ab4e3e91b155037211910";


        public static string NewsApiBaseUrl = "https://newsapi.org/v2";
        public static string NewsApiEverything = "/everything?q={PARAM}&apiKey={KEY}";
        public static string NewsApiKey = "de37cbb597fc453da942bcf8d00b9815";


        public static string NewsApiWrapperBaseUrl = "https://newsapiorgwrapper.azurewebsites.net";
        public static string NewsApiWrapperEndpoint = "/news/fetch?city={keyword}";

        public static string EncryptionPassword = "!H6_#2dGgdt@qkI12";
        public static string Salt = "W9QYQ/rGqPUigZAzOhbzvH+nScenYqvT0NpQP3M43c8oMxqEQCETjyzqnT36NLuypSs=";

        //api.openweathermap.org/data/2.5/weather?q={city name}&appid={API key}
        public static string OpenWeatherApiBaseUrl = "https://api.openweathermap.org/data/2.5";
        public static string OpenWeatherApiOneCallEndpoint = "/onecall?lat={lat}&lon={lon}&appid={APIkey}&units=metric";
        //public static string OpenWeatherApiOneCallEndpoint = "/onecall?lat={lat}&lon={lon}&exclude={part}&appid={APIkey}";
        public static string OpenWeatherApiCurrentEndpoint = "/weather?q={city name}&appid={APIkey}";
        public static string OpenWeatherApiKey = "e111016c4c4dbc4ca07f9809f63095a7";
    }
}
