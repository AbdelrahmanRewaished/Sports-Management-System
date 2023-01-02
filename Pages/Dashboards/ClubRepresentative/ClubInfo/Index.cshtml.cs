using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;
using Microsoft.AspNetCore.Http;

namespace Sports_Management_System.Pages.Dashboards.ClubRepresentative.ClubInfo
{
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public Club Club;
        public List<string> HostClubs;
        public List<string> GuestClubs;
        public List<DateTime> StartTimes;
        public List<DateTime> EndTimes;
        public string Username;
        public int MatchId;

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
           
        }
        public IActionResult OnGet()
        {
            Username = HttpContext.Session.GetString("Username");
            if (Username == null)
            {
                return Redirect("../../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role");
            if (Role != "ClubRepresentative")
            {
                return Redirect("../../../Auth/UnAuthorized");
            }

            Club = _db.Clubs.Find(_db.getCurrentClubRepresentative(Username).ClubId);

            HostClubs = _db.Database.SqlQuery<string>
                ($"SELECT HostClub FROM dbo.getAllUpComingMatchesOfClub({Club.ClubId})")
                .ToList();

            GuestClubs = _db.Database.SqlQuery<string>
                ($"SELECT GuestClub FROM dbo.getAllUpComingMatchesOfClub({Club.ClubId})")
                .ToList();

            StartTimes = _db.Database.SqlQuery<DateTime>
                ($"SELECT StartTime FROM dbo.getAllUpComingMatchesOfClub({Club.ClubId})")
                .ToList();

            EndTimes = _db.Database.SqlQuery<DateTime>
                ($"SELECT EndTime FROM dbo.getAllUpComingMatchesOfClub({Club.ClubId})")
                .ToList();

            return null;
        }

        public bool isMatchHostable(string hostClub, string guestClub, DateTime startTime)
        {
            return _db.isMatchHostable(Username, hostClub, guestClub, startTime);
        }
    }
}
