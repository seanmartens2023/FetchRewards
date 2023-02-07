using Newtonsoft.Json;

namespace FetchDevSample.Models
{
    public class GetPointsResponse
    {
        [JsonProperty("points")]
        public long Points;
    }
}
