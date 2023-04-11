using System.Text.Json.Serialization;

namespace BackEndTestTask.Models.Api
{
    public class CreateNodeEndpointData
    {
        [JsonPropertyName("parentNodeId")]
        public int ParentNodeId { get; set; }

        [JsonPropertyName("nodeName")]
        public string NewNodeName { get; set; }

        [JsonPropertyName("treeName")]
        public string TreeParentName { get; set; }
    }
}
