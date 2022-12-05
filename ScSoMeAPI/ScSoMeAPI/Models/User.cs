using System;
using System.Collections.Generic;

namespace ScSoMeAPI.Models;

public partial class User
{
    public string Username { get; set; }

    public string HashedPassword { get; set; }

    public int? SubscriptionLevel { get; set; }
}
