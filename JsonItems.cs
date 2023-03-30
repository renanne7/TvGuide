using System.Text.Json.Nodes;

namespace TvGuide
{
    class JsonItems
    {
        public string ?Name { get; set; }
        public string ?Summary { get; set; }
        public JsonNode ?AvgRuntime { get; set; }
        public string ?WebChannel { get; set; }
        public JsonNode ?Runtime { get; set; }
        public JsonNode ?ScheduleTime { get; set; }
        public string ?ScheduleDays { get; set; }
    }
}