using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherAppXam.Models
{
    public class SevenDaysDataModel
    {
        public string Day { get; set; }
        public string Date { get; set; }
        public string Image { get; set; }
        public string MaxWind { get; set; }
        public string Humidity { get; set; }
        public string TempHighLow { get; set; }
    }
}
