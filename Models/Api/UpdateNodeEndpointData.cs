using System.Text.Json.Serialization;

namespace BackEndTestTask.Models.Api
{
    public class UpdateNodeEndpointData
    {
        [JsonPropertyName("nodeId")]
        public int NodeId { get; set; }

        [JsonPropertyName("newNodeName")]
        public string NewNodeName { get; set; }


        [JsonPropertyName("treeName")]
        public string TreeParentName { get; set; }
    }
}
