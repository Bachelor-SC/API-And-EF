using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ScSoMeAPI.Models.User
{
    public class User
    {
        public string username { get; set; }

        public string password { get; set; }

        public int subscriptionLevel { get; set; }

    }
}
