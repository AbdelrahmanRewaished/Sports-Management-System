using System;
using System.Collections.Generic;

namespace Sports_Management_System.Models;

public partial class SystemAdmin
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Username { get; set; }

    public virtual SystemUser? UsernameNavigation { get; set; }
}
