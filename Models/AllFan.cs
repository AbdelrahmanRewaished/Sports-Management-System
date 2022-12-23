using System;
using System.Collections.Generic;

namespace Sports_Management_System.Models;

public partial class AllFan
{
    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public int NationalId { get; set; }

    public DateTime? BirthDate { get; set; }

    public bool? Status { get; set; }
}
