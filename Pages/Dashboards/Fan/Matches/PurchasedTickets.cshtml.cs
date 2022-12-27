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
        public PurchasedTicketsModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }


        public void OnGet()
        {
            HostClubs = _db.Database.SqlQuery<string>
                ($"SELECT HostClub FROM dbo.purchasedTicketsPerMatchFor({Fan.IndexModel.fan.NationalId})").ToList();
            GuestClubs = _db.Database.SqlQuery<string>
                ($"SELECT GuestClub FROM dbo.purchasedTicketsPerMatchFor({Fan.IndexModel.fan.NationalId})").ToList();
            StartTimes = _db.Database.SqlQuery<DateTime>
                ($"SELECT StartTime FROM dbo.purchasedTicketsPerMatchFor({Fan.IndexModel.fan.NationalId})").ToList();
            Stadiums = _db.Database.SqlQuery<string>
                ($"SELECT Stadium FROM dbo.purchasedTicketsPerMatchFor({Fan.IndexModel.fan.NationalId})").ToList();
            Locations = _db.Database.SqlQuery<string>
                ($"SELECT Location FROM dbo.purchasedTicketsPerMatchFor({Fan.IndexModel.fan.NationalId})").ToList();
            TicketsList = _db.Database.SqlQuery<int>
               ($"SELECT Tickets FROM dbo.purchasedTicketsPerMatchFor({Fan.IndexModel.fan.NationalId})").ToList();

        }
    }
}
