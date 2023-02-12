namespace Sports_Management_System.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public bool? Status { get; set; }

    public int? MatchId { get; set; }

    public virtual Match? Match { get; set; }

    public virtual ICollection<Fan> FanNationals { get; } = new List<Fan>();
}
