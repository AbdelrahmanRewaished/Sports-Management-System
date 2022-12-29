using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager.MatchList
{
    public class AlreadyPlayedMatchesModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
       
        public List<string> HostClubs;
        public List<string> GuestClubs;
        public List<DateTime> StartTimes;
        public List<DateTime> EndTimes;
        public AlreadyPlayedMatchesModel(ChampionsLeagueDbContext db)
        {
            _db = db;
            HostClubs = _db.Database
                .SqlQuery<string>($"SELECT HostClub FROM AlreadyPlayedMatches")
                .ToList();
            GuestClubs = _db.Database
                .SqlQuery<string>($"SELECT GuestClub FROM AlreadyPlayedMatches")
                .ToList();
            StartTimes = _db.Database
                .SqlQuery<DateTime>($"SELECT StartTime FROM AlreadyPlayedMatches")
                .ToList();
            EndTimes = _db.Database
                .SqlQuery<DateTime>($"SELECT EndTime FROM AlreadyPlayedMatches")
                .ToList();
        }
        public async Task<IActionResult> OnGet()
        {
            string Username = HttpContext.Session.GetString("Username");
            if (Username == null)
            {
                return Redirect("../../../../Auth/Login");
            }
            string Role = HttpContext.Session.GetString("Role");
            if (Role != "AssociationManager")
            {
                return Redirect("../../../../Auth/UnAuthorized");
            }
            return null;
        }
    }
}
