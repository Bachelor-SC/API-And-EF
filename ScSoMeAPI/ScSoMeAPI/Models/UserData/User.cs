using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ScSoMeAPI.Models.UserData;

public partial class User
{
    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("hashedPassword")]
    public string HashedPassword { get; set; }

    [JsonPropertyName("subscriptionLevel")]
    public int? SubscriptionLevel { get; set; }
}
