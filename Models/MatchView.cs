﻿using System;
using System.Collections.Generic;

namespace Sports_Management_System.Models;

public partial class MatchView
{
    public string? HostClub { get; set; }

    public string? GuestClub { get; set; }

    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
