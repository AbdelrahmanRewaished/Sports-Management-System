using System;
using System.Collections.Generic;

namespace Sports_Management_System.Models;

public partial class AllMatch
{
    public string? HostClub { get; set; }

    public string? GuestClub { get; set; }

    public DateTime? StartTime { get; set; }
}
