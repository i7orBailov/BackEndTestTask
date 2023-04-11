using System.Text.Json.Serialization;

namespace BackEndTestTask.Models.Database
{
    public class ExceptionJournal
    {
        [JsonPropertyName("eventId")]
        public string EventId { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public string QueryParams { get; set; }

        [JsonIgnore]
        public string BodyParams { get; set; }

        [JsonIgnore]
        public string StackTrace { get; set; }
    }
}
