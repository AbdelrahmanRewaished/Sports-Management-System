using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.Fan.Matches
{
    public class PurchasedTicketsModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public List<string> HostClubs { get; set; }
        public List<string> GuestClubs { get; set; }
        public List<DateTime> StartTimes { get; set; }
        public List<string> Stadiums { get; set; }
        public List<string> Locations { get; set; }
        public List<int> TicketsList { get; set; }
        private int NationalId { get; set; } 
        public PurchasedTicketsModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }


        public IActionResult OnGet()
        {
            string Username = HttpContext.Session.GetString("Username");
            if (Username == null)
            {
                return Redirect("../../../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role");
            if (Role != "Fan")
            {
                return Redirect("../../../../Auth/UnAuthorized");
            }
            Models.Fan fan = _db.getCurrentFan(Username);
            HostClubs = _db.Database.SqlQuery<string>
                ($"SELECT HostClub FROM dbo.purchasedTicketsPerMatchFor({fan.NationalId})").ToList();
            GuestClubs = _db.Database.SqlQuery<string>
                ($"SELECT GuestClub FROM dbo.purchasedTicketsPerMatchFor({fan.NationalId})").ToList();
            StartTimes = _db.Database.SqlQuery<DateTime>
                ($"SELECT StartTime FROM dbo.purchasedTicketsPerMatchFor({fan.NationalId})").ToList();
            Stadiums = _db.Database.SqlQuery<string>
                ($"SELECT Stadium FROM dbo.purchasedTicketsPerMatchFor({fan.NationalId})").ToList();
            Locations = _db.Database.SqlQuery<string>
                ($"SELECT Location FROM dbo.purchasedTicketsPerMatchFor({fan.NationalId})").ToList();
            TicketsList = _db.Database.SqlQuery<int>
               ($"SELECT Tickets FROM dbo.purchasedTicketsPerMatchFor({fan.NationalId})").ToList();
            return null;
        }
    }
}
