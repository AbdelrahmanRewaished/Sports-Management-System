using System;
using System.Collections.Generic;

namespace Sports_Management_System.Models;

public partial class HostRequest
{
    public int Id { get; set; }

    public int? RepresentativeId { get; set; }

    public int? ManagerId { get; set; }

    public int? MatchId { get; set; }

    public string? Status { get; set; }

    public virtual StadiumManager? Manager { get; set; }

    public virtual Match? Match { get; set; }

    public virtual ClubRepresentative? Representative { get; set; }
}

