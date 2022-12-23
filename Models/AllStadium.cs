using System;
using System.Collections.Generic;

namespace Sports_Management_System.Models;

public partial class AllStadium
{
    public string? Name { get; set; }

    public string? Location { get; set; }

    public int? Capacity { get; set; }

    public bool? Status { get; set; }
}
