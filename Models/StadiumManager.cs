namespace Sports_Management_System.Models;

public partial class StadiumManager 
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? StadiumId { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<HostRequest> HostRequests { get; } = new List<HostRequest>();

    public virtual Stadium? Stadium { get; set; }

    public virtual SystemUser? UsernameNavigation { get; set; }
}
