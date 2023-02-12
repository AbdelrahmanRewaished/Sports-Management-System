using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Sports_Management_System.Models;

namespace Sports_Management_System.Pages.Dashboards.Fan
{
    [Authorize(Roles = "Fan")]
    public class IndexModel : PageModel
    {
        private readonly ChampionsLeagueDbContext _db;

        public string AvailableMatchesCount { get; set; }
        public string TotalAttendedMatches { get; set; }
        public string TotalPurchasedTickets { get; set; }

        public IndexModel(ChampionsLeagueDbContext db)
        {
            _db = db;
        }
        public async Task OnGetAsync()
        {
            string Username = Auth.Auth.GetCurrentUserName(User);
            Models.Fan fan = await _db.GetCurrentFan(Username);
            AvailableMatchesCount = NumberFormatter.getFormattedNumber(await _db.GetTotalAvailableMatches());
            TotalAttendedMatches = NumberFormatter.getFormattedNumber(await _db.ViewPurchasedTickets(fan.NationalId).CountAsync());
            TotalPurchasedTickets = NumberFormatter.getFormattedNumber(await _db.GetTotallyBoughtTickets(Username));
        }
    }
}
