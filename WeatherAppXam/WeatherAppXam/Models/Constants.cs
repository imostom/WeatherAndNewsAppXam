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
    }
}
