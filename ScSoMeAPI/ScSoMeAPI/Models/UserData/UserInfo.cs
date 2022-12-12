using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ScSoMeAPI.Models.UserData
{
    public class UserInfo : User
    {
        // public string Username { get; set; }

        [JsonPropertyName("socialMedia")]
        public SocialMedia SocialMedia { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("profilePicture")]
        public string ProfilePicture { get; set; }

        [JsonPropertyName("coverPicture")]
        public string CoverPicture { get; set; }

        //[Required(ErrorMessage = "Location is required")]
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Only numbers for your phone number")]
        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("phonenumber")]
        public string Phonenumber { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        //public List<string> Connections { get; set; }

        [JsonPropertyName("link")]
        public ExternalLink ExternalLinks { get; set; }
    }
}

