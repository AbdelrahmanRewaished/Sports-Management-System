using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.ClubRepresentative.StadiumsList
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public List<string> Names, Locations;
        public List<int> Capacities;
        public Club Club { get; set; }
        public static DateTime StartTime { get; set; }
        public static int MatchId { get; set; }

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
            Club = _db.Clubs.Find(ClubRepresentative.IndexModel.clubRepresentative.ClubId);
        }

        public void OnGet(string HostClub, string GuestClub, DateTime startTime)
        {
            Names = _db.Database.SqlQuery<string>($"SELECT Name FROM dbo.viewAvailableStadiumsOn({HostClub},{GuestClub},{startTime})").ToList();
            Locations = _db.Database.SqlQuery<string>($"SELECT Location FROM dbo.viewAvailableStadiumsOn({HostClub},{GuestClub},{startTime})").ToList();
            Capacities = _db.Database.SqlQuery<int>($"SELECT Capacity FROM dbo.viewAvailableStadiumsOn({HostClub},{GuestClub},{startTime})").ToList();            
            StartTime = startTime;
        }
       
        public async Task<IActionResult> OnPost(string Stadium)
        {
            await _db.Database.ExecuteSqlAsync($"exec AddHostRequest {Club.Name}, {Stadium}, {StartTime}");
            return Redirect("ClubInfo");
        }

    }
}
