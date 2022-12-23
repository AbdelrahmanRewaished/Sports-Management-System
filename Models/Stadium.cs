using System;
using System.Collections.Generic;

namespace Sports_Management_System.Models;

public partial class Stadium
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public int? Capacity { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Match> Matches { get; } = new List<Match>();

    public virtual ICollection<StadiumManager> StadiumManagers { get; } = new List<StadiumManager>();
}
