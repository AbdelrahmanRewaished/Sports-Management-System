using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.Fan.Matches
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public List<string> HostClubs { get; set; }
        public List<string> GuestClubs { get; set; }
        public List<DateTime> StartTimes { get; set; }
        public List<string> Stadiums { get; set; }
        public List<string> Locations { get; set; }
        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        

        public void OnGet()
        {
            HostClubs = _db.Database.SqlQuery<string>
                ($"SELECT HostClub FROM dbo.availableMatchesToAttend()").ToList();
            GuestClubs = _db.Database.SqlQuery<string>
                ($"SELECT GuestClub FROM dbo.availableMatchesToAttend()").ToList();
            StartTimes = _db.Database.SqlQuery<DateTime>
                ($"SELECT StartTime FROM dbo.availableMatchesToAttend()").ToList();
            Stadiums = _db.Database.SqlQuery<string>
                ($"SELECT Stadium FROM dbo.availableMatchesToAttend()").ToList();
            Locations = _db.Database.SqlQuery<string>
                ($"SELECT Location FROM dbo.availableMatchesToAttend()").ToList();
        }   

        public async Task<IActionResult> OnPost(string hostClub, string guestClub, DateTime startTime) 
        {
            _db.Database.ExecuteSql($"exec purchaseTicket {Fan.IndexModel.fan.NationalId}, {hostClub}, {guestClub}, {startTime}");
            return RedirectToPage("Index");
        }
    }
}
