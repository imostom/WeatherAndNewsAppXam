using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherAppXam.Droid
{
    [Activity(Label = "WeatherNewsApp", MainLauncher =true, Theme = "@style/MyTheme.Splash", NoHistory =true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            Thread.Sleep(4000);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            //SetContentView(Resource.Layout.splash);
            //await SimulateStartu();
        }

        //private async Task SimulateStartu()
        //{
        //    await Task.Delay(TimeSpan.FromSeconds(3));
        //    StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        //}
    }
}