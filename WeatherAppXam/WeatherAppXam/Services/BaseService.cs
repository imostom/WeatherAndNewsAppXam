using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherAppXam.ViewModels;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherAppXam.Services
{
    public class BaseService
    {
        static HttpClient client;
        

        public static async Task<T> GetAsync<T>(string url, string key, int mins = 1, bool forcceRefresh = false)
        {
            var json = string.Empty;
            client = new HttpClient();

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                json = Barrel.Current.Get<string>(key);
            else if (!Barrel.Current.IsExpired(key))
                json = Barrel.Current.Get<string>(key);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    //var json2 = client.GetStringAsync(url).Result;
                    json = await client.GetStringAsync(url);

                    Barrel.Current.Add(key, json, TimeSpan.FromMinutes(mins));
                }
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw ex;
            }
        }

        public static async Task<T> GetAsync<T>(string url)
        {
            var json = string.Empty;
            client = new HttpClient();

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    //var json2 = client.GetStringAsync(url).Result;
                    json = await client.GetStringAsync(url);

                }
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw ex;
            }
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public void DoCleanUp()
        {
            //removes all data
            //Barrel.Current.EmptyAll();

            //removes all expired data
            //Barrel.Current.EmptyExpired();

            //param list of keys to flush
            //Barrel.Current.Empty(key: url);
        }

        public static async Task<Location> GetLocation(string type)
        {
            var location = new Location();
            var toast = new ToastOptions();
            CancellationTokenSource cts;
            try
            {
                if (type == "start")
                {
                    if (location != null)
                    {
                        location = await Geolocation.GetLastKnownLocationAsync();
                    }
                    else
                    {
                        location = await GetCurrentLocationAsync();
                    }

                }
                else if (type == "refresh")
                {
                    location = await GetCurrentLocationAsync();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                toast = new BaseViewModel().DoToast("Location feature not supported on device. Please use the search feature", "error");
                await Application.Current.MainPage.DisplayToastAsync(toast);
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
                toast = new BaseViewModel().DoToast("Location not enabled on device.", "error");
                await Application.Current.MainPage.DisplayToastAsync(toast);
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
                toast = new BaseViewModel().DoToast("Access to current location is not granted.", "error");
                await Application.Current.MainPage.DisplayToastAsync(toast);
            }
            catch (Exception ex)
            {
                // Unable to get location
                toast = new BaseViewModel().DoToast("Unable to get current location.", "error");
                await Application.Current.MainPage.DisplayToastAsync(toast);
            }

            return location;
        }


        private static async Task<Location> GetCurrentLocationAsync()
        {
            var location = new Location();
            CancellationTokenSource cts;
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                location = await Geolocation.GetLocationAsync(request, cts.Token);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return location;
        }

        public static async Task<Placemark> ReverseGeocode(double longitude, double latitude)
        {
            var resultPlacemark = new Placemark();
            try
            {
                var location = new Location
                {
                    Longitude = 41.40338,
                    Latitude = 2.17403
                };
                var result = await Geocoding.GetPlacemarksAsync(latitude, longitude);

                //Position position = new Position(37.8044866, -122.4324132);
                if (result.Any())
                {
                    resultPlacemark = result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return resultPlacemark;
        }
    }
}
