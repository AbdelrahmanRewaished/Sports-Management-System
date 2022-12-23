using System;
using System.Collections.Generic;

namespace Sports_Management_System.Models;

public partial class ClubRepresentative
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? ClubId { get; set; }

    public string? Username { get; set; }

    public virtual Club? Club { get; set; }

    public virtual ICollection<HostRequest> HostRequests { get; } = new List<HostRequest>();

    public virtual SystemUser? UsernameNavigation { get; set; }
}
