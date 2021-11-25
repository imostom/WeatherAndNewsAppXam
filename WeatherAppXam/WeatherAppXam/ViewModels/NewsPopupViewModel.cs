using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using WeatherAppXam.Models;
using WeatherAppXam.Services;

namespace WeatherAppXam.ViewModels
{
    public class NewsPopupViewModel : BaseViewModel
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


        public NewsPopupViewModel(string tappedItem, ObservableCollection<NewsDisplayModel> newsObjects)
        {
            LoadNewsItem(tappedItem, newsObjects);
        }

        public async void LoadNewsItem(string tappedItem, ObservableCollection<NewsDisplayModel> newsObjects)
        {
            try
            {
                //var endpoint = $"{Constants.NewsApiWrapperBaseUrl}{Constants.NewsApiWrapperEndpoint.Replace("{keyword}", keyword)}";
                //var newsResponse = await ApiService.GetNews(endpoint);
                NewsSource = new ObservableCollection<NewsDisplayModel>();
                if (newsObjects.Count > 0)
                {
                    foreach (var v in newsObjects)
                    {
                        if (v.Url == tappedItem)
                        {
                            NewsSource.Add(v);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
