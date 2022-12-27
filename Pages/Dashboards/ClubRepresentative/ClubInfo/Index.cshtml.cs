using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

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
        public int MatchId;

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;

        }
        public void OnGet()
        {
            Club = _db.Clubs.Find(ClubRepresentative.IndexModel.clubRepresentative.ClubId);
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
        }
    }
}
