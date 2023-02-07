using Newtonsoft.Json;

namespace FetchDevSample.Models
{
    public class ProcessedResponse
    {
        [JsonProperty("id")]
        public Guid Id;
    }
}
