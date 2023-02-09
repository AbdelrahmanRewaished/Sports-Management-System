using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.AssociationManager
{
    [Authorize(Roles = "AssociationManager")]
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }

        public string MatchesCreatedCount { get; set; }
        public string AlreadyPlayedMatchesCount { get; set; }
        public string UpcomingMatchesCount { get; set; }
        public async Task OnGetAsync()
        {
            MatchesCreatedCount = NumberFormatter.getFormattedNumber(await _db.GetCreatedMatchesCount());
            AlreadyPlayedMatchesCount = NumberFormatter.getFormattedNumber(await _db.GetAlreadyPlayedMatchesCount());
            UpcomingMatchesCount = NumberFormatter.getFormattedNumber(await _db.GetUpcomingMatchesCount());
        }
    }
}
