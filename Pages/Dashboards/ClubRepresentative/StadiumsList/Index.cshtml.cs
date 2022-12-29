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
        public static string Club { get; set; }
        public static DateTime StartTime { get; set; }
        public static int MatchId { get; set; }
        public static string hostClub {get; set;}
        public static string guestClub { get; set; }
        public static string Username { get; set; }

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(string HostClub, string GuestClub, DateTime startTime)
        {
            Username = HttpContext.Session.GetString("Username")!;
            if (Username == null)
            {
                return Redirect("../../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role")!;
            if (Role != "ClubRepresentative")
            {
                return Redirect("../../../Auth/UnAuthorized");
            }
            hostClub = HostClub;
            guestClub = GuestClub;
            Club = _db.Clubs.Find(_db.getCurrentClubRepresentative(Username).ClubId)!.Name!;
            Names = _db.Database.SqlQuery<string>($"SELECT Name FROM dbo.viewAvailableStadiumsOn({HostClub},{GuestClub},{startTime})").ToList();
            Locations = _db.Database.SqlQuery<string>($"SELECT Location FROM dbo.viewAvailableStadiumsOn({HostClub},{GuestClub},{startTime})").ToList();
            Capacities = _db.Database.SqlQuery<int>($"SELECT Capacity FROM dbo.viewAvailableStadiumsOn({HostClub},{GuestClub},{startTime})").ToList();            
            StartTime = startTime;
            return null;
        }
       
        public async Task<IActionResult> OnPost(string Stadium)
        {
            await _db.Database.ExecuteSqlAsync($"exec AddHostRequest {Club}, {Stadium}, {StartTime}");
            return Redirect("ClubInfo");
        }

        public string getHostRequestStatus(string Stadium)
        {
            return _db.getHostRequestStatus(Username, hostClub, guestClub, StartTime, Stadium);
        }
    }
}
