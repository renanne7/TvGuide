namespace TvGuide
{
    internal class TvShowSearchResults
    {
        public string ?Name { get; init; }
        public string ?Summary { get; init; }
        public string ?averageRuntime { get; init; }
        public WebChannel ?WebChannel { get; init; }
        public Show ?Show { get; init; }
    }
    public class Show
    {
        public string ?Name { get; init; }  
        public string ?Summary { get; init; }
        public string ?averageRuntime { get; init; }
        public WebChannel ?WebChannel { get; init; }
        public Schedule ?Schedule { get; init; }
    }
        public class WebChannel
    {
        public string ?Name { get; init; }  
    }
    public class Schedule
    {
        public string ?Time { get; init; }  
    }
}