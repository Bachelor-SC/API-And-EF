
using System.Text.Json.Serialization;

namespace ScSoMeAPI.Models
{
    public class Location
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("postalCode")]
        public int PostalCode { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
    }
}
