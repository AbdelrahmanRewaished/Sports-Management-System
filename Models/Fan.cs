using System;
using System.Collections.Generic;

namespace Sports_Management_System.Models;

public partial class Fan
{
    public int NationalId { get; set; }

    public string? Name { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Address { get; set; }

    public int? PhoneNo { get; set; }

    public bool? Status { get; set; }

    public string? Username { get; set; }

    public virtual SystemUser? UsernameNavigation { get; set; }

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
