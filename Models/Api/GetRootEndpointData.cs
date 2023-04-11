using System.Text.Json.Serialization;

namespace BackEndTestTask.Models.Api
{
    public class GetRootEndpointData
    {
        [JsonPropertyName("treeName")]
        public string RootName { get; set; }
    }
}
