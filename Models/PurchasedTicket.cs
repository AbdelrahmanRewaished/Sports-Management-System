namespace Sports_Management_System.Models
{
    public partial class PurchasedTicket
    {
        public string? HostClub { get; set; }
        public string? GuestClub { get; set; }
        public DateTime? StartTime { get; set; }
        public string? Stadium { get; set; }
        public string? Location { get; set; }
        public int Tickets { get; set; }
    }
}
