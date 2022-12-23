using System;
using System.Collections.Generic;

namespace Sports_Management_System.Models;

public partial class Club
{
    public int ClubId { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<ClubRepresentative> ClubRepresentatives { get; } = new List<ClubRepresentative>();

    public virtual ICollection<Match> MatchGuestClubs { get; } = new List<Match>();

    public virtual ICollection<Match> MatchHostClubs { get; } = new List<Match>();
}
