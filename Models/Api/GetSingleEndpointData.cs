using System.Text.Json.Serialization;

namespace BackEndTestTask.Models.Api
{
    public class GetSingleEndpointData
    {
        [JsonPropertyName("eventId")]
        public string EventId { get; set; }
    }
}
