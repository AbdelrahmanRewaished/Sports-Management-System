namespace Sports_Management_System.Models;

public partial class Match
{
    public int MatchId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? HostClubId { get; set; }

    public int? GuestClubId { get; set; }

    public int? StadiumId { get; set; }

    public virtual Club? GuestClub { get; set; }

    public virtual Club? HostClub { get; set; }

    public virtual ICollection<HostRequest> HostRequests { get; } = new List<HostRequest>();

    public virtual Stadium? Stadium { get; set; }

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();
}
