using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Text.Json.Serialization;

namespace ScSoMeAPI.Models.User
{
    public class User
    {
        [Key]
        public string username { get; set; }

        public string password { get; set; }

        public int subscriptionLevel { get; set; }

    }


}
