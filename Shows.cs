using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace TvGuide
{
    class Shows
    {
        public void printShows(string ShowsJson)
        {
            var tvShowData = JsonConvert.DeserializeObject<IEnumerable<TvShowSearchResults>>(ShowsJson);

            List<string> AvailableWebChannels = new List<string>();

            int Show = 0;
            foreach(var each in tvShowData)
            {
                Show +=1;
                Console.WriteLine($"\n\n~~ Show {Show} ~~");
                Console.WriteLine($"\nShow Name: {each.Show?.Name}");
                if(each.Show?.Summary != null)
                {
                    Console.WriteLine($"Summary  : {Regex.Replace(each.Show.Summary, @"<[^>]+>|&nbsp;", "").Trim()}");
                }
                Console.WriteLine($"Runtime  : {each.Show?.averageRuntime}");
                if(each.Show?.WebChannel != null)
                {
                    Console.WriteLine($"Channel  : {each.Show.WebChannel.Name}");
                    AvailableWebChannels.Add(each.Show.WebChannel.Name);
                }
                Console.WriteLine($"Schedule Time: {each.Show?.Schedule?.Time}");
            }

            Console.WriteLine("\nChoose a web channel:");
            AvailableWebChannels.Select(x => x).Distinct();

            int ChannelNo = 0;
            foreach (var channels in AvailableWebChannels) 
            {
                ChannelNo +=1;
                Console.WriteLine($"Channel {ChannelNo}: {channels}");
            }

            //Get the shows showing in the selected channel
            var channel = Console.ReadLine();

            int ShowNo = 0;
            foreach(var each in tvShowData)
            {
                if(each.Show?.WebChannel != null && each.Show.WebChannel.Name == channel) 
                {
                    ShowNo +=1;
                    Console.WriteLine($"\n\n~~ Show {ShowNo} ~~");
                    Console.WriteLine($"\nShow Name    : {each.Show.Name}");
                    if(each.Show.Summary != null)
                    {
                        Console.WriteLine($"Summary      : {Regex.Replace(each.Show.Summary, @"<[^>]+>|&nbsp;", "").Trim()}");
                    }
                    Console.WriteLine($"Runtime      : {each.Show.averageRuntime}");
                    if(each.Show.WebChannel != null)
                    {
                        Console.WriteLine($"Channel      : {each.Show.WebChannel.Name}");
                        AvailableWebChannels.Add(each.Show.WebChannel.Name);
                    }
                    Console.WriteLine($"Schedule Time: {each.Show.Schedule?.Time}");
                }        
            }
        }
    }
}