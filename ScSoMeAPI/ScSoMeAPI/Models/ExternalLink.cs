
using System.Text.Json.Serialization;

namespace ScSoMeAPI.Models
{
    public class ExternalLink
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

    }
}
