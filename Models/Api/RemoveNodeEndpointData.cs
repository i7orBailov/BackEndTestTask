using System.Text.Json.Serialization;

namespace BackEndTestTask.Models.Api
{
    public class RemoveNodeEndpointData
    {
        [JsonPropertyName("nodeId")]
        public int NodeId { get; set; }

        [JsonPropertyName("treeName")]
        public string TreeParentName { get; set; }
    }
}
