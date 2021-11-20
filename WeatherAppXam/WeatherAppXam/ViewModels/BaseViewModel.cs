using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WeatherAppXam.Models;
using WeatherAppXam.Services;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherAppXam.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public ToastOptions toast = new ToastOptions();
        public Current CurrentReport { get; set; }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }


        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        public ToastOptions DoToast(string message, string toastType)
        {
            var messageOptions = new MessageOptions
            {
                Message = message,
                Foreground = Color.White,
                Font = Font.SystemFontOfSize(13),
                //Padding = new Thickness(20)
            };

            var toastOptions = new ToastOptions
            {
                MessageOptions = messageOptions,
                CornerRadius = new Thickness(0, 0, 0, 0),
                BackgroundColor = Color.FromHex("#262b28"),

            };

            //b0100b
            if (toastType == "error")
                toastOptions.BackgroundColor = Color.Red;
            else if (toastType == "info")
                toastOptions.BackgroundColor = Color.FromHex("#0a81c2");
            else if (toastType == "success")
                toastOptions.BackgroundColor = Color.FromHex("#207E60");
            else if (toastType == "warning")
                toastOptions.BackgroundColor = Color.FromHex("#838a01");
            else
                toastOptions.BackgroundColor = Color.FromHex("#1e211f");

            return toastOptions;
        }

    }
}
