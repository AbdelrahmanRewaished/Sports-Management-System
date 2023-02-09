using Microsoft.AspNetCore.Identity;

namespace Sports_Management_System.Models;

public partial class Fan
{
    public string NationalId { get; set; }

    public string? Name { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Address { get; set; }

    public string? PhoneNo { get; set; }

    public bool? Status { get; set; }

    public string? Username { get; set; }

    public virtual SystemUser? UsernameNavigation { get; set; }

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();

    public void logIn(string Username, string Password)
    {
        throw new NotImplementedException();
    }
}
