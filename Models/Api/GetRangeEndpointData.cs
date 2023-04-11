using System.Text.Json.Serialization;

namespace BackEndTestTask.Models.Api
{
    public class GetRangeEndpointData
    {
        [JsonPropertyName("skip")]
        public int Page { get; set; }

        [JsonPropertyName("take")]
        public int PageSize { get; set; }
    }
}
