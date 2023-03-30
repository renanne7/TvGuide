using System.Data;
using System.Globalization;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace TvGuide
{
    class Guide
    {
        public void SearchShow()
        {
            Console.WriteLine("\nSearch Shows (single search):");
            var ShowName = Console.ReadLine();

            if(ShowName.Length > 0)
            {
                //Get json from api
                HttpClient client = new HttpClient();
                string ShowsJson = client.GetStringAsync("https://api.tvmaze.com/singlesearch/shows?q=" + ShowName).Result;   
                // Create a JsonNode DOM from a JSON string
                JsonNode tvNode = JsonNode.Parse(ShowsJson)!;
            
                //Create new object to store results
                var result = new JsonItems();

                // Create a new DataTable to store the search
                DataTable SingleShow = new DataTable();
                SingleShow.Columns.Add("Name", typeof(string));
                SingleShow.Columns.Add("Summary", typeof(string));
                SingleShow.Columns.Add("Run Time", typeof(string));
                SingleShow.Columns.Add("Web Channel Name", typeof(string));

                // Get values from a JsonNode
                JsonNode Name = tvNode!["name"]!;
                if(Name != null)
                {
                    result.Name = Name.ToJsonString().Replace("\"","");
                }
                JsonNode Summary = tvNode!["summary"]!; 
                if(Summary != null)
                {
                    result.Summary = Summary.ToJsonString().Replace("\"","");
                }
                JsonNode AvgRuntime = tvNode!["averageRuntime"]!;
                if(AvgRuntime != null)
                {
                    result.AvgRuntime = AvgRuntime.ToJsonString().Replace("\"","");
                }
                JsonNode WebChannel = tvNode!["webChannel"]!;
                if(WebChannel != null)
                {
                    result.WebChannel = WebChannel["name"]!.ToJsonString().Replace("\"","");
                }     
                SingleShow.Rows.Add(result.Name, result.Summary, result.AvgRuntime, result.WebChannel);

                //Print the show
                foreach(DataRow row in SingleShow.Rows)
                {   
                    Console.WriteLine($"\n");
                    Console.WriteLine($"Show Name: {row.Field<string>(0)}");
                    Console.WriteLine($"Summary  : {row.Field<string>(1)}");
                    Console.WriteLine($"Runtime  : {row.Field<string>(2)}");
                    Console.WriteLine($"Channel  : {row.Field<string>(3)}");
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

            if(ShowName.Length > 0)
            {
                HttpClient client = new HttpClient();
                string ShowsJson = client.GetStringAsync("https://api.tvmaze.com/search/shows?q=" + ShowName).Result;   

                // Create a JsonNode DOM from a JSON string
                JsonNode tvNode = JsonNode.Parse(ShowsJson)!;            
                //Json Array to loop through
                JsonArray tvNodeArray = tvNode!.AsArray();

                //Create new object to store results
                var result = new JsonItems();

                // Create a new DataTable to store the search
                DataTable SingleShow = new DataTable();
                SingleShow.Columns.Add("Name", typeof(string));
                SingleShow.Columns.Add("Summary", typeof(string));
                SingleShow.Columns.Add("Run Time", typeof(string));
                SingleShow.Columns.Add("Web Channel Name", typeof(string));
     
                //Loop through the json array
                foreach (JsonNode? shows in tvNodeArray)
                {
                    JsonNode Name = shows!["show"]!;
                    if(Name != null)
                    {
                        result.Name = Name["name"]!.ToString();
                    } 
                    JsonNode Summary = shows!["show"]!;
                    if(Summary != null)
                    {
                        result.Summary = Regex.Replace(Regex.Replace(Summary["summary"]!.ToString(), @"<[^>]+>|&nbsp;", "").Trim(), @"\s{2,}", " ");
                    } 
                    JsonNode Runtime = shows!["show"]!;
                    if(Runtime != null)
                    {
                        result.Runtime = Runtime["averageRuntime"]!;
                    } 
                    JsonNode WebChannel = shows!["show"]!["webChannel"]!;
                    if(WebChannel != null)
                    {
                        result.WebChannel = WebChannel["name"]!.ToString();
                    } 
                    SingleShow.Rows.Add(result.Name, result.Summary, result.AvgRuntime, result.WebChannel);
                }      
                //Print all shows
                int Show = 0;
                foreach(DataRow row in SingleShow.Rows)
                {   
                    Show +=1;
                    Console.WriteLine($"\n\n~~ Show {Show} ~~");

                    Console.WriteLine($"Show Name    : {row.Field<string>(0)}");
                    Console.WriteLine($"Summary      : {row.Field<string>(1)}");
                    Console.WriteLine($"Runtime      : {row.Field<string>(2)}");
                    Console.WriteLine($"Channel      : {row.Field<string>(3)}");
                }
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

            //Get json from api
            HttpClient client = new HttpClient();
            string ShowsJson = client.GetStringAsync("https://api.tvmaze.com/schedule?country=US&date=" + date).Result;
            // Create a JsonNode DOM from a JSON string
            JsonNode tvNode = JsonNode.Parse(ShowsJson)!;            
            //Json Array to loop through
            JsonArray tvNodeArray = tvNode!.AsArray();

            //Create new object to store results
            var result = new JsonItems();

            // Create a new DataTable to store the search
            DataTable SingleShow = new DataTable();
            SingleShow.Columns.Add("Name", typeof(string));
            SingleShow.Columns.Add("Summary", typeof(string));
            SingleShow.Columns.Add("Run Time", typeof(string));
            SingleShow.Columns.Add("Web Channel Name", typeof(string));
            SingleShow.Columns.Add("Schedule Time", typeof(string));
            SingleShow.Columns.Add("Schedule Days", typeof(string));

            //Loop through the json array
            foreach (JsonNode? shows in tvNodeArray)
            {
                JsonNode Name = shows!["show"]!;
                if(Name != null)
                {
                    result.Name = Name["name"]!.ToString();
                } 
                JsonNode Summary = shows!["show"]!;
                if(Summary != null)
                {
                    result.Summary = Regex.Replace(Regex.Replace(Summary["summary"]!.ToString(), @"<[^>]+>|&nbsp;", "").Trim(), @"\s{2,}", " ");
                } 
                JsonNode Runtime = shows!["show"]!;
                if(Runtime != null)
                {
                    result.Runtime = Runtime["runtime"]!;
                } 
                JsonNode WebChannel = shows!["show"]!["webChannel"]!;
                if(WebChannel != null)
                {
                    result.WebChannel = WebChannel["name"]!.ToString();
                } 
                JsonNode ScheduleTime = shows!["show"]!["schedule"]!;
                if(ScheduleTime != null)
                {
                    result.ScheduleTime = ScheduleTime["time"]!;
                } 
                JsonNode ScheduleDays = shows!["show"]!["schedule"]!;
                if(ScheduleDays != null)
                {
                    result.ScheduleDays = ScheduleDays!["days"]!.AsArray()!.ToJsonString().Replace("[","").Replace("]","").Replace("\"","");
                } 

                //Console.WriteLine($"Name: {result.Name}");
                SingleShow.Rows.Add(result.Name, result.Summary, result.AvgRuntime, result.WebChannel, result.ScheduleTime, result.ScheduleDays);
            }      
            
            //Print all shows
            /*int Show = 0;
            foreach(DataRow row in SingleShow.Rows)
            {   
                Show +=1;
                Console.WriteLine($"\n\n~~ Show {Show} ~~");

                Console.WriteLine($"Show Name    : {row.Field<string>(0)}");
                Console.WriteLine($"Summary      : {row.Field<string>(1)}");
                Console.WriteLine($"Runtime      : {row.Field<string>(2)}");
                Console.WriteLine($"Channel      : {row.Field<string>(3)}");
                Console.WriteLine($"Schedule Time: {row.Field<string>(4)}");
                Console.WriteLine($"Schedule Days: {row.Field<string>(5)}");
            }*/
            /*--------------------------------------------------------------------------------*/
            //Get the distinct web channel names for user to choose from
            Console.WriteLine("\nChoose a web channel:");
            
            //Get the distinct web channels in a list, remove nulls or whitespaces
            var AvailableWebChannels = SingleShow.AsEnumerable().Select(column => column[3]).Distinct().ToList();
            AvailableWebChannels = AvailableWebChannels.Where(x => !string.IsNullOrWhiteSpace(x.ToString())).ToList();
            //AvailableWebChannels.ForEach(Console.WriteLine);

            //Get the count of distinct web channels
            //var distinctCount2 = SingleShow.Rows.OfType<DataRow>().DistinctBy(x => x["Web Channel Name"]).Count(x => !string.IsNullOrWhiteSpace(x["Web Channel Name"].ToString()));

            //Print the available we channels
            int ChannelNo = 0;
            foreach (var channels in AvailableWebChannels) 
            {
                ChannelNo +=1;
                Console.WriteLine($"Channel {ChannelNo}: {channels}");
            }
            /*--------------------------------------------------------------------------------*/
            //Get the shows showing in the selected channel
            var channel = Console.ReadLine();
            //var rows = SingleShow.AsEnumerable().Where(r=> r.Field<string>("Web Channel Name") == channel);
            var rows = SingleShow.AsEnumerable().Where(r=> string.Equals(r.Field<string>("Web Channel Name"), channel, StringComparison.InvariantCultureIgnoreCase));

            var ShowNo = 0;
            foreach(DataRow row in rows)
            {   
                ShowNo +=1;
                Console.WriteLine($"\n\n~~ Show {ShowNo} ~~");

                Console.WriteLine($"Show Name    : {row.Field<string>(0)}");
                Console.WriteLine($"Summary      : {row.Field<string>(1)}");
                Console.WriteLine($"Runtime      : {row.Field<string>(2)}");
                Console.WriteLine($"Channel      : {row.Field<string>(3)}");
                Console.WriteLine($"Schedule Time: {row.Field<string>(4)}");
                Console.WriteLine($"Schedule Days: {row.Field<string>(5)}");
            } 
        }
    }
}

