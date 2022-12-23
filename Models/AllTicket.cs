using System;
using System.Collections.Generic;

namespace Sports_Management_System.Models;

public partial class AllTicket
{
    public string? HostClub { get; set; }

    public string? GuestClub { get; set; }

    public string? Name { get; set; }

    public DateTime? StartTime { get; set; }
}
