using System.Text.Json.Serialization;

namespace ScSoMeAPI.Models.Location
{
    public class Location
    {
        public string address { get; set; }

        public int postalCode { get; set; }

        public string city { get; set; }
    }
}
