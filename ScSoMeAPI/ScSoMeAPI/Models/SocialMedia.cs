
using System.Text.Json.Serialization;

namespace ScSoMeAPI.Models
{
    public class SocialMedia
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("facebook")]
        public string Facebook { get; set; }

        [JsonPropertyName("linkedIn")]
        public string LinkedIn { get; set; }

        [JsonPropertyName("instagram")]
        public string Instagram { get; set; }

    }
}
