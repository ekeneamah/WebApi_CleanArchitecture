using Newtonsoft.Json;

namespace Application.Dtos
{
    public class TravelPollicyResponseDto
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("Policy Amount")]  // Map "Policy Amount" to C# property
        public int PolicyAmount { get; set; }

        [JsonProperty("Policy Id")]  // Map "Policy Id" to C# property
        public string PolicyId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
