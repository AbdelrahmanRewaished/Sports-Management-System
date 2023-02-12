using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sports_Management_System.Models;
using Microsoft.AspNetCore.Authorization;

namespace Sports_Management_System.Pages.Dashboards.ClubRepresentative
{
	[Authorize(Roles = "ClubRepresentative")]
	public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;
        public Club Club;


        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        public string PendingHostingRequests { get; set; }
        public string UnhostedUpcomingMatches { get; set; }
        public string TotalHostedMatches { get; set; }
        public async Task OnGetAsync()
        {
            string Username = Auth.Auth.GetCurrentUserName(User);
            Club = await _db.Clubs.FindAsync((await _db.GetCurrentClubRepresentative(Username)).ClubId);

            PendingHostingRequests = NumberFormatter.getFormattedNumber(await _db.GetPendingHostRequestsCount(Username));
            UnhostedUpcomingMatches = NumberFormatter.getFormattedNumber(await _db.GetUnHostedMatchesCount(Username));
            TotalHostedMatches = NumberFormatter.getFormattedNumber(await _db.GetTotallyHostedMatches(Username));
        }
    }
}
