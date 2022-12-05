using System;
using System.Collections.Generic;

namespace ScSoMeAPI.Models;

public partial class Activity
{
    public string Type { get; set; }

    public string Action { get; set; }

    public DateTime Date { get; set; }

    public string AdditionalInfo { get; set; }

    public string Username { get; set; }
}
