namespace Sports_Management_System.Models
{
    public class UpComingMatchesOfClub
    {
        public string? HostClub { get; set; }
        public string? GuestClub { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? IsHostable { get; set; }
    }
}
