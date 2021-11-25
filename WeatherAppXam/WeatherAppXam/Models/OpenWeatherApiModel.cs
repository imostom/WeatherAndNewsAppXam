using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherAppXam.Models
{

    public class OpenWeatherApiModel
    {
        public decimal lat { get; set; }
        public decimal lon { get; set; }
        public string timezone { get; set; }
        public decimal timezone_offset { get; set; }
        public OpenCurrent current { get; set; }
        public Minutely[] minutely { get; set; }
        public Hourly[] hourly { get; set; }
        public Daily[] daily { get; set; }
    }

    public class OpenCurrent
    {
        public decimal dt { get; set; }
        public decimal sunrise { get; set; }
        public decimal sunset { get; set; }
        public decimal temp { get; set; }
        public decimal feels_like { get; set; }
        public decimal pressure { get; set; }
        public decimal humidity { get; set; }
        public decimal dew_podecimal { get; set; }
        public decimal uvi { get; set; }
        public decimal clouds { get; set; }
        public decimal visibility { get; set; }
        public decimal wind_speed { get; set; }
        public decimal wind_deg { get; set; }
        public Weather[] weather { get; set; }
    }

    public class Weather
    {
        public decimal id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Minutely
    {
        public decimal dt { get; set; }
        public decimal precipitation { get; set; }
    }

    public class Hourly
    {
        public decimal dt { get; set; }
        public decimal temp { get; set; }
        public decimal feels_like { get; set; }
        public decimal pressure { get; set; }
        public decimal humidity { get; set; }
        public decimal dew_podecimal { get; set; }
        public decimal uvi { get; set; }
        public decimal clouds { get; set; }
        public decimal visibility { get; set; }
        public decimal wind_speed { get; set; }
        public decimal wind_deg { get; set; }
        public decimal wind_gust { get; set; }
        public Weather[] weather { get; set; }
        public decimal pop { get; set; }
    }

    public class Daily
    {
        public decimal dt { get; set; }
        public decimal sunrise { get; set; }
        public decimal sunset { get; set; }
        public decimal moonrise { get; set; }
        public decimal moonset { get; set; }
        public decimal moon_phase { get; set; }
        public Temp temp { get; set; }
        public Feels_Like feels_like { get; set; }
        public decimal pressure { get; set; }
        public decimal humidity { get; set; }
        public decimal dew_podecimal { get; set; }
        public decimal wind_speed { get; set; }
        public decimal wind_deg { get; set; }
        public decimal wind_gust { get; set; }
        public Weather[] weather { get; set; }
        public decimal clouds { get; set; }
        public decimal pop { get; set; }
        public decimal uvi { get; set; }
    }

    public class Temp
    {
        public decimal day { get; set; }
        public decimal min { get; set; }
        public decimal max { get; set; }
        public decimal night { get; set; }
        public decimal eve { get; set; }
        public decimal morn { get; set; }
    }

    public class Feels_Like
    {
        public decimal day { get; set; }
        public decimal night { get; set; }
        public decimal eve { get; set; }
        public decimal morn { get; set; }
    }

}
