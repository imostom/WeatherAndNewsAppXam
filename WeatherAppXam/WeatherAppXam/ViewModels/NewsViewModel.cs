using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WeatherAppXam.Models;
using WeatherAppXam.Services;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace WeatherAppXam.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        public ObservableCollection<NewsDisplayModel> NewsSource
        {
            get
            {
                return newsSource;
            }
            set
            {
                newsSource = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<NewsDisplayModel> newsSource { get; set; }

        public NewsViewModel()
        {
            string keyword = "apple";
            LoadNews(keyword);
        }

        public async void LoadNews(string keyword)
        {
            try
            {
                var endpoint = $"{Constants.NewsApiWrapperBaseUrl}{Constants.NewsApiWrapperEndpoint.Replace("{keyword}", keyword)}";
                NewsSource = new ObservableCollection<NewsDisplayModel>();

                var newsResponse = await ApiService.GetNews(endpoint);
                if (newsResponse.totalResults > 0)
                {
                    foreach (var v in newsResponse.articles)
                    {
                        //var content = v.Content;
                        NewsSource.Add(new NewsDisplayModel
                        {
                            NewsImage = v.UrlToImage,
                            NewsTitle = v.Title,
                            NewsDescription = v.Description,
                            NewsMore = v.Url,
                            Source = $"Source: {v.Source.Name}"
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                toast = DoToast("We encountered an error processing your request. Please try again.", "error");
                await Application.Current.MainPage.DisplayToastAsync(toast);
            }
        }

        public async void LoadNews2(string keyword)
        {
            try
            {
                //var newsResponse2 = NewsApiService.GetNews2(keyword);
                //var newsResponse = NewsApiService.GetNews(keyword);
                var newsResponse = NewsApiService.GetNewsFromWrapper(keyword);
                NewsSource = new ObservableCollection<NewsDisplayModel>();

                
                if (newsResponse.totalResults > 0)
                {
                    foreach (var v in newsResponse.articles)
                    {
                        //var content = v.Content;
                        NewsSource.Add(new NewsDisplayModel
                        {
                            NewsImage = v.UrlToImage,
                            NewsTitle = v.Title,
                            NewsDescription = v.Description,
                            NewsMore = v.Url,
                            Source =$"Source: {v.Source.Name}"
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                toast = DoToast("We encountered an error processing your request. Please try again.", "error");
                await Application.Current.MainPage.DisplayToastAsync(toast);
            }
        }

    }
}
