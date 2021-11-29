using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherAppXam.Models;
using WeatherAppXam.Services;
using WeatherAppXam.Views;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherAppXam.ViewModels
{
    public class NewsViewModel : BaseViewModel
    {
        private bool _isLoading, _isVisible;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
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
  
        private static string keyword { get; set; }


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
        public NewsApiModel newsResponse { get; set; }


        public NewsViewModel()
        {
            ShowLoader();
            if (string.IsNullOrEmpty(keyword))
                keyword = "finance";
            //string keyword = "technology";
            LoadNews(keyword);
        }

        public ICommand PerformSearch => new Command<string>((string query) =>
        {
            try
            {
                keyword = query;

                if (string.IsNullOrEmpty(query))
                {
                    toast = DoToast("Please enter a search query.", "error");
                    Application.Current.MainPage.DisplayToastAsync(toast);
                }
                else
                {
                    ShowLoader();

                    //GotoPage(query);
                    LoadNews(query);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        });


        public async void LoadNews(string keyword)
        {
            try
            {
                var endpoint = $"{Constants.NewsApiWrapperBaseUrl}{Constants.NewsApiWrapperEndpoint.Replace("{keyword}", keyword)}";
                NewsSource = new ObservableCollection<NewsDisplayModel>();

                newsResponse = await ApiService.GetNews(endpoint);
                if (newsResponse.totalResults > 0)
                {
                    IsLoading = false;
                    IndicatorVisibility = false;
                    NewsSource = new ObservableCollection<NewsDisplayModel>();

                    foreach (var v in newsResponse.articles)
                    {
                        var pub = $"{v.PublishedAt.ToString("MMM dd, yyyy")}, {v.PublishedAt.ToShortTimeString()}";
                        //var pub2 = v.PublishedAt.ToShortDateString();
                        //var pub3 = v.PublishedAt.ToShortTimeString();
                        //var pub4 = v.PublishedAt.ToLongDateString();
                        //var pub5 = v.PublishedAt.ToLongTimeString();
                        //var content = v.Content;
                        NewsSource.Add(new NewsDisplayModel
                        {
                            NewsImage = v.UrlToImage,
                            NewsTitle = v.Title,
                            NewsDescription = v.Description,
                            Url = v.Url,
                            Source = $"Source: {v.Source.Name}",
                            Content = v.Content,
                            Author = v.Author,
                            PublishedAt = pub
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


        public async void NewsItem_Tapped(EventArgs tappedEventArgs)
        {
            var tappedItem = ((TappedEventArgs)tappedEventArgs).Parameter.ToString();
            
            await Browser.OpenAsync(tappedItem, BrowserLaunchMode.SystemPreferred);
            //await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new NewsPopupPage(tappedItem, NewsSource));
        }

        private async void ShowLoader()
        {
            IsLoading = true;
            IndicatorVisibility = true;
            await Task.Delay(1000);

        }

    }
}
