using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using WeatherAppXam.Models;

namespace WeatherAppXam.Services
{
    public class NewsApiService
    {
        public static List<Article> GetNews(string keyword)
        {
            var newsData = new List<Article>();
            try
            {
                // init with your API key
                //var newsApiClient = new NewsApiClient(Constants.NewsApiKey);
                //var currentDate = DateTime.UtcNow.AddHours(1).AddDays(-1);
                //var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
                //{
                //    Q = "Apple",
                //    SortBy = SortBys.Popularity,
                //    Language = Languages.EN,
                //    From = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day)
                //});

                var newsApiClient = new NewsApiClient("de37cbb597fc453da942bcf8d00b9815");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
                {
                    Q = "Apple",
                    SortBy = SortBys.Popularity,
                    Language = Languages.EN,
                    From = new DateTime(2021, 10, 25)
                });

                if (articlesResponse.Status == Statuses.Ok)
                {

                    newsData = articlesResponse.Articles;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return newsData;
        }

        public static NewsApiModel GetNews2(string keyword)
        {
            var newsData = new NewsApiModel();
            try
            {
                var client = new HttpClient();
                string requestUrl = Constants.NewsApiBaseUrl + Constants.NewsApiEverything.Replace("{PARAM}", keyword).Replace("{KEY}", Constants.NewsApiKey);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var response = client.GetAsync(requestUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawResult = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(rawResult))
                    {
                        newsData = JsonConvert.DeserializeObject<NewsApiModel>(rawResult);

                    }
                }
                else
                {
                    var rawResult = response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return newsData;
        }

        public static NewsApiModel GetNewsFromWrapper(string keyword)
        {
            var newsData = new NewsApiModel();
            try
            {
                var client = new HttpClient();
                string requestUrl = $"https://newsapiorgwrapper.azurewebsites.net/news/fetch?city={keyword}";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var response = client.GetAsync(requestUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rawResult = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(rawResult))
                    {
                        newsData = JsonConvert.DeserializeObject<NewsApiModel>(rawResult);

                    }
                }
                else
                {
                    var rawResult = response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return newsData;
        }
    }
}
