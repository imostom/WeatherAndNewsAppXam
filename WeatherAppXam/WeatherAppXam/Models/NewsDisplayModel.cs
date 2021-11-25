using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherAppXam.Models
{
    public class NewsDisplayModel
    {
        public string NewsTitle { get; set; }
        public string NewsDescription { get; set; }
        public string NewsImage { get; set; }
        public string Url { get; set; }
        public string Source { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string PublishedAt { get; set; }
    }
}
