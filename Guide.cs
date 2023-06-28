using Newtonsoft.Json;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TvGuide
{
    class Guide
    {
        public async Task SearchShow()
        {
            Console.WriteLine("\nSearch Shows (single search):");
            var ShowName = Console.ReadLine();
            HttpClient client = new HttpClient();

            if(!string.IsNullOrEmpty(ShowName))
            {         
                string ShowsJson = await client.GetStringAsync("https://api.tvmaze.com/singlesearch/shows?q=" + ShowName);   
                var tvShowData = JsonConvert.DeserializeObject<TvShowSearchResults>(ShowsJson);

                Console.WriteLine($"\n");
                Console.WriteLine($"Show Name: {tvShowData?.Name}");
                Console.WriteLine($"Summary  : {Regex.Replace(tvShowData.Summary, @"<[^>]+>|&nbsp;", "").Trim()}");
                Console.WriteLine($"Runtime  : {tvShowData.averageRuntime}");
                if(tvShowData.WebChannel != null)
                {
                    Console.WriteLine($"Channel  : {tvShowData.WebChannel.Name}");
                }
            }
            else
            {
                Console.WriteLine("Could not find any show.");
            }   
        }
        public void SearchShows()
        {
            Console.WriteLine("\nSearch Shows (multi search):");
            var ShowName = Console.ReadLine();
            HttpClient client = new HttpClient();

            if(!string.IsNullOrEmpty(ShowName))
            {
                string ShowsJson = client.GetStringAsync("https://api.tvmaze.com/search/shows?q=" + ShowName).Result;   
                new Shows().printShows(ShowsJson);
            }
            else
            {
                Console.WriteLine("Could not find any show.");
            }   
        }
        public void WatchNow()
        {
            //Get today's date
            DateTime utcDate = DateTime.UtcNow;
            String cultureNames = "en-GB";
            var culture = new CultureInfo(cultureNames);
            var date = DateTime.Parse(utcDate.ToString(culture)).ToString("yyyy-MM-dd"); 

            HttpClient client = new HttpClient();
            string ShowsJson = client.GetStringAsync("https://api.tvmaze.com/schedule?country=GB&date=" + date).Result;
            new Shows().printShows(ShowsJson);
        }
    }
}

