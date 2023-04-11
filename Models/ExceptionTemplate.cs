using BackEndTestTask.Models.Enums;
using Newtonsoft.Json;

namespace BackEndTestTask.Models
{
    public class ExceptionTemplate
    {
        public ExceptionTemplate(string evenId, ExceptionType exceptionType, string exceptionMessage)
        {
            EventId = evenId;
            ExceptionType = exceptionType.ToString();
            ExceptionData = new Dictionary<MessageTemplate, string>()
            {
                { new MessageTemplate(), exceptionMessage }
            };
        }

        [JsonProperty("id")]
        public string EventId { get; set; }

        [JsonProperty("type")]
        public string ExceptionType { get; set; }

        [JsonProperty("data")]
        public Dictionary<MessageTemplate, string> ExceptionData { get; set; }
    }

    public class MessageTemplate
    {
        [JsonProperty("message")]
        public string Message { get; set; } = nameof(Message).ToLower();
    }
}