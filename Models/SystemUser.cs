using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Sports_Management_System.Models;

public partial class SystemUser
{
    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public virtual ICollection<ClubRepresentative> ClubRepresentatives { get; } = new List<ClubRepresentative>();

    public virtual ICollection<Fan> Fans { get; } = new List<Fan>();

    public virtual ICollection<SportsAssociationManager> SportsAssociationManagers { get; } = new List<SportsAssociationManager>();

    public virtual ICollection<StadiumManager> StadiumManagers { get; } = new List<StadiumManager>();

    public virtual ICollection<SystemAdmin> SystemAdmins { get; } = new List<SystemAdmin>();
}
