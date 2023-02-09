using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.Fan
{
    [Authorize(Roles = "Fan")]
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public string AvailableMatchesCount { get; set; }
        public string TotalPurchasedTickets { get; set; }

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        public async Task OnGetAsync()
        {
            string Username = Auth.Auth.GetCurrentUserName(User);
            AvailableMatchesCount = NumberFormatter.getFormattedNumber(await _db.GetTotalAvailableMatches());
            TotalPurchasedTickets = NumberFormatter.getFormattedNumber(await _db.GetTotallyBoughtTickets(Username));
        }
    }
}
