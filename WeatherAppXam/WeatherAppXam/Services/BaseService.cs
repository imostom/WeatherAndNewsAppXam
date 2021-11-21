﻿using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherAppXam.Models;
using Xamarin.Essentials;

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

        public void DoCleanUp()
        {
            //removes all data
            //Barrel.Current.EmptyAll();

            //removes all expired data
            //Barrel.Current.EmptyExpired();

            //param list of keys to flush
            //Barrel.Current.Empty(key: url);
        }
    }
}
