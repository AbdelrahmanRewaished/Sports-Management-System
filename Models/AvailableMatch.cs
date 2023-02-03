namespace Sports_Management_System.Models
{
    public partial class AvailableMatch
    {
        public string? HostClub { get; set; }
        public string? GuestClub { get; set; }
        public DateTime? StartTime { get; set; }
        public string? Stadium { get; set; }
        public string? Location { get; set; }
    }
}
