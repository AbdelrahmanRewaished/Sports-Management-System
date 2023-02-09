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

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        public string MatchesPendingHosting { get; set; }
        public string TotalHostedMatches { get; set; }
        public async Task OnGetAsync()
        {
            string Username = Auth.Auth.GetCurrentUserName(User);
            MatchesPendingHosting = NumberFormatter.getFormattedNumber(await _db.GetMatchesPendingHostingCount(Username));
            TotalHostedMatches = NumberFormatter.getFormattedNumber(await _db.GetTotallyHostedMatches(Username));
        }
    }
}
